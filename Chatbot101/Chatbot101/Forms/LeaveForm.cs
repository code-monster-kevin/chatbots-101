using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Threading.Tasks;

namespace Chatbot101.Forms
{
    [Serializable]
    public class LeaveForm
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public LeaveType LeaveType { get; set; }
        public DateTime LeaveFrom { get; set; }
        public DateTime LeaveTo { get; set; }

        public IForm<LeaveForm> BuildForm()
        {
            return new FormBuilder<LeaveForm>()
                .Message("May I ask a few questions?\n\n You can type 'help' at any time for more info.")
                .OnCompletion(SaveLeaveFormAsync)
                .Build();
        }

        async Task SaveLeaveFormAsync(IDialogContext context, LeaveForm leave_form)
        {
            //string reply = String.Format("You applied for leave starting on {0} until {1}.", leave_form.LeaveFrom.ToString("dd-MMM-yyyy"), leave_form.LeaveTo.ToString("dd-MMM-yyyy"));
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
