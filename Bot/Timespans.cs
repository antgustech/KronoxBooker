namespace Bot
{
    /// <summary>
    /// The different timespans for different buildings and days.
    /// </summary>
    public static class Timespans
    {
        public static readonly string[] NiagaraWeekDays = new string[]{
        "08:15-10:00",
        "10:15-13:00",
        "13:15-15:00",
        "15:15-17:00",
        "17:15-20:00"
        };

        public static readonly string[] NiagaraWeekend = new string[]{
        "08:15-10:00",
        "10:15-13:00",
        "15:15-17:00",
        "17:15-20:00"
        };

        public static readonly string[] OrkanenDays = new string[]{
        "08:15-10:00",
        "10:15-13:00",
        "13:15-15:00",
        "15:15-17:00",
        "17:15-20:00"
        };

        public static readonly string[] OrkanenBibliotekWeekDays = new string[]{
        "08:00-10:00",
        "10:00-12:00",
        "12:00-14:00",
        "14:00-16:00",
        "16:00-18:00",
        "18:00-20:00"
        };

        public static readonly string[] OrkanenBibliotekFriday = new string[]{
        "08:00-10:00",
        "10:00-12:00",
        "12:00-14:00",
        "14:00-16:00",
        "16:00-17:00"
        };

        public static readonly string[] OrkanenBibliotekSaturday = new string[]{
        "12:00-14:00",
        "14:00-16:00",
        "11:00-12:00"
        };
    }
}