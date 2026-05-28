using System;
using System.Collections.Generic;

namespace CyberSecurityChatbot
{
    public class Chatbot
    {
        private string userName = "";
        private string favouriteTopic = "";
        private string currentTopic = "";

        private Random random = new Random();

        private Dictionary<string, List<string>> keywordResponses;

        public Chatbot()
        {
            keywordResponses = new Dictionary<string, List<string>>();

            keywordResponses["password"] = new List<string>
            {
                "Use strong passwords with letters, numbers, and symbols.",
                "Avoid using personal information in your passwords.",
                "Use different passwords for different accounts."
            };

            keywordResponses["phishing"] = new List<string>
            {
                "Never click suspicious links from unknown emails.",
                "Scammers often pretend to be trusted organisations.",
                "Always verify email addresses before responding."
            };

            keywordResponses["privacy"] = new List<string>
            {
                "Review your social media privacy settings regularly.",
                "Do not share sensitive information publicly online.",
                "Enable two-factor authentication for better privacy."
            };

            keywordResponses["scam"] = new List<string>
            {
                "Be careful of online offers that seem too good to be true.",
                "Scammers often create urgency to trick victims.",
                "Never share banking details with strangers online."
            };
        }

        public string GetResponse(string input)
        {
            string originalInput = input;

            input = input.ToLower();

            // Save user's name without changing uppercase/lowercase
            if (string.IsNullOrWhiteSpace(userName))
            {
                userName = originalInput;

                return $"Nice to meet you, {userName}! You can ask me about passwords, phishing, scams, and privacy.";
            }

            // Recall user's name
            if (input.Contains("what is my name") ||
                input.Contains("remember my name"))
            {
                return $"Your name is {userName}.";
            }

            // Sentiment detection with automatic tips
            if (input.Contains("worried"))
            {
                if (input.Contains("scam"))
                {
                    currentTopic = "scam";

                    return "It's understandable to feel worried about scams. Scammers can be very convincing. Here is a helpful tip: " +
                           GetRandomResponse("scam");
                }

                if (input.Contains("phishing"))
                {
                    currentTopic = "phishing";

                    return "It's understandable to feel worried about phishing attacks. Here is a helpful tip: " +
                           GetRandomResponse("phishing");
                }

                return "It's understandable to feel worried about cybersecurity threats. Staying alert and following safe online practices can help protect you.";
            }

            if (input.Contains("frustrated"))
            {
                return "I understand cybersecurity can feel overwhelming sometimes. Take things step-by-step and stay cautious online.";
            }

            if (input.Contains("curious"))
            {
                return "That's great! Curiosity helps you learn cybersecurity and stay protected online.";
            }

            // Conversation flow
            if (input.Contains("tell me more") ||
                input.Contains("another tip") ||
                input.Contains("explain more"))
            {
                if (currentTopic != "" &&
                    keywordResponses.ContainsKey(currentTopic))
                {
                    return GetRandomResponse(currentTopic);
                }

                return "Please ask about a cybersecurity topic first.";
            }

            // Purpose
            if (input.Contains("purpose"))
            {
                return "My purpose is to educate users about cybersecurity and online safety.";
            }

            // How are you
            if (input.Contains("how are you"))
            {
                return "I'm functioning perfectly and ready to help you stay safe online!";
            }

            // What can I ask
            if (input.Contains("what can i ask"))
            {
                return "You can ask me about passwords, phishing, privacy, and scams.";
            }

            // Memory feature
            if (input.Contains("i'm interested in privacy") ||
                input.Contains("i am interested in privacy") ||
                input.Contains("im interested in privacy"))
            {
                favouriteTopic = "privacy";
                currentTopic = "privacy";

                return "Great! I will remember that you are interested in privacy. It is a crucial part of staying safe online.";
            }

            // Favourite topic recall
            if (input.Contains("what is my favourite topic") ||
                input.Contains("what is my favorite topic") ||
                input.Contains("favourite topic") ||
                input.Contains("favorite topic"))
            {
                if (favouriteTopic != "")
                {
                    return $"Your favourite cybersecurity topic is {favouriteTopic}.";
                }

                return "You have not shared your favourite cybersecurity topic yet.";
            }

            // Recall favourite topic
            if (input.Contains("remember"))
            {
                if (favouriteTopic != "")
                {
                    return $"I remember that you are interested in {favouriteTopic}.";
                }

                return "You haven't shared a favourite topic yet.";
            }

            // Keyword recognition
            foreach (var keyword in keywordResponses.Keys)
            {
                if (input.Contains(keyword))
                {
                    currentTopic = keyword;

                    // Personalised response
                    if (favouriteTopic == keyword)
                    {
                        return $"Since you are interested in {favouriteTopic}, here is a tip: " +
                               GetRandomResponse(keyword);
                    }

                    return GetRandomResponse(keyword);
                }
            }

            // Default response
            return "I'm not sure I understand. Please try rephrasing.";
        }

        private string GetRandomResponse(string keyword)
        {
            List<string> responses = keywordResponses[keyword];

            int index = random.Next(responses.Count);

            return responses[index];
        }
    }
}