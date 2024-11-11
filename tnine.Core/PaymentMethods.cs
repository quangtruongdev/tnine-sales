using tnine.Core.Auditing;

namespace tnine.Core
{
    public class PaymentMethods : FullAuditedEntity<long>
    {
        public string Name { get; set; }
    }
}
