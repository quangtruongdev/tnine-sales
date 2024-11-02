using System.ComponentModel.DataAnnotations.Schema;
using tnine.Core.Auditing;

namespace tnine.Core
{
    [Table("ApplicationUserGroups")]
    public class ApplicationUserGroup : FullAuditedEntity<long>
    {
        public long UserId { get; set; }
        public long GroupId { get; set; }
    }
}
