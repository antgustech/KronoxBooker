using Microsoft.Win32.TaskScheduler;
using System;
using System.IO;

namespace KronoxScraperBotGUI
{
    internal class TaskScheduler
    {
        /// <summary>
        /// Adds a new task or updates existing.
        /// </summary>
        internal void AddTask()
        {
            var now = DateTime.Now;
            var midnightNextDay = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0).AddDays(1);
            var oneMinute = new TimeSpan(0, 00, 01, 0);
            var oneHour = new TimeSpan(0, 01, 00, 0);

            using (TaskService ts = new TaskService())
            {
                var td = ts.NewTask();
                td.RegistrationInfo.Description = "This task will start the bot booking application at midnight.";
                td.Triggers.Add(new TimeTrigger(midnightNextDay));
                td.Settings.AllowDemandStart = true;
                td.Settings.WakeToRun = true;
                td.Settings.RestartCount = 3;
                td.Settings.DisallowStartIfOnBatteries = false;
                td.Settings.StopIfGoingOnBatteries = false;
                td.Settings.StartWhenAvailable = true;
                td.Settings.RestartInterval = oneMinute;
                td.Settings.ExecutionTimeLimit = oneHour;
                td.Actions.Add(new ExecAction(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)) + "/Kronox Bot/files/Bot.exe", null, null));
                ts.RootFolder.RegisterTaskDefinition(@"StartKronoxBotBooker", td);
            }
        }

        /// <summary>
        /// Removes the task.
        /// </summary>
        internal static void RemoveTask()
        {
            using (TaskService ts = new TaskService())
            {
                try
                {
                    ts.RootFolder.DeleteTask("StartKronoxBotBooker");
                }
                catch (IOException) { }
            }
        }
    }
}