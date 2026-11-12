using System;
using System.Drawing;
using System.Windows.Forms;

namespace CybersecurityTaskAssistant
{
    public class MainForm : Form
    {
        private Button btnTasks;
        private Button btnQuiz;
        private Button btnChatbot;
        private Button btnActivityLog;

        public MainForm()
        {
            BuildInterface();
        }

        private void BuildInterface()
        {
            Text = "Cybersecurity Task Assistant";
            Size = new Size(800, 550);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.AliceBlue;

            Label lblTitle = new Label();
            lblTitle.Text = "CYBERSECURITY TASK ASSISTANT";
            lblTitle.Font = new Font("Arial", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkBlue;
            lblTitle.Location = new Point(150, 40);
            lblTitle.Size = new Size(500, 40);

            Label lblDescription = new Label();
            lblDescription.Text =
                "Select a cybersecurity feature below:";
            lblDescription.Location = new Point(250, 110);
            lblDescription.Size = new Size(350, 30);

            btnTasks = CreateButton(
                "Task Assistant", 250, 170);

            btnQuiz = CreateButton(
                "Cybersecurity Quiz", 250, 230);

            btnChatbot = CreateButton(
                "NLP Chatbot", 250, 290);

            btnActivityLog = CreateButton(
                "Activity Log", 250, 350);

            btnTasks.Click += (s, e) =>
            {
                new TaskAssistantForm().ShowDialog();
            };

            btnQuiz.Click += (s, e) =>
            {
                new QuizForm().ShowDialog();
            };

            btnChatbot.Click += (s, e) =>
            {
                new NLPChatbotForm().ShowDialog();
            };

            btnActivityLog.Click += (s, e) =>
            {
                new ActivityLogForm().ShowDialog();
            };

            Controls.Add(lblTitle);
            Controls.Add(lblDescription);
            Controls.Add(btnTasks);
            Controls.Add(btnQuiz);
            Controls.Add(btnChatbot);
            Controls.Add(btnActivityLog);
        }

        private Button CreateButton(
            string text, int x, int y)
        {
            Button button = new Button();

            button.Text = text;
            button.Location = new Point(x, y);
            button.Size = new Size(300, 45);
            button.BackColor = Color.LightSteelBlue;
            button.Font = new Font("Arial", 11,
                FontStyle.Bold);

            return button;
        }
    }
}
