using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using tnine.Application.Shared.ICategoryService;
using tnine.Application.Shared.IColorService;
using tnine.Application.Shared.IProductService;
using tnine.Application.Shared.IProductService.Dto;
using tnine.Application.Shared.ISizeService;
using tnine.Core;
using tnine.Core.Shared.Dtos;
using tnine.Web.Host.Models;

namespace tnine.Web.Host.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IColorService _colorService;
        private readonly ISizeService _sizeService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper, ISizeService sizeService, IColorService colorService, ICategoryService categoryService)
        {
            _productService = productService;
            _mapper = mapper;
            _colorService = colorService;
            _sizeService = sizeService;
            _categoryService = categoryService;
        }

        //public async Task<ActionResult> Index(int? categoryId = null, int pageIndex = 1, int pageSize = 8, string sortOrder = null)
        //{
        //    // Lấy danh mục để đổ vào ViewBag
        //    var categories = await _categoryService.GetAll();
        //    ViewBag.Categories = categories.Select(c => new SelectListItem
        //    {
        //        Value = c.Id.ToString(),
        //        Text = c.Name,
        //        Selected = categoryId.HasValue && c.Id == categoryId
        //    }).ToList();

        //    // Lấy danh sách sản phẩm và áp dụng bộ lọc danh mục
        //    var pagedResult = await _productService.GetAll();
        //    var products = pagedResult.Results.AsQueryable();
        //    if (categoryId.HasValue)
        //    {
        //        products = products.Where(p => p.CategoryId == categoryId);
        //    }

        //    // Sắp xếp sản phẩm theo giá
        //    if (!string.IsNullOrEmpty(sortOrder))
        //    {
        //        if (sortOrder.ToLower() == "asc")
        //        {
        //            products = products.OrderBy(p => p.Price);
        //        }
        //        else if (sortOrder.ToLower() == "desc")
        //        {
        //            products = products.OrderByDescending(p => p.Price);
        //        }
        //    }

        //    // Tính tổng số sản phẩm và số trang
        //    var totalCount = products.Count();
        //    var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        //    // Đảm bảo pageIndex nằm trong phạm vi hợp lệ
        //    pageIndex = pageIndex < 1 ? 1 : (pageIndex > totalPages ? totalPages : pageIndex);

        //    // Phân trang
        //    var pagedItems = products
        //        .Skip((pageIndex - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList();

        //    // Chuyển đổi sang ViewModel
        //    var productViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(pagedItems);

        //    // Truyền dữ liệu cần thiết sang View bằng ViewBag
        //    ViewBag.TotalCount = totalCount;
        //    ViewBag.PageIndex = pageIndex;
        //    ViewBag.PageSize = pageSize;
        //    ViewBag.TotalPages = totalPages;
        //    ViewBag.SortOrder = sortOrder;
        //    ViewBag.SelectedCategoryId = categoryId;

        //    return View(productViewModels);
        //}

        public async Task<ActionResult> Index(string searchTerm = null, int? categoryId = null, int pageIndex = 1, int pageSize = 8, string sortOrder = null)
        {
            // Lấy danh mục để đổ vào ViewBag
            var categories = await _categoryService.GetAll();
            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = categoryId.HasValue && c.Id == categoryId
            }).ToList();

            // Lấy danh sách sản phẩm
            var pagedResult = await _productService.GetAll();
            var products = pagedResult.Results.AsQueryable();

            // Áp dụng tìm kiếm nếu có
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                products = products.Where(p => p.Name.ToLower().Contains(searchTerm));
            }

            // Áp dụng bộ lọc danh mục nếu có
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }

            // Sắp xếp sản phẩm theo giá
            if (!string.IsNullOrEmpty(sortOrder))
            {
                if (sortOrder.ToLower() == "asc")
                {
                    products = products.OrderBy(p => p.Price);
                }
                else if (sortOrder.ToLower() == "desc")
                {
                    products = products.OrderByDescending(p => p.Price);
                }
            }

            // Tính tổng số sản phẩm và số trang
            var totalCount = products.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            // Đảm bảo pageIndex nằm trong phạm vi hợp lệ
            pageIndex = pageIndex < 1 ? 1 : (pageIndex > totalPages ? totalPages : pageIndex);

            // Phân trang
            var pagedItems = products
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Chuyển đổi sang ViewModel
            var productViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(pagedItems);

            // Truyền dữ liệu cần thiết sang View bằng ViewBag
            ViewBag.TotalCount = totalCount;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.SortOrder = sortOrder;
            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.SearchTerm = searchTerm;

            return View(productViewModels);
        }


        //public async Task<ActionResult> Details(long id)
        //{
        //    var product = await _productService.GetDetailProduct(id);  
        //    var productVarition = await _productService.GetListProductVariationByProductId(id);

        //    var productDetailViewModel = new ProductDetailViewModel();
        //    productDetailViewModel.ProductViewModel = _mapper.Map<ProductViewModel>(product);
        //    productDetailViewModel.productVariationViewModels = _mapper.Map<List<ProductVariationViewModel>>(productVarition);


        //    return View(productDetailViewModel); // Trả về view chi tiết sản phẩm
        //}

        public async Task<ActionResult> Details(long id)
        {
            // Lấy chi tiết sản phẩm
            var product = await _productService.GetDetailProduct(id);
            if (product == null)
            {
                // Trả về trang lỗi nếu không tìm thấy sản phẩm
                return HttpNotFound("Sản phẩm không tồn tại.");
            }

            // Lấy danh sách biến thể sản phẩm
            var productVarition = await _productService.GetListProductVariationByProductId(id);

            // Tạo ViewModel chi tiết sản phẩm
            var productDetailViewModel = new ProductDetailViewModel
            {
                ProductViewModel = _mapper.Map<ProductViewModel>(product),
                productVariationViewModels = productVarition != null
                    ? _mapper.Map<List<ProductVariationViewModel>>(productVarition)
                    : new List<ProductVariationViewModel>()
            };

            // Trả về view chi tiết sản phẩm
            return View(productDetailViewModel);
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