namespace KronoxScraperBotGUI
{
    partial class ScheduledTasksForm
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
            this.labelInfoTaskScheduled = new System.Windows.Forms.Label();
            this.listViewTasks = new System.Windows.Forms.ListView();
            this.buttonRemoveSelected = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelInfoTaskScheduled
            // 
            this.labelInfoTaskScheduled.AutoSize = true;
            this.labelInfoTaskScheduled.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfoTaskScheduled.ForeColor = System.Drawing.Color.White;
            this.labelInfoTaskScheduled.Location = new System.Drawing.Point(12, 9);
            this.labelInfoTaskScheduled.Name = "labelInfoTaskScheduled";
            this.labelInfoTaskScheduled.Size = new System.Drawing.Size(305, 45);
            this.labelInfoTaskScheduled.TabIndex = 1;
            this.labelInfoTaskScheduled.Text = "In this window you can see your scheduled tasks and\r\nremove them if you want. If " +
    "you want to edit something, \r\ndelete the task and create a new one.";
            // 
            // listViewTasks
            // 
            this.listViewTasks.BackColor = System.Drawing.Color.Silver;
            this.listViewTasks.CheckBoxes = true;
            this.listViewTasks.FullRowSelect = true;
            this.listViewTasks.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewTasks.Location = new System.Drawing.Point(38, 66);
            this.listViewTasks.Name = "listViewTasks";
            this.listViewTasks.Size = new System.Drawing.Size(258, 117);
            this.listViewTasks.TabIndex = 2;
            this.listViewTasks.UseCompatibleStateImageBehavior = false;
            // 
            // buttonRemoveSelected
            // 
            this.buttonRemoveSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRemoveSelected.Location = new System.Drawing.Point(94, 201);
            this.buttonRemoveSelected.Name = "buttonRemoveSelected";
            this.buttonRemoveSelected.Size = new System.Drawing.Size(139, 31);
            this.buttonRemoveSelected.TabIndex = 3;
            this.buttonRemoveSelected.Text = "Remove selected";
            this.buttonRemoveSelected.UseVisualStyleBackColor = true;
            this.buttonRemoveSelected.Click += new System.EventHandler(this.buttonRemoveSelected_Click);
            // 
            // ScheduledTasksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(333, 255);
            this.Controls.Add(this.buttonRemoveSelected);
            this.Controls.Add(this.listViewTasks);
            this.Controls.Add(this.labelInfoTaskScheduled);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ScheduledTasksForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scheduled Tasks";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInfoTaskScheduled;
        private System.Windows.Forms.ListView listViewTasks;
        private System.Windows.Forms.Button buttonRemoveSelected;
    }
}