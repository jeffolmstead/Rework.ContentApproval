using OrchardCore.Workflows.ViewModels;
using Rework.ContentApproval.Workflows.Activities;

namespace Rework.ContentApproval.Workflows.ViewModels
{
    public class ApprovalRequestEventViewModel : ActivityViewModel<ApprovalRequestEvent>
    {
        public ApprovalRequestEventViewModel()
        {
        }

        public ApprovalRequestEventViewModel(ApprovalRequestEvent activity) : base(activity)
        {
        }
    }
}
