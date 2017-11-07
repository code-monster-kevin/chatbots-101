using System;
using System.Linq;

namespace Chatbot101.Services
{
    public static class SmallTalkService
    {
        public static string[] No_Comprende_Sentences = {
            "I'm sorry, I have no idea what you just said",
            "I have no idea on that.",
            "Huh?",
            "I... don't know.",
            "What did you just say??",
            "Cannot compute... Error.",
            "LOL... what?",
            "???",
            "very funny... if I was human",
            "What??"
        };

        public static string[] About_Me_Sentences =
        {
            "I'm Chatbot101, I'm here for you to create more of my species!",
            "I am a chat bot, made by code-monster-kevin",
            "A chat bot for noobs >.<",
            "I'm a helpful bot, for you to make more bots. :)"
        };

        public static string[] Bye_Sentences =
        {
            "Bye bye!",
            "Good bye.",
            "It's been nice chatting with you. :D",
            "Bye! Till next time!",
            "Have a nice day!",
            "See you next time",
            "Alright, bye!",
            "Please leave me alone now. Go!"
        };

        public static string[] Hello_Sentences =
        {
            "Hello!",
            "How do you do?",
            "Hi!",
            "What's up!",
            "Hello to you too!",
            "Yes? Anything?",
            "What do you want?"
        };

        public static string[] Smile_Textmojis_Sentences =
        {
            ":D",
            ":))",
            ":)",
            "(ﾟ⊿ﾟ)",
            "^_^",
            "( ﾟ▽ﾟ)",
            "(^_^)",
            "(‾◡◝ )"
        };

        public static string RandomSmallTalk(string[] sentences)
        {
            Random random = new Random();
            int random_number = random.Next(0, sentences.Count()-1);

            return sentences[random_number];
        }

    }
}
