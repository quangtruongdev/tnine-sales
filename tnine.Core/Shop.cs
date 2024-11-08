using System.ComponentModel.DataAnnotations.Schema;
using tnine.Core.Auditing;

namespace tnine.Core
{
    [Table("Shops")]
    public class Shop : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
