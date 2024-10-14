using System.Collections.Generic;
using tnine.Application.Shared.IProductService;
using tnine.Core;
using tnine.Core.Shared.Infrastructure;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }
    }
}
