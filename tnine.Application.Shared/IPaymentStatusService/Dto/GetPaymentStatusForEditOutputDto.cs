using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnine.Application.Shared.ITodoService.Dto;

namespace tnine.Application.Shared.IPaymentSatusService.Dto
{
    public class GetPaymentStatusForEditOutputDto
    {
        public CreateOrEditPaymentStatusDto PaymentStatus { get; set; }
    }
}
