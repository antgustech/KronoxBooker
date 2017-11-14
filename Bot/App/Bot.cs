using ScrapySharp.Extensions;
using ScrapySharp.Network;
using Shared;
using System;
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
        private string username, password, timeInterval, bookingDate, building;
        private int bookingTries;
        private DayOfWeek dayOfWeek;
        public const string Niagara = "-0017";
        public const string Orkanen = "_0000";
        public const string OrkanenBiblioteket = "_0004";

        /// <summary>
        /// Starts the bot. Loops through each settings and tries to book the room for each setting.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Kronox Bot started.");
            foreach (var setting in SettingsManager.ReadSettings())
            {
                Console.WriteLine("Tries to book a room...");
                ReadSetting(setting);
                TryToBookRoom();
            }
            SettingsManager.DeleteSettings();
        }

        /// <summary>
        /// Retrieves values from JsonSettings and sets them for variables in this class.
        /// </summary>
        /// <param name="settings">A JsonSetting object that contain settings set by the user.</param>
        private void ReadSetting(Setting settings)
        {
            username = settings.Username;
            password = settings.Password;
            building = settings.BuildingDesignation;
            timeInterval = settings.TimeInterval;
            dayOfWeek = settings.DayOfWeek;
            bookingDate = DateTime.Today.ToString("yy-MM-dd");
        }

        /// <summary>
        /// Main driver of the different stages in the booking process.
        /// </summary>
        private void TryToBookRoom()
        {
            bookingTries = 0;
            var browser = LoginToPage();
            if (browser != null)
            {
                var booked = false;
                while (!booked && bookingTries < 5)
                {
                    var rooms = GetAvailableRooms(browser);
                    if (rooms == null)
                        continue;
                    booked = BookAvailableRooms(browser, rooms);
                    bookingTries++;
                }
            }
        }

        /// <summary>
        /// Creates a browser and logs in to the booking page on Kronox.
        /// </summary>
        /// <returns>A browser that has logged in. If login was unsuccessfull, return null</returns>
        private ScrapingBrowser LoginToPage()
        {
            var browser = new ScrapingBrowser() { AllowAutoRedirect = true, AllowMetaRedirect = true };
            var page = browser.NavigateToPage(new Uri("https://schema.mah.se/"));
            var form = page.FindFormById("loginform");
            form["username"] = username;
            form["password"] = password;
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
            var AvailableRoomPage = browser.NavigateToPage(new Uri($"https://schema.mah.se/ajax/ajax_resursbokning.jsp?op=hamtaBokningar&datum={bookingDate}&flik=FLIK{building}"));
            var nodes = AvailableRoomPage.Html.SelectNodes("//td[@class='grupprum-ledig grupprum-kolumn']/a");
            if (nodes == null)
                return null;

            var rooms = new List<Room>();
            foreach (var node in nodes)
            {
                string[] nodeStringSplit = (node.GetAttributeValue("onclick").Split('\''));
                var roomName = (nodeStringSplit[1]);
                var timeSpan = (nodeStringSplit[7]);
                var parsedName = RoomNameToURL(roomName);
                var room = new Room(roomName, timeSpan);
                rooms.Add(room);
            }
            return rooms;
        }

        /// <summary>
        /// Tries to book the room specified by the user. Tries booking in all three building if booking was unsuccessful and user wanted it.
        /// </summary>
        /// <param name="browser">A browser object that is at the booking page.</param>
        private bool BookAvailableRooms(ScrapingBrowser browser, List<Room> rooms)
        {
            var roomsWithinTime = rooms.Where(room => room.Time == timeInterval).ToList();
            var message = "";

            foreach (var room in roomsWithinTime)
            {
                var roomName = room.Name;
                var parsedTimeSpan = TimeSpanToInt(room.Time);
                var bookingPage = browser.NavigateToPage(new Uri($"https://schema.mah.se/ajax/ajax_resursbokning.jsp?op=boka&datum={bookingDate}&id={roomName}&typ=RESURSER_LOKALER&intervall={parsedTimeSpan}&moment=Bokad&flik=FLIK{building}"));

                message = (bookingPage.Html.InnerText.ToLower());
                if (message == "ok")
                {
                    return true;
                }
                else if (message.Contains("bokningen gick inte att spara pga kollision") || message.Contains("en tid som redan har intr") || message.Contains("du har redan skapat max antal bokningar"))
                {
                    bookingTries = 5;
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
        private string RoomNameToURL(string roomName)
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
        private int TimeSpanToInt(string timeSpan)
        {
            var index = 0;
            string[] arr = new string[0];

            if (building == Niagara)
            {
                if (dayOfWeek == DayOfWeek.Saturday && dayOfWeek == DayOfWeek.Sunday)
                {
                    arr = Timespans.NiagaraWeekend;
                }
                else
                {
                    arr = Timespans.NiagaraWeekDays;
                }
            }
            else if (building == Orkanen)
            {
                arr = Timespans.OrkanenDays;
            }
            else if (building == OrkanenBiblioteket)
            {
                if (dayOfWeek == DayOfWeek.Friday)
                {
                    index = 3;
                    arr = Timespans.OrkanenBibliotekFriday;
                }
                else if (dayOfWeek == DayOfWeek.Saturday)
                {
                    index = 2;
                    arr = Timespans.OrkanenBibliotekSaturday;
                }
                else if (dayOfWeek != DayOfWeek.Sunday)
                {
                    arr = Timespans.OrkanenBibliotekWeekDays;
                }
            }
            return index += Array.IndexOf(arr, timeSpan);
        }
    }
}