using OrchardCore.DisplayManagement.Views;
using OrchardCore.Workflows.Display;
using Rework.ContentApproval.Workflows.Activities;
using Rework.ContentApproval.Workflows.ViewModels;

namespace Rework.ContentApproval.Workflows.Drivers
{
    public class ApprovalResponseEventDisplay : ActivityDisplayDriver<ApprovalResponseEvent, ApprovalResponseEventViewModel>
    {
        public override IDisplayResult Display(ApprovalResponseEvent activity)
        {
            // Return a shape rather than a view
            return Combine(
                Shape("ApprovalResponseEvent_Fields_Thumbnail", new ApprovalResponseEventViewModel(activity)).Location("Thumbnail", "Content"),
                Factory("ApprovalResponseEvent_Fields_Design", ctx =>
                {
                    var shape = new ApprovalResponseEventViewModel
                    {
                        Activity = activity
                    };
                    return shape;
                }).Location("Design", "Content")
            );
        }
    }
}
