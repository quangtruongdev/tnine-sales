using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using tnine.Application.Shared.IColorService;
using tnine.Application.Shared.IProductService;
using tnine.Application.Shared.IProductService.Dto;
using tnine.Application.Shared.ISizeService;
using tnine.Core.Shared.Dtos;
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

        public async Task<ActionResult> Index(int pageIndex = 1, int pageSize = 8)
        {

            var pagedResult = await _productService.GetAll();
            var totalCount = pagedResult.TotalCount;
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > totalPages ? totalPages : pageIndex;
            var pagedItems = pagedResult.Results
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var productViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(pagedItems);
            ViewBag.TotalCount = totalCount;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;

            return View(productViewModels);
        }




        public async Task<ActionResult> Details(long id)
        {
            var product = await _productService.GetDetailProduct(id);  
            //var color = await _colorService.GetAll();
            //var listColor = color.ToList();
            //var size = await _sizeService.GetAll();
            //var listSize = size.ToList();
            var productVarition = await _productService.GetListProductVariationByProductId(id);
            //if (product == null)
            //{
            //    return HttpNotFound(); // Trả về lỗi 404 nếu không tìm thấy sản phẩm
            //}



            // Map từ sản phẩm sang ProductViewModel
            //var productDetailViewModel = _mapper.Map<ProductDetailViewModel>(products);
            var productDetailViewModel = new ProductDetailViewModel();
            productDetailViewModel.ProductViewModel = _mapper.Map<ProductViewModel>(product);
            productDetailViewModel.productVariationViewModels = _mapper.Map<List<ProductVariationViewModel>>(productVarition);
            //productDetailViewModel.ProductViewModel.Id = (long)product.Id;
            //productDetailViewModel.ProductViewModel.Name = product.Product.Name;
            //productDetailViewModel.ProductViewModel.CategoryId = product.Product.CategoryId;
            //productDetailViewModel.ProductViewModel.CategoryName = product.Product.CategoryName;
            //productDetailViewModel.ProductViewModel.Description = product.Product.Description;
            //productDetailViewModel.ProductViewModel.Price = product.Product.Price;
            //productDetailViewModel.ProductViewModel.ImgUrl = product.Product.ImgUrl;

            //productDetailViewModel.colorViewModels = _mapper.Map<List<ColorViewModel>>(listColor);
            //productDetailViewModel.sizeViewModels = _mapper.Map<List<SizeViewModel>>(listSize);


            return View(productDetailViewModel); // Trả về view chi tiết sản phẩm
        }
        public async Task<ActionResult> Search(string searchTerm, int pageIndex = 1, int pageSize = 8)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return RedirectToAction("Index");
            }
            var searchResults = await _productService.SearchProductByNameAsync(searchTerm, pageIndex, pageSize);
            ViewBag.TotalPages = (int)Math.Ceiling((double)searchResults.TotalCount / pageSize);

            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > ViewBag.TotalPages ? ViewBag.TotalPages : pageIndex;

  
            var productViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(searchResults.Results);
            ViewBag.SearchTerm = searchTerm;
            ViewBag.TotalCount = searchResults.TotalCount;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;

            return View("Index", productViewModels);
        }

        public ActionResult Cart()
        {
            return View();
        }
    }
}