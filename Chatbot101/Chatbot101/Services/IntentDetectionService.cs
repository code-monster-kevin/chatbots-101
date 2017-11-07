namespace Chatbot101.Services
{
    public class IntentDetectionService
    {
        public enum IntentState
        {
            ABOUT_ME,
            HELLO,
            BYE_BYE,
            SMILE,
            UNKNOWN
        }

        public static IntentState CheckIntent(string user_text)
        {
            if (IsWordInSentence(user_text, Bye_Words))
            { return IntentState.BYE_BYE; }

            if (IsWordInSentence(user_text, Hello_Words))
            { return IntentState.HELLO; }

            if (IsWordInSentence(user_text, About_Me_Words))
            { return IntentState.ABOUT_ME; }

            if (IsWordInSentence(user_text, Smile_Words))
            { return IntentState.SMILE; }

            return IntentState.UNKNOWN;
        }

        private static bool IsWordInSentence(string sentence, string[] words)
        {
            foreach(var word in words)
            {
                if (sentence.ToLowerInvariant().Contains(word.ToLowerInvariant()))
                    return true;
            }
            return false;
        }

        private static string[] About_Me_Words =
        {
            "who",
            "what",
            "help",
            "about"
        };

        private static string[] Hello_Words =
        {
            "hello",
            "howdy",
            "how are you",
            "hi",
            "hiya"
        };

        private static string[] Bye_Words =
        {
            "bye",
            "seeya",
            "goodbye",
            "so long",
            "quit",
            "exit"
        };

        private static string[] Smile_Words =
        {
            "smile",
            ":)",
            "^_^",
            "happy",
            "haha",
            "hehe",
            ":D",
            "lol"
        };
    }
}
