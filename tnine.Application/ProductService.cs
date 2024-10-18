using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.IProductService;
using tnine.Core;
using tnine.Core.Shared.Infrastructure;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepo, IUnitOfWork unitOfWork)
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Product>> GetAll()
        {
            var products = await _productRepo.GetAllAsync();
            return products;
        }

        //public Task<List<PagedResultDto<GetProductForViewDto>>> GetAll(GetProductInputDto input)
        //{
        //    var products = from p in _productRepo.GetAll()
        //                   .Where(e => e.Name == input.FilterText)
        //                   select new GetProductForViewDto
        //                   {
        //                       Name = p.Name,
        //                       Description = p.Description,
        //                       Price = p.Price,
        //                   };

        //    var productList = products.ToList();
        //    var totalCount = productList.Count;

        //    var pagedResult = new PagedResultDto<GetProductForViewDto>
        //    {
        //        Items = productList,
        //        TotalCount = totalCount
        //    };

        //    return Task.FromResult(new List<PagedResultDto<GetProductForViewDto>> { pagedResult });
        //}
    }
}
