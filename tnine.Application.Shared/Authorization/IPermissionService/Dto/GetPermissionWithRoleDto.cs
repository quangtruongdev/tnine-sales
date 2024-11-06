using System.Collections.Generic;

namespace tnine.Application.Shared.Authorization.IPermissionService.Dto
{
    public class GetPermissionWithRoleDto
    {
        public List<string> Roles { get; set; }
        public string PermissionName { get; set; }
    }
}
