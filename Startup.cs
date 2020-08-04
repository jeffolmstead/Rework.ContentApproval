using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.DisplayManagement.Descriptors;
using OrchardCore.Modules;
using OrchardCore.Security.Permissions;
using Rework.ContentApproval.Drivers;
using Rework.ContentApproval.Handlers;
using Rework.ContentApproval.Indexes;
using Rework.ContentApproval.Models;
using Rework.ContentApproval.Shapes;
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
            services.AddScoped<IShapeTableProvider, ContentShapes>();
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