using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnine.Core.Auditing;

namespace tnine.Core
{
    public class Orders : FullAuditedEntity<long>
    {
        public decimal Total { get; set; }
        public long CustomerId { get; set; }
    }
}
