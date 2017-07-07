using Microsoft.Win32.TaskScheduler;
using System;
using System.IO;

namespace KronoxScraperBotGUI
{
    internal class TaskScheduler
    {
        internal void SetTask()
        {
            var now = DateTime.Now;
            DateTime t = new DateTime(now.Year, now.Month, now.Day, 0, 0, 01).AddDays(1);

            using (TaskService ts = new TaskService())
            {
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "This task will start the bot booking application at midnight.";
                td.Triggers.Add(new TimeTrigger(t));
                td.Settings.AllowDemandStart = true;
                td.Settings.WakeToRun = true;
                td.Settings.RestartCount = 3;
                td.Actions.Add(new ExecAction(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)) + "/KronoxBotBooker/files/Bot.exe", null, null));
                ts.RootFolder.RegisterTaskDefinition(@"StartKronoxBotBooker", td);
            }
        }
    }
}
