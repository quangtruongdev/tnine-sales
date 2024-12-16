using System.Collections.Generic;

namespace tnine.Application.Shared.IProductWarehouseReceiptService.Dto
{
    public class CreateOrEditProductWarehouseReceiptDto
    {
        public long WarehouseReceiptId { get; set; }
        public long SupplierId { get; set; }
        public List<ProductInWarehouseReceiptDto> ProductInWarehouseReceipts { get; set; }
    }
    public class ProductInWarehouseReceiptDto
    {
        public long ProductId { get; set; }
        public List<VariationProduct> VariationProducts { get; set; }
    }
    public class VariationProduct
    {
        public long ColorId { get; set; }
        public long SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
