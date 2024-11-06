using System;
using System.ComponentModel.DataAnnotations;

namespace tnine.Core.Auditing
{
    public abstract class FullAuditedEntity<TKey> where TKey : struct
    {
        [Key]
        public virtual TKey Id { get; set; }
        public virtual DateTime? CreationTime { get; set; }
        public virtual TKey? CreatorId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual TKey? LastModifierId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual TKey? DeleterId { get; set; }
        public virtual bool? IsDeleted { get; set; }
    }
}
