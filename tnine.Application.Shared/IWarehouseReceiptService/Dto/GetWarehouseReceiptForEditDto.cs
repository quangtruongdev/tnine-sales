namespace tnine.Application.Shared.IWarehouseReceiptService.Dto
{
    public class GetWarehouseReceiptForEditDto
    {
        public string SupplierName { get; set; }
        public string ProductName { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
