using System.ComponentModel.DataAnnotations.Schema;
using tnine.Core.Auditing;

namespace tnine.Core
{
    [Table("RolePermissions")]
    public class RolePermission : FullAuditedEntity<long>
    {
        public long RoleId { get; set; }
        public ApplicationRole Role { get; set; }
        public long PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
