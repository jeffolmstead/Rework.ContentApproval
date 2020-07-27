using OrchardCore.Workflows.ViewModels;
using Rework.ContentApproval.Workflows.Activities;

namespace Rework.ContentApproval.Workflows.ViewModels
{
    public class ApprovalResponseEventViewModel : ActivityViewModel<ApprovalResponseEvent>
    {
        public ApprovalResponseEventViewModel()
        {
        }

        public ApprovalResponseEventViewModel(ApprovalResponseEvent activity) : base(activity)
        {
        }
    }
}
