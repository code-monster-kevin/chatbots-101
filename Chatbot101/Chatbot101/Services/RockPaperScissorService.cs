using System;

namespace Chatbot101.Services
{
    public static class RockPaperScissorService
    {
        public static string[] GameOptions = new string[] { RockPaperScissors.Rock, RockPaperScissors.Paper, RockPaperScissors.Scissors };

        public static GameResult PlayerWinLoseOrTie(string player_choice, string bot_choice)
        {
            GameResult game_result = GameResult.Tie;
            switch (player_choice)
            {
                case RockPaperScissors.Paper:
                    game_result = PaperVersus(bot_choice);
                    break;
                case RockPaperScissors.Rock:
                    game_result = RockVersus(bot_choice);
                    break;
                case RockPaperScissors.Scissors:
                    game_result = ScissorsVersus(bot_choice);
                    break;
                default:
                    game_result = GameResult.Tie;
                    break;
            }
            return game_result;
        }

        public static string GetBotChoice()
        {
            long seed = DateTime.Now.Ticks;
            var random = new Random(unchecked((int)seed));
            int position = random.Next(maxValue: GameOptions.Length);

            return GameOptions[position];
        }

        public static GameResult RockVersus(string choice)
        {
            if (choice == RockPaperScissors.Rock) return GameResult.Tie;
            if (choice == RockPaperScissors.Paper) return GameResult.Lose;
            if (choice == RockPaperScissors.Scissors) return GameResult.Win;
            return GameResult.Tie;
        }

        public static GameResult PaperVersus(string choice)
        {
            if (choice == RockPaperScissors.Rock) return GameResult.Win;
            if (choice == RockPaperScissors.Paper) return GameResult.Tie;
            if (choice == RockPaperScissors.Scissors) return GameResult.Lose;
            return GameResult.Tie;
        }

        public static GameResult ScissorsVersus(string choice)
        {
            if (choice == RockPaperScissors.Rock) return GameResult.Lose;
            if (choice == RockPaperScissors.Paper) return GameResult.Win;
            if (choice == RockPaperScissors.Scissors) return GameResult.Tie;
            return GameResult.Tie;
        }
    }

    public static class RockPaperScissors
    {
        public const string Rock = "ROCK";
        public const string Paper = "PAPER";
        public const string Scissors = "SCISSORS";
    }

    public enum GameResult
    {
        Win,
        Lose,
        Tie
    }
}
