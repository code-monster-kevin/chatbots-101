using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Threading.Tasks;

namespace Chatbot101.Forms
{
    [Serializable]
    [Template(TemplateUsage.NotUnderstood,
        "Sorry, I didn’t get that.",
        "Please try again.",
        "My apologies, I didn’t understand '{0}'.",
        "Excuse me, I didn’t quite get that.",
        "Sorry, but I’m a chatbot and don’t know what '{0}' means.")]
    public class LeaveForm
    {
        [Describe(Description = "Your name")]
        [Prompt("Please tell me your name (current value: {})")]
        public string Name { get; set; }

        [Describe(Description = "Your email")]
        [Prompt("Please tell me your email (current value: {})")]
        [Pattern(@".+@.+\..+")]
        public string Email { get; set; }

        [Describe(Description = "The type of leave you want to apply")]
        [Prompt("What type of leave do you want to apply? (current value: {})  {||}")]
        public LeaveType LeaveType { get; set; }

        [Describe(Description = "The leave date starting from")]
        [Prompt("What date will your leave start? (current value: {})")]
        public DateTime LeaveFrom { get; set; }

        [Describe(Description = "The leave ending date")]
        [Prompt("What date will your leave end? (current value: {})")]
        public DateTime LeaveTo { get; set; }

        [Optional]
        [Numeric(1, 5)]
        [Describe(Description = "Rate the leave application service")]
        [Prompt("Please rate how much you like our leave application service\n\n (Choose 1 to 5) (current value: {})")]
        public int? Rating { get; set; }  // when select Optional, the value type must be nullable

        public IForm<LeaveForm> BuildForm()
        {
            return new FormBuilder<LeaveForm>()
                .Message("May I ask a few questions?\n\n You can type 'help' at any time for more info.")
                .OnCompletion(SaveLeaveFormAsync)
                .Build();
        }

        async Task SaveLeaveFormAsync(IDialogContext context, LeaveForm leave_form)
        {
            string reply = "You leave is being processed.";
            await context.PostAsync(reply);
        }
    }

    public enum LeaveType
    {
        None,
        AnnualLeave,
        MedicalLeave,
        TrainingLeave
    }
}
