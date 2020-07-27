using OrchardCore.Security.Permissions;
using Rework.ContentApproval.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rework.ContentApproval
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission RequestApproval = new Permission("RequestApproval", Settings.RequestContentApprovalPermission);

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            // Not defaulting any role to this permission
            return Enumerable.Empty<PermissionStereotype>();
        }

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[] { RequestApproval }.AsEnumerable());
        }
    }
}
