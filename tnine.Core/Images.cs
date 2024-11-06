using System.ComponentModel.DataAnnotations.Schema;
using tnine.Core.Auditing;

namespace tnine.Core
{
    [Table("Images")]
    public class Images : FullAuditedEntity<long>
    {
        public string ImgUrl { get; set; }
        public long ProductId { get; set; }
    }
}
