using System.Collections.Generic;

namespace tnine.Application.Shared.Authorization.IPermissionService.Dto
{
    public class GetPermissionWithRoleDto
    {
        public List<long> RoleIds { get; set; }
        public string PermissionName { get; set; }
    }
}
