using System.ComponentModel.DataAnnotations.Schema;
using tnine.Core.Auditing;

namespace tnine.Core
{
    [Table("WarehouseReceipt")]
    public class WarehouseReceipt : FullAuditedEntity<long>
    {
        public long SupplierId { get; set; }
        public decimal Total { get; set; }

    }
}
