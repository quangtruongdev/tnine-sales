using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tnine.Core.Auditing;

namespace tnine.Core
{
    [Table("Colors")]
    public class Colors : FullAuditedEntity<long>
    {
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(50)]
        public string HexCode { get; set; }
    }
}
