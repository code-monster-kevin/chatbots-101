using Chatbot101.Services;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;

namespace Chatbot101.Dialogs
{
    [Serializable]
    public class RockPaperScissorsDialog : IDialog<object>
    {
        const string PromptYes = "YES";
        const string PromptNo = "NO";

        public Task StartAsync(IDialogContext context)
        {
            var prompt_options = new string[] { PromptYes, PromptNo };
            string question = "Do you want to play Rock, Paper, Scissors?";
            string retry = string.Format("Please choose only {0} or {1} answers", PromptYes, PromptNo);

            PromptDialog.Choice(
                context: context,
                prompt: question,
                resume: ResumeGetChoice,
                options: prompt_options,                
                promptStyle: PromptStyle.Auto,
                retry: retry
            );
            return Task.CompletedTask;
        }

        private async Task ResumeGetChoice(IDialogContext context, IAwaitable<string> result)
        {
            string prompt_answer = await result;
            if (prompt_answer == PromptYes)
            {
                string question = "Let's play. Make your choice!";
                string retry = string.Format("Please choose only {0}, {1} or {2}", RockPaperScissors.Rock, RockPaperScissors.Paper, RockPaperScissors.Scissors);

                PromptDialog.Choice(
                    context: context,
                    prompt: question,
                    resume: ResumeGetResult,
                    options: RockPaperScissorService.GameOptions,
                    promptStyle: PromptStyle.Auto,
                    retry: retry
                );
            }
            else
            {
                await context.PostAsync("Alright, we can just make small talk.");
            }
        }

        private async Task ResumeGetResult(IDialogContext context, IAwaitable<string> result)
        {
            string player_choice = await result;
            string bot_choice = RockPaperScissorService.GetBotChoice();

            GameResult game_result = RockPaperScissorService.PlayerWinLoseOrTie(player_choice, bot_choice);

            string reply = string.Format("You chose {0} and I chose {1}\n\n", player_choice, bot_choice);
            switch(game_result)
            {
                case GameResult.Win:
                    reply += "You win!.";
                    break;
                case GameResult.Lose:
                    reply += "I win!.";
                    break;
                case GameResult.Tie:
                    reply += "It's a tie!";
                    break;
                default:
                    reply += "result undefined. >.<";
                    break;
            }
            await context.PostAsync(reply);
            context.Done(this);
        }
    }
}
