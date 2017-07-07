using Bot;
using System;
using System.Windows.Forms;

namespace KronoxScraperBotGUI
{
    public partial class MainForm : Form
    {
        private DateTime date;
        public MainForm()
        {
            date = DateTime.Now;
            //date = date.AddDays(1);
            InitializeComponent();
        }

        /// <summary>
        /// Intializes listbox with the buildings and some other smaller forms.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            listBoxBuilding.Items.Add("Niagara");
            listBoxBuilding.Text = "Niagara";

            listBoxBuilding.Items.Add("Orkanen");
            listBoxBuilding.Text = "Orkanen";

            listBoxBuilding.Items.Add("Orkanenbiblioteket");
            listBoxBuilding.Text = "Orkanenbiblioteket";

            listBoxBuilding.SelectedItem = "Niagara";

            textBoxPassword.PasswordChar = '*';
            buttonSetTask.Enabled = false;
            SetTimeDropDown();
        }

        /// <summary>
        /// Sets the correct time intervals for the selected building and according to what day it is.
        /// </summary>
        private void SetTimeDropDown()
        {
            listBoxTime.Items.Clear();
            var day = date.DayOfWeek;

            if (listBoxBuilding.SelectedItem.Equals("Niagara"))
            {
                listBoxTime.Items.Add("08:15-10:00");
                listBoxTime.Text = "08:15-10:00";

                listBoxTime.Items.Add("10:15-13:00");
                listBoxTime.Text = "10:15-13:00";

                if (day != DayOfWeek.Saturday && day != DayOfWeek.Sunday)
                {
                    listBoxTime.Items.Add("13:15-15:00");
                    listBoxTime.Text = "13:15-15:00";
                }

                listBoxTime.Items.Add("15:15-17:00");
                listBoxTime.Text = "15:15-17:00";

                listBoxTime.Items.Add("17:15-20:00");
                listBoxTime.Text = "17:15-20:00";

                listBoxBuilding.SelectedItem = "10:15-13:00";
            }
            else if (listBoxBuilding.SelectedItem.Equals("Orkanen"))
            {
                listBoxTime.Items.Add("08:15-10:00");
                listBoxTime.Text = "08:15-10:00";

                listBoxTime.Items.Add("10:15-13:00");
                listBoxTime.Text = "10:15-13:00";

                listBoxTime.Items.Add("13:15-15:00");
                listBoxTime.Text = "13:15-15:00";

                listBoxTime.Items.Add("15:15-17:00");
                listBoxTime.Text = "15:15-17:00";

                listBoxTime.Items.Add("17:15-20:00");
                listBoxTime.Text = "17:15-20:00";

                listBoxBuilding.SelectedItem = "10:15-13:00";
            }
            else if (listBoxBuilding.SelectedItem.Equals("Orkanenbiblioteket"))
            {
                if (day == DayOfWeek.Friday)
                {
                    listBoxTime.Items.Add("08:00-10:00");
                    listBoxTime.Text = "08:00-10:00";

                    listBoxTime.Items.Add("10:00-12:00");
                    listBoxTime.Text = "10:00-12:00";

                    listBoxTime.Items.Add("12:00-14:00");
                    listBoxTime.Text = "12:00-14:00";

                    listBoxTime.Items.Add("14:00-16:00");
                    listBoxTime.Text = "14:00-16:00";

                    listBoxTime.Items.Add("16:00-17:00");
                    listBoxTime.Text = "16:00-17:00";
                    

                    listBoxBuilding.SelectedItem = "10:00 - 12:00";
                }
                else if (day == DayOfWeek.Saturday)
                {
                    listBoxTime.Items.Add("11:00-12:00");
                    listBoxTime.Text = "11:00-12:00";

                    listBoxTime.Items.Add("12:00-14:00");
                    listBoxTime.Text = "12:00-14:00";

                    listBoxTime.Items.Add("14:00-16:00");
                    listBoxTime.Text = "14:00-16:00";

                    listBoxBuilding.SelectedItem = "11:00-12:00";
                }
                else if (day != DayOfWeek.Sunday)
                {
                    listBoxTime.Items.Add("08:00-10:00");
                    listBoxTime.Text = "08:00-10:00";

                    listBoxTime.Items.Add("10:00-12:00");
                    listBoxTime.Text = "10:00-12:00";

                    listBoxTime.Items.Add("12:00-14:00");
                    listBoxTime.Text = "12:00-14:00";

                    listBoxTime.Items.Add("14:00-16:00");
                    listBoxTime.Text = "14:00-16:00";

                    listBoxTime.Items.Add("16:00-18:00");
                    listBoxTime.Text = "16:00-18:00";

                    listBoxTime.Items.Add("18:00-20:00");
                    listBoxTime.Text = "18:00-20:00";

                    listBoxBuilding.SelectedItem = "10:00 - 12:00";
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
            SaveSettings(username, password, int.Parse(building), time);
            var shed = new TaskScheduler();
            shed.SetTask();
            MessageBox.Show("Task is scheduled!", "Message");
        }



        /// <summary>
        /// Saves settings to file.
        /// </summary>
        private void SaveSettings(string username, string password, int building, string time)
        {
            var buildingDesignation = "";
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

            var settings = new JsonSettings()
            {
                Username = username,
                Password = password,
                BuildingDesignation = buildingDesignation,
                TimeInterval = time,
                DayOfWeek = date.DayOfWeek
            };
            SettingsManager.WriteSettings(settings);
        }

        private void listBoxBuilding_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTimeDropDown();
        }

        private void textBoxUsername_TextChanged(object sender, EventArgs e)
        {
            buttonSetTask.Enabled = !string.IsNullOrEmpty(textBoxUsername.Text) && !string.IsNullOrEmpty(textBoxPassword.Text);
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            buttonSetTask.Enabled = !string.IsNullOrEmpty(textBoxUsername.Text) && !string.IsNullOrEmpty(textBoxPassword.Text);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Baxtex/KronoxBooker");
        }
    }
}
