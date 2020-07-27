using Microsoft.Extensions.DependencyInjection;
using Rework.ContentApproval.Workflows.Activities;
using Rework.ContentApproval.Workflows.Drivers;
using OrchardCore.Workflows.Helpers;
using OrchardCore.Modules;

namespace Rework.ContentApproval.Workflows
{
    [RequireFeatures("OrchardCore.Workflows")]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddActivity<ApprovalResponseEvent, ApprovalResponseEventDisplay>();
            services.AddActivity<ApprovalRequestEvent, ApprovalRequestEventDisplay>();
        }
    }
}
