using System;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IWarehouseReceiptService.Dto
{
    public class GetWarehouseReceiptForViewDto : EntityDto<long>
    {
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public DateTime CreationTime { get; set; }
        public decimal Total { get; set; }
    }
}
