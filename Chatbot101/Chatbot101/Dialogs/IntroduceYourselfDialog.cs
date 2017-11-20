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
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            PromptDialog.Text(
                    context: context,
                    resume: ResumeGetName,
                    prompt: "Please tell me your name",
                    retry: "Can you share your name, please?"
                );
        }

        public virtual async Task ResumeGetName(IDialogContext context, IAwaitable<string> response)
        {
            user_name = await response;

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

            //BotUserData new_bot_user_data = new BotUserData
            //{
            //    Id = Guid.NewGuid(),
            //    FullName = user_name,
            //    Email = user_email
            //};
            //await new UserStateService().UpdateAsync(activity, new_bot_user_data);

            string reply = @"Hi {0}, Thanks for sharing your email {1} with me.\nI feel more comfortable talking to you now.";
            await context.PostAsync(String.Format(reply, user_name, user_email));

            context.Done(this);
        }
    }
}
