using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tnine.Core.Auditing;

namespace tnine.Core
{
    [Table("Customers")]
    public class Customer : FullAuditedEntity<long>
    {
        [StringLength(256)]
        public string FullName { get; set; }
        [StringLength(256)]
        public string Address { get; set; }
        [StringLength(50)]
        public string PhoneNumber { get; set; }
    }
}
