using System.ComponentModel.DataAnnotations;

namespace tnine.Core
{
    public class Product
    {
        [Key]
        public long Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
