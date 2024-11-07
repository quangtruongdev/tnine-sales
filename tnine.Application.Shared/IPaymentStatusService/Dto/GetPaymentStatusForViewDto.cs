using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IPaymentSatusService.Dto
{
    public class GetPaymentStatusForViewDto: EntityDto<long>
    {
        public string Name { get; set; }
    }
}
