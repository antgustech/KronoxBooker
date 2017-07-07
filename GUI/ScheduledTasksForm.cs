using Bot;
using System;
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
        }

        /// <summary>
        /// Updates the listview with tasks.
        /// </summary>
        private void UpdateListView()
        {
            listViewTasks.Clear();
            listViewTasks.Columns.Add("UserName");
            listViewTasks.Columns.Add("Building");
            listViewTasks.Columns.Add("Time");
            try
            {
                var settings = SettingsManager.ReadSettings();
                foreach (JsonSettings setting in settings)
                {
                    string[] row = setting.ToStringArray();
                    var item = new ListViewItem(row);
                    listViewTasks.Items.Add(item);
                }
            }catch(System.IO.FileNotFoundException)
            {
                //Nothing to handle.
            }
           
            listViewTasks.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
    }
}
