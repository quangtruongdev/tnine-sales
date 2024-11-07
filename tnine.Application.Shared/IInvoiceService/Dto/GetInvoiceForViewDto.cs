using System;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IInvoiceService.Dto
{
    public class GetInvoiceForViewDto : EntityDto<long>
    {
        public DateTime CreationTime { get; set; }
        public string CustomerName { get; set; }
        public string CustomerTelephone { get; set; }
        public string PaymentStatusName { get; set; }
        public string PaymentMethodName { get; set; }
        public decimal Total { get; set; }
    }
}
