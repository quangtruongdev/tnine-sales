using System.ComponentModel.DataAnnotations.Schema;

namespace tnine.Core
{
    [Table("ProductVariations")]
    public class ProductVariations
    {
        public long ProductId { get; set; }
        public long ColorId { get; set; }
        public long SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
