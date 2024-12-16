using System.ComponentModel.DataAnnotations.Schema;
using tnine.Core.Auditing;

namespace tnine.Core
{
    [Table("Invoices")]
    public class Invoice : FullAuditedEntity<long>
    {
        public decimal Total { get; set; }
        public long? CustomerId { get; set; }
        public long PaymentStatusId { get; set; }
        public long PaymentMethodId { get; set; }
    }
}
