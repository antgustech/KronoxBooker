using Shared;
using System;
using System.IO;
using System.Windows.Forms;

namespace KronoxScraperBotGUI
{
    public partial class MainForm : Form
    {
        private DateTime date;

        public MainForm()
        {
            date = DateTime.Now;
            date = date.AddDays(1);
            InitializeComponent();
        }

        /// <summary>
        /// Intializes listbox with the buildings and some other smaller forms.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var name in Building.BuildingNames)
            {
                listBoxBuilding.Items.Add(name);
                listBoxBuilding.Text = name;
            }

            listBoxBuilding.SelectedItem = Building.BuildingNames[0];

            textBoxPassword.PasswordChar = '*';
            buttonSetTask.Enabled = false;
            SetTimeDropDown();
            SetCheckBoxes();
        }

        private void SetCheckBoxes()
        {
            try
            {
                var user = HistoryManager.ReadUser();
                if (user.Name != null)
                {
                    checkBoxSaveUsername.Checked = true;
                    textBoxUsername.Text = user.Name;
                }
                if (user.Password != null)
                {
                    checkBoxSavePassword.Checked = true;
                    textBoxPassword.Text = user.Password;
                }
            }
            catch (IOException e)
            {
                //Do nothing.
            }
        }

        /// <summary>
        /// Sets the correct time intervals for the selected building and according to what day it is.
        /// </summary>
        private void SetTimeDropDown()
        {
            listBoxTime.Items.Clear();
            var day = date.DayOfWeek;

            if (listBoxBuilding.SelectedItem.Equals(Building.BuildingNames[0]))
            {
                if (day == DayOfWeek.Saturday && day == DayOfWeek.Sunday)
                {
                    listBoxTime.Items.AddRange(Timespans.NiagaraWeekend);
                    listBoxTime.SelectedItem = Timespans.NiagaraWeekend[1];
                }
                else
                {
                    listBoxTime.Items.AddRange(Timespans.NiagaraWeekDays);
                    listBoxTime.SelectedItem = Timespans.NiagaraWeekDays[1];
                }
            }
            else if (listBoxBuilding.SelectedItem.Equals(Building.BuildingNames[1]))
            {
                listBoxTime.Items.AddRange(Timespans.OrkanenDays);
                listBoxTime.SelectedItem = Timespans.OrkanenDays[1];
            }
            else if (listBoxBuilding.SelectedItem.Equals(Building.BuildingNames[2]))
            {
                if (day == DayOfWeek.Friday)
                {
                    listBoxTime.Items.AddRange(Timespans.OrkanenBibliotekFriday);
                    listBoxTime.SelectedItem = Timespans.OrkanenBibliotekFriday[1];
                }
                else if (day == DayOfWeek.Saturday)
                {
                    listBoxTime.Items.AddRange(Timespans.OrkanenBibliotekSaturday);
                    listBoxTime.SelectedItem = Timespans.OrkanenBibliotekSaturday[1];
                }
                else if (day != DayOfWeek.Sunday)
                {
                    listBoxTime.Items.AddRange(Timespans.OrkanenBibliotekWeekDays);
                    listBoxTime.SelectedItem = Timespans.OrkanenBibliotekWeekDays[1];
                }
            }
        }

        /// <summary>
        /// Eventhandler for button to show scheduled tasks.
        /// </summary>
        private void buttonTasks_Click(object sender, EventArgs e)
        {
            var scheduledForm = new ScheduledTasksForm();
            scheduledForm.ShowDialog();
        }

        /// <summary>
        /// Eventhandler for button to schedule a new task.
        /// </summary>
        private void buttonSetTask_Click(object sender, EventArgs e)
        {
            var username = textBoxUsername.Text;
            var password = textBoxPassword.Text;
            var building = listBoxBuilding.SelectedIndex.ToString();
            var time = listBoxTime.SelectedItem.ToString();
            SetTimeDropDown();
            var shed = new TaskScheduler();
            shed.ScheduleTask();
            checkCheckBoxes();
            SaveSettings(username, password, int.Parse(building), time);
        }

        private void checkCheckBoxes()
        {
            if (checkBoxSaveUsername.Checked && checkBoxSavePassword.Checked)
            {
                HistoryManager.WriteUser(new User { Name = textBoxUsername.Text, Password = textBoxPassword.Text });
            }
            else if (checkBoxSaveUsername.Checked && !checkBoxSavePassword.Checked)
            {
                HistoryManager.WriteUser(new User { Name = textBoxUsername.Text, Password = null });
            }
            else if (!checkBoxSaveUsername.Checked && checkBoxSavePassword.Checked)
            {
                HistoryManager.WriteUser(new User { Name = null, Password = textBoxPassword.Text });
            }
            else
            {
                HistoryManager.DeleteUser();
            }
        }

        /// <summary>
        /// Saves settings to file.
        /// </summary>
        private void SaveSettings(string username, string password, int building, string time)
        {
            string buildingDesignation ="";
            switch (building)
            {
                case 0:
                    buildingDesignation = Building.Niagara;
                    break;

                case 1:
                    buildingDesignation = Building.Orkanen;
                    break;

                case 2:
                    buildingDesignation = Building.OrkanenBiblioteket;
                    break;
            }

            var settings = new Setting()
            {
                Username = username,
                Password = password,
                BuildingDesignation = buildingDesignation,
                TimeInterval = time,
                DayOfWeek = date.DayOfWeek
            };
            var exists = SettingsManager.WriteSetting(settings);

            if (!exists)
            {
                MessageBox.Show("Task already exists! Please set another time, building or user.", "Message");
            }
            else
            {
                MessageBox.Show("Task is scheduled!", "Message");
            }
        }

        private void listBoxBuilding_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTimeDropDown();
            ControlTaskButton();
        }

        private void listBoxBuilding_TextChanged(object sender, EventArgs e)
        {
            SetTimeDropDown();
        }

        private void textBoxUsername_TextChanged(object sender, EventArgs e)
        {
            ControlTaskButton();
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            ControlTaskButton();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Baxtex/KronoxBooker");
        }

        private void listBoxTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            ControlTaskButton();
        }

        private void ControlTaskButton()
        {
            buttonSetTask.Enabled = !string.IsNullOrEmpty(textBoxUsername.Text) && !string.IsNullOrEmpty(textBoxPassword.Text) && listBoxTime.Items.Count > 0 && !string.IsNullOrEmpty(listBoxTime.Text);
        }
    }
}