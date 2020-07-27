using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCore.Security.Permissions;
using Rework.ContentApproval.Drivers;
using Rework.ContentApproval.Handlers;
using Rework.ContentApproval.Indexes;
using Rework.ContentApproval.Models;
using YesSql.Indexes;

namespace Rework.ContentApproval
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddContentPart<ContentApprovalPart>()
                .UseDisplayDriver<ContentApprovalPartDisplayDriver>();
            services.AddScoped<IDataMigration, Migrations>();
            services.AddSingleton<IIndexProvider, ContentApprovalPartIndexProvider>();
            services.AddScoped<IPermissionProvider, Permissions>();
        }
    }

    [RequireFeatures("OrchardCore.Workflows")]
    public class StartupWithWorkflows : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddContentPart<ContentApprovalPart>()
                .AddHandler<ContentApprovalPartHandler>();
        }
    }
}