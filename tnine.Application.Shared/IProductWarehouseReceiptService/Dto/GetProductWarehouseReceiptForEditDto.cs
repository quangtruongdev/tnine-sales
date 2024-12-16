namespace tnine.Application.Shared.IProductWarehouseReceiptService.Dto
{
    public class GetProductWarehouseReceiptForEditDto
    {
        public long WarehouseReceiptId { get; set; }
        public long ProductId { get; set; }
        public long ColorId { get; set; }
        public long SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
