using tnine.Core.Auditing;

namespace tnine.Core
{
    public class Customer : FullAuditedEntity<long>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
