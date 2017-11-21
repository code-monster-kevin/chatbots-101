using Chatbot101.Services;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace Chatbot101.Dialogs
{
    [Serializable]
    public class IntroduceYourselfDialog : IDialog<object>
    {
        string user_name;
        string user_email;

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hi, I'm Small Talk bot, and I don't believe we've met...\n\nWhat do you want me to call you?");
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result;

            user_name = activity.Text;
            PromptDialog.Text(
                context: context,
                resume: ResumeGetEmail,
                prompt: "What is your email address?",
                retry: "Can you share with me your email address?"
            );
        }

        public virtual async Task ResumeGetEmail(IDialogContext context, IAwaitable<string> response)
        {
            user_email = await response;
            BotUserData bot_user_data = new BotUserData
            {
                FullName = user_name,
                Email = user_email
            };
            new UserStateService().SetBotUserData(context, bot_user_data);

            string reply = @"Hi {0}, Thanks for sharing your email {1} with me.\n\nI feel more comfortable talking to you now.";
            await context.PostAsync(String.Format(reply, user_name, user_email));

            context.Done(this);
        }
    }
}
