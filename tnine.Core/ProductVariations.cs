using System;
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
        public virtual DateTime? CreationTime { get; set; }
        public virtual long? CreatorId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? LastModifierId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual long? DeleterId { get; set; }
        public virtual bool? IsDeleted { get; set; }
    }
}
