using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace Chatbot101.Loggers
{
    public static class LoggerHelper
    {
        public static ActivityLog GetActivityLog(Activity activity)
        {
            return new ActivityLog
            {
                Id = activity.Id,
                Type = activity.Type,
                Name = activity.Name,
                TopicName = activity.TopicName,
                TextFormat = activity.TextFormat,
                Action = activity.Action,
                ReplyToId = activity.ReplyToId,
                ServiceUrl = activity.ServiceUrl,
                ChannelId = activity.ChannelId,
                Locale = activity.Locale,
                FromChannelAccountId = activity.From.Id,
                FromChannelAccountName = activity.From.Name,
                RecipientChannelAccountId = activity.Recipient.Id,
                RecipientChannelAccountName = activity.Recipient.Name,
                ConversationId = activity.Conversation.Id,
                ConversationName = activity.Conversation.Name,
                ConversationIsGroup = activity.Conversation.IsGroup,
                Text = activity.Text,
                Speak = activity.Speak,
                InputHint = activity.InputHint,
                Summary = activity.Summary
            };
        }

        public static string FormatActivityLog(ActivityLog activity_log)
        {
            return JsonConvert.SerializeObject(activity_log);
        }

        public static string FormatActivity(Activity activity)
        {
            return FormatActivityLog(GetActivityLog(activity));
        }
    }

    public class ActivityLog
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string TopicName { get; set; }
        public string TextFormat { get; set; }
        public string Action { get; set; }
        public string ReplyToId { get; set; }
        public string ServiceUrl { get; set; }
        public string ChannelId { get; set; }
        public string Locale { get; set; }
        public string FromChannelAccountId { get; set; }
        public string FromChannelAccountName { get; set; }
        public string RecipientChannelAccountId { get; set; }
        public string RecipientChannelAccountName { get; set; }
        public string ConversationId { get; set; }
        public string ConversationName { get; set; }
        public bool? ConversationIsGroup { get; set; }
        public string Text { get; set; }
        public string Speak { get; set; }
        public string InputHint { get; set; }
        public string Summary { get; set; }
    }
}
