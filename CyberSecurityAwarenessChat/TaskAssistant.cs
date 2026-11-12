using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CybersecurityTaskAssistant
{
    public class SecurityTask
    {
        public string TaskName { get; set; }
        public DateTime ReminderDate { get; set; }
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            string status = IsCompleted
                ? "[Completed]" : "[Pending]";

            return $"{status} {TaskName} | " +
                   $"Reminder: {ReminderDate:g}";
        }
    }

    public class TaskAssistantForm : Form
    {
        private TextBox txtTask;
        private DateTimePicker dtReminder;
        private ListBox lstTasks;

        private Timer reminderTimer;

        private List<SecurityTask> taskList =
            new List<SecurityTask>();

        public TaskAssistantForm()
        {
            BuildInterface();
            StartReminderSystem();
        }

        private void BuildInterface()
        {
            Text = "Cybersecurity Task Assistant";
            Size = new Size(700, 500);
            StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label();
            lblTitle.Text = "TASK ASSISTANT";
            lblTitle.Location = new Point(20, 20);
            lblTitle.Size = new Size(300, 30);

            txtTask = new TextBox();
            txtTask.Location = new Point(80, 75);
            txtTask.Size = new Size(300, 30);

            dtReminder = new DateTimePicker();
            dtReminder.Location = new Point(80, 115);
            dtReminder.Size = new Size(250, 30);

            Button btnAdd = new Button();
            btnAdd.Text = "Add Task";
            btnAdd.Location = new Point(450, 75);
            btnAdd.Click += BtnAdd_Click;

            Button btnComplete = new Button();
            btnComplete.Text = "Mark Complete";
            btnComplete.Location = new Point(450, 120);
            btnComplete.Click += BtnComplete_Click;

            Button btnDelete = new Button();
            btnDelete.Text = "Delete Task";
            btnDelete.Location = new Point(450, 165);
            btnDelete.Click += BtnDelete_Click;

            lstTasks = new ListBox();
            lstTasks.Location = new Point(20, 220);
            lstTasks.Size = new Size(630, 180);

            Controls.Add(lblTitle);
            Controls.Add(txtTask);
            Controls.Add(dtReminder);
            Controls.Add(btnAdd);
            Controls.Add(btnComplete);
            Controls.Add(btnDelete);
            Controls.Add(lstTasks);
        }

        private void StartReminderSystem()
        {
            reminderTimer = new Timer();
            reminderTimer.Interval = 60000;
            reminderTimer.Tick += ReminderTimer_Tick;
            reminderTimer.Start();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTask.Text))
            {
                MessageBox.Show("Enter a task.");
                return;
            }

            SecurityTask task = new SecurityTask
            {
                TaskName = txtTask.Text,
                ReminderDate = dtReminder.Value,
                IsCompleted = false
            };

            taskList.Add(task);
            RefreshTaskList();
            txtTask.Clear();

            ActivityLogForm.RecordActivity(
                "Task Added: " + task.TaskName);
        }

        private void BtnComplete_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex == -1)
            {
                MessageBox.Show("Select a task first.");
                return;
            }

            SecurityTask task =
                taskList[lstTasks.SelectedIndex];

            task.IsCompleted = true;

            RefreshTaskList();

            ActivityLogForm.RecordActivity(
                "Task Completed: " + task.TaskName);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex == -1)
            {
                MessageBox.Show("Select a task first.");
                return;
            }

            string name =
                taskList[lstTasks.SelectedIndex].TaskName;

            taskList.RemoveAt(lstTasks.SelectedIndex);

            RefreshTaskList();

            ActivityLogForm.RecordActivity(
                "Task Deleted: " + name);
        }

        private void RefreshTaskList()
        {
            lstTasks.Items.Clear();

            foreach (SecurityTask task in taskList)
            {
                lstTasks.Items.Add(task);
            }
        }

        private void ReminderTimer_Tick(object sender, EventArgs e)
        {
            foreach (SecurityTask task in taskList)
            {
                if (!task.IsCompleted &&
                    task.ReminderDate <= DateTime.Now &&
                    task.ReminderDate > DateTime.Now.AddMinutes(-1))
                {
                    MessageBox.Show(
                        "Reminder:\n" + task.TaskName);

                    ActivityLogForm.RecordActivity(
                        "Reminder Triggered: " + task.TaskName);
                }
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            reminderTimer.Stop();
            reminderTimer.Dispose();
            base.OnFormClosed(e);
        }
    }
}