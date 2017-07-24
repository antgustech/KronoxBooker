namespace KronoxScraperBotGUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxBuilding = new System.Windows.Forms.ComboBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxTime = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonSetTask = new System.Windows.Forms.Button();
            this.buttonTasks = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip4 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip5 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip6 = new System.Windows.Forms.ToolTip(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.checkBoxSaveUsername = new System.Windows.Forms.CheckBox();
            this.checkBoxSavePassword = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(40, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(394, 112);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(92, 149);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Building:";
            // 
            // listBoxBuilding
            // 
            this.listBoxBuilding.BackColor = System.Drawing.Color.Silver;
            this.listBoxBuilding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listBoxBuilding.FormattingEnabled = true;
            this.listBoxBuilding.Location = new System.Drawing.Point(177, 146);
            this.listBoxBuilding.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxBuilding.Name = "listBoxBuilding";
            this.listBoxBuilding.Size = new System.Drawing.Size(160, 24);
            this.listBoxBuilding.TabIndex = 3;
            this.toolTip1.SetToolTip(this.listBoxBuilding, "Choose the building where you want to book a room in");
            this.listBoxBuilding.SelectedIndexChanged += new System.EventHandler(this.listBoxBuilding_SelectedIndexChanged);
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.BackColor = System.Drawing.Color.LightGray;
            this.textBoxUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUsername.Location = new System.Drawing.Point(177, 233);
            this.textBoxUsername.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(160, 24);
            this.textBoxUsername.TabIndex = 5;
            this.toolTip1.SetToolTip(this.textBoxUsername, "You username for Kronox and it\'s learning, usually on the format AB1234");
            this.textBoxUsername.TextChanged += new System.EventHandler(this.textBoxUsername_TextChanged);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.textBoxPassword.BackColor = System.Drawing.Color.LightGray;
            this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPassword.Location = new System.Drawing.Point(177, 276);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(160, 24);
            this.textBoxPassword.TabIndex = 6;
            this.toolTip1.SetToolTip(this.textBoxPassword, "Your password for its learning/kronox.");
            this.textBoxPassword.TextChanged += new System.EventHandler(this.textBoxPassword_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(91, 233);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Username:";
            // 
            // listBoxTime
            // 
            this.listBoxTime.BackColor = System.Drawing.Color.LightGray;
            this.listBoxTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listBoxTime.FormattingEnabled = true;
            this.listBoxTime.Location = new System.Drawing.Point(177, 188);
            this.listBoxTime.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxTime.Name = "listBoxTime";
            this.listBoxTime.Size = new System.Drawing.Size(160, 24);
            this.listBoxTime.TabIndex = 8;
            this.toolTip1.SetToolTip(this.listBoxTime, "Choose the time for when you want to book your room");
            this.listBoxTime.SelectedIndexChanged += new System.EventHandler(this.listBoxTime_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(91, 191);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Time:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(91, 276);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Password:";
            // 
            // buttonSetTask
            // 
            this.buttonSetTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetTask.Location = new System.Drawing.Point(177, 330);
            this.buttonSetTask.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSetTask.Name = "buttonSetTask";
            this.buttonSetTask.Size = new System.Drawing.Size(169, 28);
            this.buttonSetTask.TabIndex = 11;
            this.buttonSetTask.Text = "Schedule task";
            this.toolTip1.SetToolTip(this.buttonSetTask, "Schedules a task  with the Windows Task manager.");
            this.buttonSetTask.UseVisualStyleBackColor = true;
            this.buttonSetTask.Click += new System.EventHandler(this.buttonSetTask_Click);
            // 
            // buttonTasks
            // 
            this.buttonTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTasks.Location = new System.Drawing.Point(90, 330);
            this.buttonTasks.Margin = new System.Windows.Forms.Padding(4);
            this.buttonTasks.Name = "buttonTasks";
            this.buttonTasks.Size = new System.Drawing.Size(61, 28);
            this.buttonTasks.TabIndex = 12;
            this.buttonTasks.Text = "Tasks";
            this.toolTip1.SetToolTip(this.buttonTasks, "See already set tasks");
            this.buttonTasks.UseVisualStyleBackColor = true;
            this.buttonTasks.Click += new System.EventHandler(this.buttonTasks_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(118, 414);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "Anton Gustafsson";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.DimGray;
            this.linkLabel1.Location = new System.Drawing.Point(255, 414);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(46, 16);
            this.linkLabel1.TabIndex = 14;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Github";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // checkBoxSaveUsername
            // 
            this.checkBoxSaveUsername.AutoSize = true;
            this.checkBoxSaveUsername.Location = new System.Drawing.Point(355, 236);
            this.checkBoxSaveUsername.Name = "checkBoxSaveUsername";
            this.checkBoxSaveUsername.Size = new System.Drawing.Size(59, 20);
            this.checkBoxSaveUsername.TabIndex = 15;
            this.checkBoxSaveUsername.Text = "Save";
            this.checkBoxSaveUsername.UseVisualStyleBackColor = true;
            this.checkBoxSaveUsername.CheckedChanged += new System.EventHandler(this.checkBoxSaveUsername_CheckedChanged);
            // 
            // checkBoxSavePassword
            // 
            this.checkBoxSavePassword.AutoSize = true;
            this.checkBoxSavePassword.Location = new System.Drawing.Point(355, 282);
            this.checkBoxSavePassword.Name = "checkBoxSavePassword";
            this.checkBoxSavePassword.Size = new System.Drawing.Size(59, 20);
            this.checkBoxSavePassword.TabIndex = 16;
            this.checkBoxSavePassword.Text = "Save";
            this.checkBoxSavePassword.UseVisualStyleBackColor = true;
            this.checkBoxSavePassword.CheckedChanged += new System.EventHandler(this.checkBoxSavePassword_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(460, 452);
            this.Controls.Add(this.checkBoxSavePassword);
            this.Controls.Add(this.checkBoxSaveUsername);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonTasks);
            this.Controls.Add(this.buttonSetTask);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listBoxTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.listBoxBuilding);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kronox room booker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox listBoxBuilding;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox listBoxTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonSetTask;
        private System.Windows.Forms.Button buttonTasks;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip3;
        private System.Windows.Forms.ToolTip toolTip4;
        private System.Windows.Forms.ToolTip toolTip5;
        private System.Windows.Forms.ToolTip toolTip6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox checkBoxSaveUsername;
        private System.Windows.Forms.CheckBox checkBoxSavePassword;
    }
}

