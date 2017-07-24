using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Bot
{
    /// <summary>
    /// Class that acts as a bot. It logins to the Kronox and try to book a room
    /// according to the parameters set in the settings. Will try multiple times to book.
    /// </summary>
    internal class Bot
    {
        private string _username, _password, _timeInterval, _bookingDate, _building;
        private int _bookingTries;
        private DayOfWeek _dayOfWeek;

        /// <summary>
        /// Constructor that starts the bot. Loops through each settings and tries to book the room for each setting.
        /// </summary>
        public Bot()
        {
            Console.Write("Tries to book a room...");

            foreach (var setting in SettingsManager.ReadSettings())
            {
                Setup(setting);
                Start();
            }
            SettingsManager.DeleteSettings();
        }

        /// <summary>
        /// Main driver of the different stages in the booking process.
        /// </summary>
        private void Start()
        {
            _bookingTries = 0;
            var browser = Login();
            if (browser != null)
            {
                var booked = false;
                while (!booked && _bookingTries < 5)
                {
                    var rooms = GetAvailableRooms(browser);
                    if (rooms == null)
                        continue;
                    booked = Book(browser, rooms);
                    _bookingTries++;
                }
            }
        }

        /// <summary>
        /// Retrieves values from JsonSettings and sets them for variables in this class.
        /// </summary>
        /// <param name="settings">A JsonSetting object that contain settings set by the user.</param>
        private void Setup(Setting settings)
        {
            _username = settings.Username;
            _password = settings.Password;
            _building = settings.BuildingDesignation;
            _timeInterval = settings.TimeInterval;
            _dayOfWeek = settings.DayOfWeek;
            _bookingDate = DateTime.Today.ToString("yy-MM-dd");
        }

        /// <summary>
        /// Creates a browser and logs in to the booking page on Kronox.
        /// </summary>
        /// <returns>A browser that has logged in. If login was unsuccessfull, return null</returns>
        private ScrapingBrowser Login()
        {
            var browser = new ScrapingBrowser();
            browser.AllowAutoRedirect = true;
            browser.AllowMetaRedirect = true;
            var page = browser.NavigateToPage(new Uri("https://schema.mah.se/"));
            var form = page.FindFormById("loginform");
            form["username"] = _username;
            form["password"] = _password;
            form.Method = HttpVerb.Post;
            var resultsPage = form.Submit();

            if (resultsPage.Content.Contains("Välkommen på din sida på KronoX"))
                return null;

            return browser;
        }

        /// <summary>
        /// Scrapes the booking page for all rooms that are free. Creates objects from them and stores them in a list.
        /// </summary>
        /// <param name="browser">A browser object that is at the booking page.</param>
        private List<Room> GetAvailableRooms(ScrapingBrowser browser)
        {
            var AvailRoomPage = browser.NavigateToPage(new Uri($"https://schema.mah.se/ajax/ajax_resursbokning.jsp?op=hamtaBokningar&datum={_bookingDate}&flik=FLIK{_building}"));
            var nodes = AvailRoomPage.Html.SelectNodes("//td[@class='grupprum-ledig grupprum-kolumn']/a");
            if (nodes == null)
                return null;

            var rooms = new List<Room>();
            foreach (var node in nodes)
            {
                string[] split = (node.GetAttributeValue("onclick").Split('\''));
                var roomName = (split[1]);
                var timeSpan = (split[7]);
                var parsedName = ParsedRoomName(roomName);
                var room = new Room(roomName, timeSpan);
                rooms.Add(room);
            }
            return rooms;
        }

        /// <summary>
        /// Tries to book the room specified by the user. Tries booking in all three building if booking was unsuccessful and user wanted it.
        /// </summary>
        /// <param name="browser">A browser object that is at the booking page.</param>
        private bool Book(ScrapingBrowser browser, List<Room> rooms)
        {
            var roomsWithinTime = rooms.Where(room => room.Time == _timeInterval).ToList();
            var message = "";

            foreach (var room in roomsWithinTime)
            {
                var roomName = room.Name;
                var parsedTimeSpan = ParseTimeSpan(room.Time);
                var bookingPage = browser.NavigateToPage(new Uri($"https://schema.mah.se/ajax/ajax_resursbokning.jsp?op=boka&datum={_bookingDate}&id={roomName}&typ=RESURSER_LOKALER&intervall={parsedTimeSpan}&moment=Bokad&flik=FLIK{_building}"));

                message = (bookingPage.Html.InnerText.ToLower());
                if (message == "ok")
                {
                    return true;
                }
                else if (message.Contains("bokningen gick inte att spara pga kollision") || message.Contains("en tid som redan har intr") || message.Contains("du har redan skapat max antal bokningar"))
                {
                    _bookingTries = 5;
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Parses the room name into the format that the booking url requires.
        /// </summary>
        /// <param name="roomName">String representation of the name.</param>
        /// <returns>string represantation in url friendly format.</returns>
        private string ParsedRoomName(string roomName)
        {
            roomName = roomName.Replace(':', '%');
            roomName = roomName.Insert(3, "3A");
            return roomName;
        }

        /// <summary>
        /// Parses the room string interval into int taking in consideration the day and the building.
        /// </summary>
        /// <param name="timeSpan">The timespan for a room as a string.</param>
        /// <returns>The interval for that timespan.</returns>
        private int ParseTimeSpan(string timeSpan)
        {
            var index = 0;
            string[] arr = new string[0];

            if (_building == Building.Niagara)
            {
                if (_dayOfWeek == DayOfWeek.Saturday && _dayOfWeek == DayOfWeek.Sunday)
                {
                    arr = Timespans.NiagaraWeekend;
                }
                else
                {
                    arr = Timespans.NiagaraWeekDays;
                }
            }
            else if (_building == Building.Orkanen)
            {
                arr = Timespans.OrkanenDays;
            }
            else if (_building == Building.OrkanenBiblioteket)
            {
                if (_dayOfWeek == DayOfWeek.Friday)
                {
                    index = 3;
                    arr = Timespans.OrkanenBibliotekFriday;
                }
                else if (_dayOfWeek == DayOfWeek.Saturday)
                {
                    index = 2;
                    arr = Timespans.OrkanenBibliotekSaturday;
                }
                else if (_dayOfWeek != DayOfWeek.Sunday)
                {
                    arr = Timespans.OrkanenBibliotekWeekDays;
                }
            }
            index += Array.IndexOf(arr, timeSpan);
            return index;
        }
    }
}