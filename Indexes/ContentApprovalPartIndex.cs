using OrchardCore.ContentManagement;
using Rework.ContentApproval.Models;
using YesSql.Indexes;

namespace Rework.ContentApproval.Indexes
{
    public class ContentApprovalPartIndex : MapIndex
    {
        public string Status { get; set; }
    }

    public class ContentApprovalPartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<ContentApprovalPartIndex>()
                .Map(contentItem =>
                {
                    var contentApprovalPart = contentItem.As<ContentApprovalPart>();
                    if (contentApprovalPart == null)
                    {
                        return null;
                    }

                    return new ContentApprovalPartIndex
                    {
                        Status = contentApprovalPart.Status ?? string.Empty
                    };
                });
        }
    }
}
