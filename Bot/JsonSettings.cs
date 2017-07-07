namespace Bot
{
    /// <summary>
    /// Settings for the bot. Each setting is stored in a json file that is later
    /// read by the bot.
    /// </summary>
    public class JsonSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Building { get; set; }
        public string TimeInterval { get; set; }

        /// <summary>
        ///Creates a string array of the settings with their GUI names. 
        /// </summary>
        /// <returns></returns>
        public string[] ToStringArray()
        {
            var building = "";
            switch (Building)
            {
                case 0:
                    building = "Niagara";
                    break;

                case 1:
                    building = "Orkanen";
                    break;

                case 2:
                    building = "OrkanenBiblioteket";
                    break;
            }

            var time = "";
            if (Building == 2)
            {
                switch (TimeInterval)
                {
                    case "0":
                        time = "08:00-10:00";
                        break;

                    case "1":
                        time = "10:00-12:00";
                        break;

                    case "2":
                        time = "12:00-14:00";
                        break;

                    case "3":
                        time = "14:00-16:00";
                        break;

                    case "4":
                        time = "16:00-18:00";
                        break;

                    case "5":
                        time = "18:00-20:00";
                        break;
                }
            }
            else
            {
                switch (TimeInterval)
                {
                    case "0":
                        time = "08:15-10:15";
                        break;

                    case "1":
                        time = "10:15-13:15";
                        break;

                    case "2":
                        time = "13:15-15:00";
                        break;

                    case "3":
                        time = "15:15-17:00";
                        break;

                    case "4":
                        time = "17:15-:20:00";
                        break;
                }
            }
            return new string[] { Username, building, time };
        }
    }
}
