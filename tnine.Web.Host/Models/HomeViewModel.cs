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

    public class SizeViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class ProductDetailViewModel
    {
        public ProductViewModel ProductViewModel { get; set; }
        public List<ColorViewModel> colorViewModels { get; set; }
        public List<SizeViewModel> sizeViewModels { get; set; }
    }
}