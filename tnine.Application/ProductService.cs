using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository productRepo,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

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
