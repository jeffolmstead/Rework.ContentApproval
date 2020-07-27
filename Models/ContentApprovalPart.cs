using OrchardCore.ContentManagement;
using Rework.ContentApproval.Common;

namespace Rework.ContentApproval.Models
{
    public class ContentApprovalPart : ContentPart
    {
        public string Status { get; set; }
        public string Notes { get; set; }
        public string NotificationNeeded { get; set; }
    }
}
