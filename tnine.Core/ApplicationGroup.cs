using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tnine.Core.Auditing;

namespace tnine.Core
{
    [Table("ApplicationGroups")]
    public class ApplicationGroup : FullAuditedEntity<long>
    {
        [StringLength(128)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
    }
}
