using System;
using System.Collections.Generic;
using System.Linq;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace Bot
{
    /// <summary>
    /// Class that acts as a bot. It logins to the Kronox and try to book a room
    /// according to the parameters set in the settings. Will try multiple times to book.
    /// </summary>
    internal class Bot
    {
        private string _username, _password, _timeInterval, _bookingDate, _buildingDesignation;
        private int _bookingTries, _building;
        private readonly string _niagara = "-0017";
        private readonly string _orkanen = "_0000";
        private readonly string _orkanenBiblioteket = "_0004";

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
            //SettingsManager.DeleteSettings();
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
        private void Setup(JsonSettings settings)
        {
            _username = settings.Username;
            _password = settings.Password;
            _building = settings.Building;
            _timeInterval = settings.TimeInterval;
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
            var AvailRoomPage = browser.NavigateToPage(new Uri(BuildRoomUrl(_building)));
            var nodes = AvailRoomPage.Html.SelectNodes("//td[@class='grupprum-ledig grupprum-kolumn']/a");
            if (nodes == null)
                return null;

            var rooms = new List<Room>();
            foreach (var node in nodes)
            {
                string[] split = (node.GetAttributeValue("onclick").Split('\''));
                var roomName = (split[1]);
                var timeInterval = (split[7]);
                var room = new Room(roomName, timeInterval);
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
            var roomsWithinTime = rooms.Where(room => room.Time.ToString() == _timeInterval).ToList();
            var message = "";

            foreach (var room in roomsWithinTime)
            {
                var roomName = room.Name;
                var bookRoomUrl = $"https://schema.mah.se/ajax/ajax_resursbokning.jsp?op=boka&datum={_bookingDate}&id={roomName}&typ=RESURSER_LOKALER&intervall={_timeInterval}&moment=Bokad&flik=FLIK{_buildingDesignation}";
                var bookingPage = browser.NavigateToPage(new Uri(bookRoomUrl));

                message = (bookingPage.Html.InnerText.ToLower());
                if (message == "ok")
                {
                    return true;
                }
                else if (message.Contains("bokningen gick inte att spara pga kollision"))
                {
                    _bookingTries = 5;
                }
            }
            return false;
        }

        /// <summary>
        /// Builds the URL for logging in to the correct page for each building.
        /// </summary>
        /// <param name="i"> The index of which building to build the URL to.</param>
        private string BuildRoomUrl(int i)
        {
            var roomUrl = $"https://schema.mah.se/ajax/ajax_resursbokning.jsp?op=hamtaBokningar&datum={_bookingDate}&flik=FLIK";

            switch (i)
            {
                case 0:
                    _buildingDesignation = _niagara;
                    break;
                case 1:
                    _buildingDesignation = _orkanen;
                    break;
                case 2:
                    _buildingDesignation = _orkanenBiblioteket;
                    break;
            }
            return roomUrl += _buildingDesignation;
        }
    }
}
