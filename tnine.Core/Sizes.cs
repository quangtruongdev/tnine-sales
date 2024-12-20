﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tnine.Core.Auditing;

namespace tnine.Core
{
    [Table("Sizes")]
    public class Sizes : FullAuditedEntity<long>
    {
        [MaxLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
