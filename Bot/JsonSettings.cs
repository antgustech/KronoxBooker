﻿using System;

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
        public string BuildingDesignation { get; set; }
        public string TimeInterval { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }
}