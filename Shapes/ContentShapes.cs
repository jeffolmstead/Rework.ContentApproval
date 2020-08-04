using OrchardCore.ContentManagement;
using OrchardCore.DisplayManagement.Descriptors;
using Rework.ContentApproval.Common;
using Rework.ContentApproval.Models;

namespace Rework.ContentApproval.Shapes
{
    public class ContentShapes : IShapeTableProvider
    {
        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("Content_Edit")
                .OnDisplaying(context =>
                {
                    dynamic shape = context.Shape;
                    var contentItem = (ContentItem)shape.ContentItem;
                    if (contentItem != null && contentItem.Has<ContentApprovalPart>())
                    {
                        dynamic actions = shape.Actions;
                        actions.Remove("Content_PublishButton");

                        var contentApprovalPart = contentItem.As<ContentApprovalPart>();
                        if (contentApprovalPart.Status == Settings.ContentApprovalRequested)
                        {
                            actions.Remove("Content_SaveDraftButton");
                        }
                    }
                });
        }
    }
}
