using System;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IOrderService.Dto
{
    public class GetOrderForViewDto : EntityDto<long>
    {
        public DateTime CreationTime { get; set; }
        public string CustomerName { get; set; }
        public string CustomerTelephone { get; set; }
        public decimal Total { get; set; }
    }
}
