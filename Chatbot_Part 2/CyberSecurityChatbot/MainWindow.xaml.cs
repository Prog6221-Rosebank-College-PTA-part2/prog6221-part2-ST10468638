using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CyberSecurityChatbot
{
    public partial class MainWindow : Window
    {
        private Chatbot chatbot;

        // Delegate
        private delegate void MessageDelegate(string sender, string message);

        public MainWindow()
        {
            InitializeComponent();

            chatbot = new Chatbot();

            VoicePlayer.PlayGreeting();

            AppendMessage("BOT", "Hello! Welcome to the Cybersecurity Awareness Bot.");
            AppendMessage("BOT", "What is your name?");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessInput();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessInput();
            }
        }

        private void ProcessInput()
        {
            string input = UserInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                AppendMessage("BOT", "Please enter something.");
                return;
            }

            // User message
            MessageDelegate displayUserMessage = AppendMessage;
            displayUserMessage("YOU", input);

            // Bot response
            string response = chatbot.GetResponse(input);

            MessageDelegate displayBotMessage = AppendMessage;
            displayBotMessage("BOT", response);

            UserInput.Clear();
        }

        private void AppendMessage(string sender, string message)
        {
            Border messageBubble = new Border();

            messageBubble.CornerRadius = new CornerRadius(15);
            messageBubble.Padding = new Thickness(15);
            messageBubble.Margin = new Thickness(10);
            messageBubble.MaxWidth = 500;

            TextBlock messageText = new TextBlock();

            messageText.Text = message;
            messageText.Foreground = Brushes.White;
            messageText.FontSize = 16;
            messageText.TextWrapping = TextWrapping.Wrap;

            messageBubble.Child = messageText;

            StackPanel container = new StackPanel();

            if (sender == "YOU")
            {
                messageBubble.Background = Brushes.LimeGreen;
                messageText.Foreground = Brushes.Black;

                container.HorizontalAlignment = HorizontalAlignment.Right;
            }
            else
            {
                messageBubble.Background = new SolidColorBrush(Color.FromRgb(40, 40, 40));

                container.HorizontalAlignment = HorizontalAlignment.Left;
            }

            container.Children.Add(messageBubble);

            ChatPanel.Children.Add(container);

            ChatScrollViewer.ScrollToEnd();
        }
    }
}