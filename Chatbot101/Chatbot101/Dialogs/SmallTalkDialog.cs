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

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            BotUserData bot_user_data = new UserStateService().GetBotUserData(context);
            if (bot_user_data == null)
            {
                context.Call<object>(new IntroduceYourselfDialog(), ResumeAfterIntroducingYourselfDialog);
            }
            else
            {
                string reply = bot_user_data.FullName + ", ";
                reply += SmallTalkResponse(message.Text);
                await context.PostAsync(reply);
                context.Wait(MessageReceivedAsync);
            }
        }

        public virtual async Task ResumeAfterIntroducingYourselfDialog(IDialogContext context, IAwaitable<object> response)
        {
            string reply = SmallTalkResponse("hi");
            await context.PostAsync(reply);
            context.Done(this);
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
