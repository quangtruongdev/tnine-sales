using tnine.Core.Auditing;

namespace tnine.Core
{
    public class PaymentStatus : FullAuditedEntity<long>
    {
        public string Name { get; set; }
    }
}
