using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using tnine.Application.Shared.IColorService;
using tnine.Application.Shared.IProductService;
using tnine.Application.Shared.ISizeService;
using tnine.Web.Host.Models;

namespace tnine.Web.Host.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IColorService _colorService;
        private readonly ISizeService _sizeService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper, ISizeService sizeService, IColorService colorService)
        {
            _productService = productService;
            _mapper = mapper;
            _colorService = colorService;
            _sizeService = sizeService;
        }

        public async Task<ActionResult> Index()
        {
            var pagedResult = await _productService.GetAll();
            var products = pagedResult.Results;
            var productViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return View(productViewModels);
        }

        public async Task<ActionResult> Details(long id)
        {
            // Lấy thông tin sản phẩm theo id
            var product = await _productService.GetDetailProduct(id);  
            var color = await _colorService.GetAll();
            var listColor = color.ToList();
            var size = await _sizeService.GetAll();
            var listSize = size.ToList();

            //if (product == null)
            //{
            //    return HttpNotFound(); // Trả về lỗi 404 nếu không tìm thấy sản phẩm
            //}



            // Map từ sản phẩm sang ProductViewModel
            //var productDetailViewModel = _mapper.Map<ProductDetailViewModel>(products);
            var productDetailViewModel = new ProductDetailViewModel();
            productDetailViewModel.ProductViewModel = _mapper.Map<ProductViewModel>(product);
            //productDetailViewModel.ProductViewModel.Id = (long)product.Id;
            //productDetailViewModel.ProductViewModel.Name = product.Product.Name;
            //productDetailViewModel.ProductViewModel.CategoryId = product.Product.CategoryId;
            //productDetailViewModel.ProductViewModel.CategoryName = product.Product.CategoryName;
            //productDetailViewModel.ProductViewModel.Description = product.Product.Description;
            //productDetailViewModel.ProductViewModel.Price = product.Product.Price;
            //productDetailViewModel.ProductViewModel.ImgUrl = product.Product.ImgUrl;

            productDetailViewModel.colorViewModels = _mapper.Map<List<ColorViewModel>>(listColor);
            productDetailViewModel.sizeViewModels = _mapper.Map<List<SizeViewModel>>(listSize);


            return View(productDetailViewModel); // Trả về view chi tiết sản phẩm
        }
    }
}