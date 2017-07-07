namespace Bot
{
    /// <summary>
    /// Represents a free room. Stores the time that the booking starts from and the name of the room. 
    /// Converts name and time to proper formats.
    /// </summary>
    public class Room
    {
        private string _prettyPrint;
        /// <summary>
        /// Creates a new Room by parsing the name and time.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="timeInterval"></param>
        public Room(string name, string timeInterval)
        {
            _prettyPrint = string.Copy(name) + " at " + string.Copy(timeInterval);
            name = name.Replace(':', '%');
            name = name.Insert(3, "3A");
            Name = name;
            var hour = timeInterval.Substring(0, 2);
            switch (hour)
            {
                case "08":
                    Time = 0;
                    break;
                case "10":
                    Time = 1;
                    break;
                case "12":
                    Time = 2;
                    break;
                case "13":
                    Time = 2;
                    break;
                case "14":
                    Time = 3;
                    break;
                case "15":
                    Time = 3;
                    break;
                case "16":
                    Time = 4;
                    break;
                case "17":
                    Time = 4;
                    break;
                case "18":
                    Time = 5;
                    break;
            }
        }
        public string Name { get; }
        public int Time { get; }
        public override string ToString()
        {
            return _prettyPrint;
        }
    }
}
