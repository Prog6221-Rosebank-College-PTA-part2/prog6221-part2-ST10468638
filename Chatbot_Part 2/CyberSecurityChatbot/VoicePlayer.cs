using System;
using System.IO;
using System.Media;

namespace CyberSecurityChatbot
{
    public static class VoicePlayer
    {
        public static void PlayGreeting()
        {
            try
            {
                string filePath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "greeting.wav"
                );

                if (File.Exists(filePath))
                {
                    SoundPlayer player = new SoundPlayer(filePath);
                    player.Play();
                }
            }
            catch
            {
            }
        }
    }
}