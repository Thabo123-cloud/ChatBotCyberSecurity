using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CybersecurityTaskAssistant
{
    public class NLPChatbotForm : Form
    {
        private RichTextBox chatWindow;
        private TextBox txtInput;

        private Dictionary<string, string[]> keywordDictionary;

        public NLPChatbotForm()
        {
            BuildInterface();
            LoadKeywords();

            AddMessage("Bot",
                "Hello! I am your Cybersecurity NLP Chatbot.\n" +
                "Ask me about passwords, phishing, malware, " +
                "quizzes or reminders.");
        }

        private void BuildInterface()
        {
            Text = "Cybersecurity NLP Chatbot";
            Size = new Size(800, 600);
            StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label();
            lblTitle.Text = "CYBERSECURITY NLP CHATBOT";
            lblTitle.Location = new Point(20, 15);
            lblTitle.Size = new Size(400, 30);

            chatWindow = new RichTextBox();
            chatWindow.Location = new Point(20, 60);
            chatWindow.Size = new Size(740, 380);
            chatWindow.ReadOnly = true;

            txtInput = new TextBox();
            txtInput.Location = new Point(20, 470);
            txtInput.Size = new Size(600, 30);

            Button btnSend = new Button();
            btnSend.Text = "Send";
            btnSend.Location = new Point(640, 468);
            btnSend.Size = new Size(120, 35);
            btnSend.Click += BtnSend_Click;

            Controls.Add(lblTitle);
            Controls.Add(chatWindow);
            Controls.Add(txtInput);
            Controls.Add(btnSend);

            AcceptButton = btnSend;
        }

        private void LoadKeywords()
        {
            keywordDictionary =
                new Dictionary<string, string[]>
                {
                    ["greeting"] = new[] {
                        "hello", "hi", "hey", "good morning"
                    },

                    ["task"] = new[] {
                        "task", "reminder", "schedule",
                        "remind me", "add task"
                    },

                    ["quiz"] = new[] {
                        "quiz", "test my knowledge",
                        "questions", "assessment"
                    },

                    ["phishing"] = new[] {
                        "phishing", "fake email",
                        "email scam", "suspicious email"
                    },

                    ["password"] = new[] {
                        "password", "passphrase",
                        "login", "password safety"
                    },

                    ["malware"] = new[] {
                        "malware", "virus", "trojan",
                        "spyware", "ransomware"
                    }
                };
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            string message = txtInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Enter a message.");
                return;
            }

            AddMessage("You", message);

            string response = GenerateResponse(message);

            AddMessage("Bot", response);

            txtInput.Clear();
        }

        private string GenerateResponse(string message)
        {
            string input = message.ToLowerInvariant();

            if (ContainsKeywords(input, "greeting"))
                return "Hello! How can I help you with cybersecurity?";

            if (ContainsKeywords(input, "task"))
            {
                ActivityLogForm.RecordActivity(
                    "Chatbot: Task Information Requested");

                return "You can create tasks such as:\n" +
                       "- Enable 2FA\n" +
                       "- Update passwords\n" +
                       "- Run antivirus scans";
            }

            if (ContainsKeywords(input, "quiz"))
                return "Open the Cybersecurity Quiz to test your knowledge.";

            if (ContainsKeywords(input, "phishing"))
                return "Phishing is an attack where criminals use " +
                       "fake emails, websites or messages to steal information.";

            if (ContainsKeywords(input, "password"))
                return "Use long, unique passwords. Consider a password " +
                       "manager and enable two-factor authentication.";

            if (ContainsKeywords(input, "malware"))
                return "Malware is harmful software designed to damage " +
                       "systems or steal information.";

            return "I couldn't recognise that request. " +
                   "Ask me about phishing, passwords, malware, " +
                   "tasks or quizzes.";
        }

        private bool ContainsKeywords(string input, string category)
        {
            return keywordDictionary[category].Any(
                keyword => input.Contains(keyword));
        }

        private void AddMessage(string sender, string message)
        {
            chatWindow.SelectionFont =
                new Font("Arial", 10, FontStyle.Bold);

            chatWindow.AppendText(sender + ": ");

            chatWindow.SelectionFont =
                new Font("Arial", 10, FontStyle.Regular);

            chatWindow.AppendText(
                message + Environment.NewLine +
                Environment.NewLine);

            chatWindow.ScrollToCaret();
        }
    }
}