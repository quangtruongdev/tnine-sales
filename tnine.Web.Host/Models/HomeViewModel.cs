using System.Collections.Generic;

namespace tnine.Web.Host.Models
{
    public class HomeViewModel
    {
    }

    public class ColorViewModel
    {
        public long Id { get; set; }
        //public string Name { get; set; }
        public string HexCode { get; set; }
    }

    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public long CategoryId { get; set; }
        public string ImgUrl { get; set; }
    }

    public class ProductDetailViewModel
    {
        public ProductViewModel ProductViewModel { get; set; }
        public List<ProductVariationViewModel> productVariationViewModels { get; set; }
    }

    public class ProductVariationViewModel
    {
        public long ProductId { get; set; }
        public long ColorId { get; set; }
        public long SizeId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
    }

    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}