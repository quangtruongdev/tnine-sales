namespace tnine.Application.Shared.IProductVariationService.Dto
{
    public class GetProductVariationForViewDto
    {
        public long ProductId { get; set; }
        public long ColorId { get; set; }
        public long SizeId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public string ColorHexCode { get; set; }
    }
}
