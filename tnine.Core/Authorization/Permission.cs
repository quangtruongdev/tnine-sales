using System.ComponentModel.DataAnnotations;
using tnine.Core.Auditing;

namespace tnine.Core
{
    public class Permission : FullAuditedEntity<long>
    {
        [StringLength(128)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public long ParentId { get; set; }
    }
}
