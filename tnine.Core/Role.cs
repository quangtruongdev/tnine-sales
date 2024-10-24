using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tnine.Core
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public long Id { get; set; }
        [StringLength(256)]
        public string Name { get; set; }
    }
}
