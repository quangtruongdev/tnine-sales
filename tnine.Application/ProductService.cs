using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.IProductService;
using tnine.Application.Shared.IProductService.Dto;
using tnine.Core;
using tnine.Core.Shared.Dtos;
using tnine.Core.Shared.Infrastructure;
using tnine.Core.Shared.Repositories;


namespace tnine.Application
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly IColorRepository _colorRepo;
        private readonly ISizeRepository _sizeRepo;
        private readonly IImageRepository _imageRepo;
        private readonly IProductVariationsRepository _productVariationsRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICategoreisRepository _categoreisRepo;

        public ProductService(
            IProductRepository productRepo,
            IColorRepository colorRepo,
            ISizeRepository sizeRepo,
            IImageRepository imageRepo,
            IProductVariationsRepository productVariationsRepo,
            ICategoreisRepository categoreisRepo,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _productRepo = productRepo;
            _colorRepo = colorRepo;
            _sizeRepo = sizeRepo;
            _imageRepo = imageRepo;
            _productVariationsRepo = productVariationsRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _categoreisRepo = categoreisRepo;
        }

        public async Task<long> CreateOrEdit(CreateOrEditProductDto input)
        {
            if (input.Id == null)
            {
                return await Create(input);
            }
            else
            {
                return await Edit(input);
            }
        }

        protected async Task<long> Create(CreateOrEditProductDto input)
        {
            var product = _mapper.Map<Product>(input);
            product.IsDeleted = false;
            var productId = await _productRepo.InsertAndGetIdAsync(product);
            return productId;

        }

        protected async Task<long> Edit(CreateOrEditProductDto input)
        {
            var product = await _productRepo.FirstOrDefaultAsync(e => e.Id == input.Id);
            _mapper.Map(input, product);
            await _productRepo.UpdateAsync(product);
            return product.Id;

        }

        public async Task Delete(long Id)
        {
            var product = await _productRepo.FirstOrDefaultAsync(e => e.Id == Id);
            product.IsDeleted = true;
            await _productRepo.UpdateAsync(product);
        }

        public async Task<PagedResultDto<GetProductForViewDto>> GetAll()
        {
            var product = await _productRepo.GetAllAsync();
            var category = await _categoreisRepo.GetAllAsync();
            var image = await _imageRepo.GetAllAsync();

            var query = from p in product
                        join c in category on p.CategoryId equals c.Id
                        join i in image on p.Id equals i.ProductId
                        where i.IsMain == true && p.IsDeleted == false
                        select new GetProductForViewDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            Price = p.Price,
                            CategoryName = c.Name,
                            ImgUrl = i == null ? "" : i.ImgUrl,
                        };

            var totalCount = query.Count();
            var items = query.Skip(0).Take(10).ToList();
            return new PagedResultDto<GetProductForViewDto>(totalCount, items);
        }

        public async Task<GetProductForEditDto> GetProductForEdit(long Id)
        {
            var product = await _productRepo.FirstOrDefaultAsync(e => e.Id == Id);

            var productDto = _mapper.Map<CreateOrEditProductDto>(product);
            return new GetProductForEditDto { Product = productDto };
        }

        public async Task<PagedResultDto<GetProductForViewDto>> GetProductByCategoryId(long CateGoryId)
        {
            var product = await _productRepo.GetAllAsync();
            var category = await _categoreisRepo.GetAllAsync();

            var query = from p in product
                        join c in category on p.CategoryId equals c.Id
                        where c.Id == CateGoryId
                        select new GetProductForViewDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            Price = p.Price,
                            CategoryName = c.Name,
                            CategoryId = c.Id
                        };

            var totalCount = query.Count();
            var items = query.Skip(0).Take(10).ToList();
            return new PagedResultDto<GetProductForViewDto>(totalCount, items);
        }
    }
}
