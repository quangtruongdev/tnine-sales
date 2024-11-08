using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnine.Application.Shared.IShopService.Dto;

namespace tnine.Application.Shared.IPaymentStatusService.Dto
{
    public class GetPaymentStatusForEditOutputDto
    {
        public CreateOrEditPaymentStatusDto PaymentStatus { get; set; }
    }
}
