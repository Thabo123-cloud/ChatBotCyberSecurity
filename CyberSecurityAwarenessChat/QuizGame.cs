using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CybersecurityTaskAssistant
{
    public class Question
    {
        public string QuestionText { get; set; }
        public string[] Options { get; set; }
        public string CorrectAnswer { get; set; }
        public string Difficulty { get; set; }
    }

    public class QuizForm : Form
    {
        private Label lblQuestion;
        private Label lblScore;
        private Label lblDifficulty;

        private RadioButton rb1, rb2, rb3, rb4;

        private Button btnNext;
        private Button btnRestart;

        private List<Question> questions =
            new List<Question>();

        private int currentQuestion = 0;
        private int score = 0;
        private int highScore = 0;

        public QuizForm()
        {
            BuildInterface();
            LoadQuestions();
            DisplayQuestion();
        }

        private void BuildInterface()
        {
            Text = "Cybersecurity Quiz Game";
            Size = new Size(800, 600);
            StartPosition = FormStartPosition.CenterScreen;

            lblQuestion = new Label();
            lblQuestion.Location = new Point(20, 100);
            lblQuestion.Size = new Size(720, 60);

            lblScore = new Label();
            lblScore.Location = new Point(550, 20);
            lblScore.Size = new Size(150, 30);

            lblDifficulty = new Label();
            lblDifficulty.Location = new Point(20, 50);
            lblDifficulty.Size = new Size(200, 30);

            rb1 = CreateRadioButton(180);
            rb2 = CreateRadioButton(230);
            rb3 = CreateRadioButton(280);
            rb4 = CreateRadioButton(330);

            btnNext = new Button();
            btnNext.Text = "Next Question";
            btnNext.Location = new Point(250, 420);
            btnNext.Size = new Size(130, 40);
            btnNext.Click += BtnNext_Click;

            btnRestart = new Button();
            btnRestart.Text = "Restart Quiz";
            btnRestart.Location = new Point(400, 420);
            btnRestart.Size = new Size(130, 40);
            btnRestart.Click += BtnRestart_Click;

            Controls.Add(lblQuestion);
            Controls.Add(lblScore);
            Controls.Add(lblDifficulty);
            Controls.Add(rb1);
            Controls.Add(rb2);
            Controls.Add(rb3);
            Controls.Add(rb4);
            Controls.Add(btnNext);
            Controls.Add(btnRestart);
        }

        private RadioButton CreateRadioButton(int y)
        {
            RadioButton rb = new RadioButton();
            rb.Location = new Point(40, y);
            rb.Size = new Size(650, 30);
            return rb;
        }

        private void LoadQuestions()
        {
            questions.Add(new Question
            {
                QuestionText = "What does phishing attempt to steal?",
                Options = new[] {
                    "Passwords", "Games", "Wallpaper", "Music"
                },
                CorrectAnswer = "Passwords",
                Difficulty = "Easy"
            });

            questions.Add(new Question
            {
                QuestionText = "Is using the same password everywhere recommended?",
                Options = new[] {
                    "True", "False", "", ""
                },
                CorrectAnswer = "False",
                Difficulty = "Easy"
            });

            questions.Add(new Question
            {
                QuestionText = "What does 2FA stand for?",
                Options = new[] {
                    "Two-Factor Authentication",
                    "Two File Access",
                    "Two Firewall Access",
                    "Two Folder Accounts"
                },
                CorrectAnswer = "Two-Factor Authentication",
                Difficulty = "Easy"
            });

            questions.Add(new Question
            {
                QuestionText = "Which password is strongest?",
                Options = new[] {
                    "123456", "password",
                    "Welcome123", "T!9#xP7@L2"
                },
                CorrectAnswer = "T!9#xP7@L2",
                Difficulty = "Easy"
            });

            questions.Add(new Question
            {
                QuestionText = "What is malware?",
                Options = new[] {
                    "Harmful software", "Firewall",
                    "Web Browser", "Email"
                },
                CorrectAnswer = "Harmful software",
                Difficulty = "Easy"
            });

            questions.Add(new Question
            {
                QuestionText = "A firewall is used to:",
                Options = new[] {
                    "Filter network traffic", "Create passwords",
                    "Store files", "Play games"
                },
                CorrectAnswer = "Filter network traffic",
                Difficulty = "Medium"
            });

            questions.Add(new Question
            {
                QuestionText = "What is ransomware?",
                Options = new[] {
                    "A browser", "Malware that encrypts files",
                    "A password", "A website"
                },
                CorrectAnswer = "Malware that encrypts files",
                Difficulty = "Medium"
            });

            questions.Add(new Question
            {
                QuestionText = "Social engineering attacks mainly target:",
                Options = new[] {
                    "People", "Firewalls",
                    "Printers", "USB Cables"
                },
                CorrectAnswer = "People",
                Difficulty = "Medium"
            });
        }

        private void DisplayQuestion()
        {
            Question q = questions[currentQuestion];

            lblQuestion.Text = q.QuestionText;
            lblDifficulty.Text = "Difficulty: " + q.Difficulty;
            lblScore.Text = "Score: " + score +
                            " | High Score: " + highScore;

            rb1.Text = q.Options[0];
            rb2.Text = q.Options[1];
            rb3.Text = q.Options[2];
            rb4.Text = q.Options[3];

            rb1.Checked = false;
            rb2.Checked = false;
            rb3.Checked = false;
            rb4.Checked = false;

            rb3.Visible = !string.IsNullOrEmpty(q.Options[2]);
            rb4.Visible = !string.IsNullOrEmpty(q.Options[3]);
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            string answer = "";

            if (rb1.Checked) answer = rb1.Text;
            else if (rb2.Checked) answer = rb2.Text;
            else if (rb3.Checked) answer = rb3.Text;
            else if (rb4.Checked) answer = rb4.Text;

            if (answer == "")
            {
                MessageBox.Show("Select an answer first.");
                return;
            }

            bool correct =
                answer == questions[currentQuestion].CorrectAnswer;

            if (correct)
            {
                score++;
                MessageBox.Show("Correct!");
            }
            else
            {
                MessageBox.Show(
                    "Incorrect!\nCorrect answer: " +
                    questions[currentQuestion].CorrectAnswer);
            }

            ActivityLogForm.RecordActivity(
                correct ? "Quiz: Correct Answer" :
                          "Quiz: Incorrect Answer");

            currentQuestion++;

            if (currentQuestion < questions.Count)
            {
                DisplayQuestion();
            }
            else
            {
                highScore = Math.Max(highScore, score);

                int percentage =
                    score * 100 / questions.Count;

                MessageBox.Show(
                    $"Quiz Complete!\nScore: {score}/{questions.Count}" +
                    $"\nPercentage: {percentage}%" +
                    $"\nHigh Score: {highScore}");

                btnNext.Enabled = false;
            }
        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            score = 0;
            currentQuestion = 0;

            btnNext.Enabled = true;

            DisplayQuestion();
        }
    }
}