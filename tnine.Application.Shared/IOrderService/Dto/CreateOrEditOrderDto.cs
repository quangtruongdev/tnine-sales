using System;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IOrderService.Dto
{
    public class CreateOrEditOrderDto : EntityDto<long>
    {
        public DateTime CreationTime { get; set; }
        public decimal Total { get; set; }
        public long CustomerId { get; set; }
    }
}
