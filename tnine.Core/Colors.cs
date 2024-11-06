using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tnine.Core.Auditing;

namespace tnine.Core
{
    [Table("Colors")]
    public class Colors : FullAuditedEntity<long>
    {
        [MaxLength(10)]
        public string HexCode { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
    }
}
