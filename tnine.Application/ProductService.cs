using AutoMapper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.ICatagoryService.Dto;
using tnine.Application.Shared.IProductService;
using tnine.Application.Shared.IProductService.Dto;
using tnine.Core;
using tnine.Core.Shared.Dto;
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

        public async Task CreateOrEdit(CreateOrEditProductAndImageDto input)
        {
            if (input.Product.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Edit(input);
            }
        }

        public async Task Create(CreateOrEditProductAndImageDto input)
        {
            var product = _mapper.Map<Product>(input.Product);
            var productId = await _productRepo.InsertAndGetIdAsync(product);
            if (input.ImgUrl != null)
            {
                foreach (var item in input.ImgUrl)
                {
                    await CreateImage(item, productId);
                }
            }
        }

        public async Task Edit(CreateOrEditProductAndImageDto input)
        {
            var product = await _productRepo.FirstOrDefaultAsync(e => e.Id == input.Product.Id);
            _mapper.Map(input.Product, product);
            await _productRepo.UpdateAsync(product);
            if (input.ImgUrl != null)
            {
                foreach (var item in input.ImgUrl)
                {
                    await CreateImage(item, product.Id);
                }
            }
        }


        public async Task CreateImage(string url, long productId)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var ImgUrl = Path.Combine(path, url);
            var image = new Images
            {
                ImgUrl = ImgUrl,
                ProductId = productId
            };
            await _imageRepo.InsertAsync(image);
        }

        public async Task Delete(long Id)
        {
            await _productRepo.DeleteAsync(Id);
        }

        public async Task<PagedResultDto<GetProductForViewDto>> GetAll()
        {
            var product = await _productRepo.GetAllAsync();
            var category = await _categoreisRepo.GetAllAsync();

            var query = from p in product
                        join c in category on p.CategoryId equals c.Id
                        select new GetProductForViewDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            Price = p.Price,
                            CategoryName = c.Name
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

        public async Task<List<GetCategoryForViewDto>> GetListCategories()
        {
            var categories = await _categoreisRepo.GetAllAsync();
            return categories.Select(o => new GetCategoryForViewDto
            {
                Id = o.Id,
                Name = o.Name
            }).ToList();

        }
    }
}
