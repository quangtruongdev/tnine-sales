using System.Collections.Generic;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IWarehouseReceiptService.Dto
{
    public class CreateOrEditWarehouseReceiptDto : EntityDto<long>
    {
        public long SupplierId { get; set; }
        public List<ProductInWarehouseReceiptDto> ProductInWarehouseReceipts { get; set; }
    }
    public class ProductInWarehouseReceiptDto
    {
        public long ProductId { get; set; }
        public decimal Price { get; set; }
        public List<VariationProduct> VariationProducts { get; set; }
    }
    public class VariationProduct
    {
        public long ColorId { get; set; }
        public long SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
