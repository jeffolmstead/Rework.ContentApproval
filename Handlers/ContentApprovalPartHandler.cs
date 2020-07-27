using OrchardCore.ContentManagement.Handlers;
using OrchardCore.Workflows.Services;
using Rework.ContentApproval.Models;
using System.Threading.Tasks;

namespace Rework.ContentApproval.Handlers
{
    public class ContentApprovalPartHandler : ContentPartHandler<ContentApprovalPart>
    {
        private readonly IWorkflowManager _workflowManager;

        public ContentApprovalPartHandler(IWorkflowManager workflowManager)
        {
            _workflowManager = workflowManager;
        }

        public override Task CreatedAsync(CreateContentContext context, ContentApprovalPart part)
        {
            return HandleNotification(part);
        }

        public override Task UpdatedAsync(UpdateContentContext context, ContentApprovalPart part)
        {
            return HandleNotification(part);
        }

        private Task HandleNotification(ContentApprovalPart part)
        {
            return !string.IsNullOrWhiteSpace(part.NotificationNeeded)
                ? _workflowManager.TriggerEventAsync(part.NotificationNeeded,
                    input: new { ContentItem = part.ContentItem },
                    correlationId: part.ContentItem.ContentItemId
                )
                : Task.CompletedTask;
        }
    }
}
