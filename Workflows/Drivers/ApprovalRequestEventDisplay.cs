using OrchardCore.DisplayManagement.Views;
using OrchardCore.Workflows.Display;
using Rework.ContentApproval.Workflows.Activities;
using Rework.ContentApproval.Workflows.ViewModels;

namespace Rework.ContentApproval.Workflows.Drivers
{
    public class ApprovalRequestEventDisplay : ActivityDisplayDriver<ApprovalRequestEvent, ApprovalRequestEventViewModel>
    {
        public override IDisplayResult Display(ApprovalRequestEvent activity)
        {
            return Combine(
                Shape("ApprovalRequestEvent_Fields_Thumbnail", new ApprovalRequestEventViewModel(activity)).Location("Thumbnail", "Content"),
                Factory("ApprovalRequestEvent_Fields_Design", ctx =>
                {
                    var shape = new ApprovalRequestEventViewModel
                    {
                        Activity = activity
                    };
                    return shape;
                }).Location("Design", "Content")
            );
        }
    }
}
