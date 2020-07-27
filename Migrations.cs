using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using Rework.ContentApproval.Common;
using Rework.ContentApproval.Indexes;
using Rework.ContentApproval.Models;

namespace Rework.ContentApproval
{
    public class Migrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public Migrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinition(nameof(ContentApprovalPart), builder => builder
                .Attachable()
                .WithDescription($"Adds the ability for users with '{Settings.RequestContentApprovalPermission}' permission to initiate a request for approval."));

            SchemaBuilder.CreateMapIndexTable(nameof(ContentApprovalPartIndex), table => table
                .Column<string>(nameof(ContentApprovalPartIndex.Status))
            );

            SchemaBuilder.AlterTable(nameof(ContentApprovalPartIndex), table => table
                .CreateIndex(
                    $"IDX_{nameof(ContentApprovalPartIndex)}_{nameof(ContentApprovalPartIndex.Status)}",
                    nameof(ContentApprovalPartIndex.Status))
            );

            return 1;
        }
    }
}
