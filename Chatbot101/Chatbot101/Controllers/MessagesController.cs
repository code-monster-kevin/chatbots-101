using Chatbot101.Forms;
using Chatbot101.Loggers;
using Chatbot101.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chatbot101.Controllers
{
    [Route("api/[controller]")]
    [BotAuthentication]
    public class MessagesController : Controller
    {
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(ILogger<MessagesController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            _logger.LogInformation(LoggerHelper.FormatActivity(activity));

            if (activity?.Type == ActivityTypes.Message)
            {
                try
                {
                    await Conversation.SendAsync(activity, BuildLeaveFormDialog);
                }
                catch (FormCanceledException ex)
                {
                    HandleCanceledForm(activity, ex);
                }
            }
            else
            {
                await HandleSystemMessage(activity);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private async Task<Activity> HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
                await new UserStateService().DeleteUserDataAsync(message);
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }

        IDialog<LeaveForm> BuildLeaveFormDialog()
        {
            return FormDialog.FromForm(new LeaveForm().BuildForm);
        }

        void HandleCanceledForm(Activity activity, FormCanceledException ex)
        {
            string responseMessage =  $"Your conversation ended on { ex.Last}. " +
                "The following properties have values: " +
                string.Join(", ", ex.Completed);

            var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            var response = activity.CreateReply(responseMessage);
            connector.Conversations.ReplyToActivity(response);
        }
    }
}