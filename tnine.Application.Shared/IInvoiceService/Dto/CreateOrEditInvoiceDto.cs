using System;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IInvoiceService.Dto
{
    public class CreateOrEditInvoiceDto : EntityDto<long>
    {
        public DateTime? CreationTime { get; set; }
        public decimal Total { get; set; }
        public long? CustomerId { get; set; }
        public long? PaymentStatusId { get; set; }
        public long? PaymentMethodId { get; set; }
        //public IEnumerable<long> ProductIds { get; set; }
        public string PaymentStatusName { get; set; }
        public string PaymentMethodName { get; set; }
    }
}
