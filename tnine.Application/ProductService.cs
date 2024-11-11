using AutoMapper;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.IImageService.Dto;
using tnine.Application.Shared.IProductService;
using tnine.Application.Shared.IProductService.Dto;
using tnine.Application.Shared.IProductVariationDto.Dto;
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
            product.IsDeleted = false;
            var productId = await _productRepo.InsertAndGetIdAsync(product);
            if (input.ImgUrl != null)
            {
                foreach (var item in input.ImgUrl)
                {
                    await CreateImage(item, productId);
                }
            }

            if (input.ProductVariation != null)
            {
                foreach (var item in input.ProductVariation)
                {
                    await CreateProductVariation(item, productId);
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

            if (input.ProductVariation != null)
            {
                foreach (var item in input.ProductVariation)
                {
                    await CreateProductVariation(item, product.Id);
                }
            }
        }

        protected async Task CreateProductVariation(CreateOrEditProductVariaionDto input, long productId)
        {
            var isExist = await _productVariationsRepo.FirstOrDefaultAsync(e => e.ProductId == productId && e.ColorId == input.ColorId && e.SizeId == input.SizeId);
            if (isExist != null)
            {
                isExist.Quantity = input.Quantity;
                isExist.SizeId = input.SizeId;
                isExist.ColorId = input.ColorId;
                await _productVariationsRepo.UpdateAsync(isExist);
                return;
            }
            else
            {
                var productVariation = _mapper.Map<ProductVariations>(input);
                productVariation.ProductId = productId;
                await _productVariationsRepo.InsertAsync(productVariation);
            }

        }

        public async Task CreateImage(CreateOrEditImageDto input, long productId)
        {
            if (input.Id == null)
            {
                string wwwRootPath = GetWwwRootPath();

                var url = Path.Combine(Guid.NewGuid().ToString() + ".jpg");

                var imagePath = Path.Combine(wwwRootPath, url);

                if (!Directory.Exists(Path.GetDirectoryName(imagePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
                }

                var base64Data = input.ImgUrl.Split(',')[1];
                var imageBytes = Convert.FromBase64String(base64Data);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await fileStream.WriteAsync(imageBytes, 0, imageBytes.Length);
                }
                var isMain = _imageRepo.FirstOrDefaultAsync(e => e.ProductId == productId && e.IsMain == true);
                if (isMain != null)
                {
                    var image = new Images
                    {
                        ImgUrl = url,
                        ProductId = productId,
                        IsMain = true
                    };
                    await _imageRepo.InsertAsync(image);
                }
                else
                {
                    var image = new Images
                    {
                        ImgUrl = url,
                        ProductId = productId
                    };
                    await _imageRepo.InsertAsync(image);
                }

            }
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

        public string GetWwwRootPath()
        {

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string wwwRootPath = Path.Combine(baseDirectory, "wwwroot");

            return wwwRootPath;
        }
    }
}
