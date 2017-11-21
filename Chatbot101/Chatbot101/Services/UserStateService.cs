using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading.Tasks;

namespace Chatbot101.Services
{
    public class UserStateService
    {
        const string ChatBot101BotUserProperty = "chatbot101_botuser";

        public BotUserData GetBotUserData(IDialogContext context)
        {
            BotUserData bot_data = null;
            context.UserData.TryGetValue<BotUserData>(ChatBot101BotUserProperty, out bot_data);

            return bot_data;
        }

        public void SetBotUserData(IDialogContext context, BotUserData botuserdata)
        {
            context.UserData.SetValue<BotUserData>(ChatBot101BotUserProperty, botuserdata);
        }

        public async Task DeleteUserDataAsync(Activity activity)
        {
            using (StateClient state_client = activity.GetStateClient())
            {
                IBotState bot_state = state_client.BotState;
                await bot_state.DeleteStateForUserAsync(activity.ChannelId, activity.From.Id);
            }
        }
    }

    public class BotUserData
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }

}
