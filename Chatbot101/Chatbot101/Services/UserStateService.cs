using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatbot101.Services
{
    public class UserStateService
    {
        const string ChatBot101BotUserProperty = "chatbot101_botuser";

        public async Task<BotUserData> GetAsync(Activity activity)
        {
            using (StateClient state_client = activity.GetStateClient())
            {
                IBotState bot_state = state_client.BotState;
                BotData bot_data = await bot_state.GetUserDataAsync(activity.ChannelId, activity.From.Id);
                return bot_data.GetProperty<BotUserData>(ChatBot101BotUserProperty);
            }
        }

        public async Task UpdateAsync(Activity activity, BotUserData botuserdata)
        {
            using (StateClient state_client = activity.GetStateClient())
            {
                IBotState bot_state = state_client.BotState;
                BotData bot_data = await bot_state.GetUserDataAsync(activity.ChannelId, activity.From.Id);
                BotUserData bot_user_data = bot_data.GetProperty<BotUserData>(ChatBot101BotUserProperty);
                if (bot_user_data == null)
                {
                    bot_data.SetProperty(ChatBot101BotUserProperty, data: botuserdata);
                    await bot_state.SetUserDataAsync(activity.ChannelId, activity.From.Id, bot_data);
                }
            }
        }

        public async Task DeleteAsync(Activity activity)
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
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

}
