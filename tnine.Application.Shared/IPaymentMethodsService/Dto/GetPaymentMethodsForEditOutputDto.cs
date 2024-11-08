using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnine.Application.Shared.IPaymentMethodsService.Dto;

namespace tnine.Application.Shared.IPaymentMethodsService.Dto
{
    public class GetPaymentMethodsForEditOutputDto
    {
        public CreateOrEditPaymentMethodsDto PaymentMethods { get; set; }
    }
}
