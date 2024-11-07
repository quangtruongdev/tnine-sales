using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnine.Core.Auditing;

namespace tnine.Core
{
    public class PaymentStatus: FullAuditedEntity<long>
    {
        public String Name { get; set; }
    }
}
