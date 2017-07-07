using Bot;
using System;
using System.Windows.Forms;

namespace KronoxScraperBotGUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
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

            SetTimeDropDown();
            textBoxPassword.PasswordChar = '*';
        }

        /// <summary>
        /// Sets the correct time intervals for the selected building.
        /// </summary>
        private void SetTimeDropDown()
        {
            listBoxTime.Items.Clear();

            if (listBoxBuilding.SelectedItem.Equals("Orkanenbiblioteket"))
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
            else
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
            var time = listBoxTime.SelectedIndex.ToString();
            SaveSettings(username, password, int.Parse(building), time);
            var shed = new TaskScheduler();
            shed.SetTask();
            MessageBox.Show("Task is set!", "Message");
        }

        /// <summary>
        /// Saves settings to file.
        /// </summary>
        private void SaveSettings(string username, string password, int building, string time)
        {
            var settings = new JsonSettings()
            {
                Username = username,
                Password = password,
                Building = building,
                TimeInterval = time,
            };
            SettingsManager.WriteSettings(settings);
        }
    }
}
