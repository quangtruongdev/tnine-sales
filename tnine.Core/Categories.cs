using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tnine.Core.Auditing;

namespace tnine.Core
{
    [Table("Categories")]
    public class Categories : FullAuditedEntity<long>
    {
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
