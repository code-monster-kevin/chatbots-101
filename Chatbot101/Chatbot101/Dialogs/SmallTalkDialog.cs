using Chatbot101.Services;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace Chatbot101.Dialogs
{
    [Serializable]
    public class SmallTalkDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            BotUserData bot_user_data = await new UserStateService().GetAsync(activity);

            string reply = SmallTalkResponse(activity.Text);

            if (bot_user_data == null)
            {
                BotUserData new_bot_user_data = new BotUserData
                {
                    // TODO
                    Id = Guid.NewGuid(),
                    FullName = "Code Green",
                    Email = "codegreen@botframework.com"
                };
                await new UserStateService().UpdateAsync(activity, new_bot_user_data);
            }
            else
            {
                reply = "Hi " + bot_user_data.FullName + ", " + reply;
            }
            
            await context.PostAsync(reply);
            context.Wait(MessageReceivedAsync);
        }

        private string SmallTalkResponse(string user_text)
        {
            string bot_reply = String.Empty;

            switch(IntentDetectionService.CheckIntent(user_text))
            {
                case IntentDetectionService.IntentState.ABOUT_ME:
                    bot_reply = SmallTalkService.RandomSmallTalk(SmallTalkService.About_Me_Sentences);
                    break;
                case IntentDetectionService.IntentState.BYE_BYE:
                    bot_reply = SmallTalkService.RandomSmallTalk(SmallTalkService.Bye_Sentences);
                    break;
                case IntentDetectionService.IntentState.HELLO:
                    bot_reply = SmallTalkService.RandomSmallTalk(SmallTalkService.Hello_Sentences);
                    break;
                case IntentDetectionService.IntentState.SMILE:
                    bot_reply = SmallTalkService.RandomSmallTalk(SmallTalkService.Smile_Textmojis_Sentences);
                    break;
                default:
                    bot_reply = SmallTalkService.RandomSmallTalk(SmallTalkService.No_Comprende_Sentences);
                    break;
            }

            return bot_reply;
        }
    }
}
