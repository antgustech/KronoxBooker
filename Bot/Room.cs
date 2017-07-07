namespace Bot
{
    /// <summary>
    /// Represents a free room. Stores the time that the booking starts from and the name of the room. 
    /// Converts name and time to proper formats.
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Creates a new Room by parsing the name and time.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="timeInterval"></param>
        public Room(string name, string timeInterval)
        {
            Name = name;
            Time = timeInterval;
        }
        public string Name { get; }
        public string Time { get; }
    }
}
