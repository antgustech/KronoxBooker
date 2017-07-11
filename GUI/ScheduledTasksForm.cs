using Bot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace KronoxScraperBotGUI
{
    public partial class ScheduledTasksForm : Form
    {
        public ScheduledTasksForm()
        {
            InitializeComponent();
            UpdateListView();
            listViewTasks.View = View.Details;
            ControlTaskButton();
        }

        /// <summary>
        /// Eventhandler for button to remove tasks.
        /// </summary>
        private void buttonRemoveSelected_Click(object sender, EventArgs e)
        {
            var list = SettingsManager.ReadSettings();
            var checkedItems = listViewTasks.CheckedItems;
            for (int i = 0; i < checkedItems.Count; i++)
            {
                list.RemoveAt(checkedItems[i].Index - i);
            }
            SettingsManager.UpdateSettings(list);
            UpdateListView();
            ControlTaskButton();
        }

        /// <summary>
        /// Updates the listview with tasks.
        /// </summary>
        private void UpdateListView()
        {
            listViewTasks.Clear();
            listViewTasks.Columns.Add("User");
            listViewTasks.Columns.Add("Building");
            listViewTasks.Columns.Add("Time");
            listViewTasks.Columns.Add("Day");
            List<JsonSettings> settings = null;
            try
            {
                settings = SettingsManager.ReadSettings();

            }
            catch (IOException)
            {
                return;
            }
                foreach (JsonSettings setting in settings)
                {
                    string[] row = ToStringArray(setting);
                    var item = new ListViewItem(row);
                    listViewTasks.Items.Add(item);
                }
            
            
            if(listViewTasks.Items.Count < 1)
            {
                TaskScheduler.RemoveTask();
                SettingsManager.DeleteSettings();
            }
            
        }

        /// <summary>
        /// Creates a jsonarray from the JsonSetting object. TODO IMPLEMENT THIS.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static string[] ToStringArray(JsonSettings setting)
        {
            var buildingName = "";
            var buildingDesignation = setting.BuildingDesignation;

            if (buildingDesignation == Building.Niagara)
            {
                buildingName = "Niagara";
            }
            else if (buildingDesignation == Building.Orkanen)
            {
                buildingName = "Orkanen";
            }
            else
            {
                buildingName = "Orkanenbiblioteket";
            }
            return new string[] { setting.Username, buildingName, setting.TimeInterval, setting.DayOfWeek.ToString() };
        }


        private void ControlTaskButton()
        {
            buttonRemoveSelected.Enabled = listViewTasks.Items.Count > 0;
        }
    }
}
