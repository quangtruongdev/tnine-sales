using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.IProductService;
using tnine.Application.Shared.IProductService.Dto;
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

        //public async Task<PagedResultDto<GetProductForViewDto>> GetAll(GetProductInputDto input)
        //{
        //    var products = from product in _productRepo.GetAll()
        //                   select new GetProductForViewDto
        //                   {
        //                       Id = product.Id,
        //                       Name = product.Name,
        //                       Price = product.Price
        //                   };

        //    var totalCount = products.Count();
        //    var results = products;

        //    return new PagedResultDto<GetProductForViewDto>(totalCount, results);
        //}

        public async Task<List<GetProductForViewDto>> GetAll()
        {
            var products = await _productRepo.GetAllAsync();
            return products.Select(p => new GetProductForViewDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();

        }
    }
}
