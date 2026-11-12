using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CybersecurityTaskAssistant
{
    public class ActivityLogForm : Form
    {
        private RichTextBox logDisplay;

        private static List<string> activityLog =
            new List<string>();

        public ActivityLogForm()
        {
            BuildInterface();
        }

        public static void RecordActivity(string action)
        {
            string entry =
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                + " - " + action;

            activityLog.Add(entry);
        }

        private void BuildInterface()
        {
            Text = "Cybersecurity Activity Log";
            Size = new Size(800, 600);
            StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label();
            lblTitle.Text = "CYBERSECURITY ACTIVITY LOG";
            lblTitle.Location = new Point(20, 20);
            lblTitle.Size = new Size(400, 30);

            logDisplay = new RichTextBox();
            logDisplay.Location = new Point(20, 70);
            logDisplay.Size = new Size(740, 350);
            logDisplay.ReadOnly = true;

            Button btnView = new Button();
            btnView.Text = "View Log";
            btnView.Location = new Point(200, 450);
            btnView.Size = new Size(120, 40);
            btnView.Click += BtnView_Click;

            Button btnClear = new Button();
            btnClear.Text = "Clear Log";
            btnClear.Location = new Point(350, 450);
            btnClear.Size = new Size(120, 40);
            btnClear.Click += BtnClear_Click;

            Controls.Add(lblTitle);
            Controls.Add(logDisplay);
            Controls.Add(btnView);
            Controls.Add(btnClear);
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            RecordActivity("Activity Log Viewed");
            DisplayLog();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            activityLog.Clear();

            RecordActivity("Activity Log Cleared");

            DisplayLog();
        }

        private void DisplayLog()
        {
            logDisplay.Clear();

            foreach (string entry in activityLog)
            {
                logDisplay.AppendText(
                    entry + Environment.NewLine);
            }
        }
    }
}