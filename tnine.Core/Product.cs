using System.ComponentModel.DataAnnotations;
using tnine.Core.Auditing;

namespace tnine.Core
{
    public class Product : FullAuditedEntity<long>
    {
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
