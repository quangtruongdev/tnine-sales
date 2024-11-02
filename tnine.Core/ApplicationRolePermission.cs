using System.ComponentModel.DataAnnotations.Schema;

namespace tnine.Core
{
    [Table("ApplicationRolePermissions")]
    public class ApplicationRolePermission
    {
        public long RoleId { get; set; }
        public long PermissionId { get; set; }
    }
}
