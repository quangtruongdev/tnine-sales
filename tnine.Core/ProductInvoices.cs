using System.ComponentModel.DataAnnotations.Schema;
using tnine.Core.Auditing;

namespace tnine.Core
{
    [Table("ProductInvoices")]
    public class ProductInvoices : FullAuditedEntity<long>
    {
        public long ProductId { get; set; }
        public long InvoiceId { get; set; }
        public int Quantity { get; set; }
    }
}
