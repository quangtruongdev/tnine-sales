using System.ComponentModel.DataAnnotations.Schema;
using tnine.Core.Auditing;

namespace tnine.Core
{
    [Table("ProductVariations")]
    public class ProductVariations : FullAuditedEntity<long>
    {
        public long ProductId { get; set; }
        public long ColorId { get; set; }
        public long SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
