using Chatbot101.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chatbot101.UnitTest
{
    [TestClass]
    public class RockPaperScissorServiceUnitTest
    {
        [TestMethod]
        public void PlayerWinLoseOrTieTest()
        {
            GameResult rock_rock = RockPaperScissorService.PlayerWinLoseOrTie(RockPaperScissors.Rock, RockPaperScissors.Rock);
            GameResult rock_paper = RockPaperScissorService.PlayerWinLoseOrTie(RockPaperScissors.Rock, RockPaperScissors.Paper);
            GameResult rock_scissors = RockPaperScissorService.PlayerWinLoseOrTie(RockPaperScissors.Rock, RockPaperScissors.Scissors);

            GameResult paper_rock = RockPaperScissorService.PlayerWinLoseOrTie(RockPaperScissors.Paper, RockPaperScissors.Rock);
            GameResult paper_paper = RockPaperScissorService.PlayerWinLoseOrTie(RockPaperScissors.Paper, RockPaperScissors.Paper);
            GameResult paper_scissors = RockPaperScissorService.PlayerWinLoseOrTie(RockPaperScissors.Paper, RockPaperScissors.Scissors);

            GameResult scissors_rock = RockPaperScissorService.PlayerWinLoseOrTie(RockPaperScissors.Scissors, RockPaperScissors.Rock);
            GameResult scissors_paper = RockPaperScissorService.PlayerWinLoseOrTie(RockPaperScissors.Scissors, RockPaperScissors.Paper);
            GameResult scissors_scissors = RockPaperScissorService.PlayerWinLoseOrTie(RockPaperScissors.Scissors, RockPaperScissors.Scissors);
            
            Assert.AreEqual(GameResult.Tie, rock_rock);
            Assert.AreEqual(GameResult.Lose, rock_paper);
            Assert.AreEqual(GameResult.Win, rock_scissors);

            Assert.AreEqual(GameResult.Win, paper_rock);
            Assert.AreEqual(GameResult.Tie, paper_paper);
            Assert.AreEqual(GameResult.Lose, paper_scissors);

            Assert.AreEqual(GameResult.Lose, scissors_rock);
            Assert.AreEqual(GameResult.Win, scissors_paper);
            Assert.AreEqual(GameResult.Tie, scissors_scissors);
        }
    }
}
