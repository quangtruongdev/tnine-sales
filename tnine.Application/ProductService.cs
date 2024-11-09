using AutoMapper;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.IImageService.Dto;
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

            foreach (var color in input.ColorIds)
            {
                foreach (var size in input.SizeIds)
                {
                    var productVariations = new ProductVariations
                    {
                        ProductId = productId,
                        ColorId = color,
                        SizeId = size,
                        Quantity = 0
                    };
                    await _productVariationsRepo.InsertAsync(productVariations);
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

        public async Task CreateImage(CreateOrEditImageDto input, long productId)
        {
            string wwwRootPath = GetWwwRootPath();

            // Kiểm tra nếu ImgUrl là chuỗi Base64
            if (input.ImgUrl.StartsWith("data:image/"))
            {
                var imagePath = Path.Combine(wwwRootPath, "Image", Guid.NewGuid().ToString() + ".jpg");

                if (!Directory.Exists(Path.GetDirectoryName(imagePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
                }

                // Tách phần dữ liệu Base64
                var base64Data = input.ImgUrl.Split(',')[1];  // Tách chuỗi từ phần 'data:image/jpg;base64,...'
                var imageBytes = Convert.FromBase64String(base64Data);

                // Lưu ảnh vào tệp
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await fileStream.WriteAsync(imageBytes, 0, imageBytes.Length);
                }

                // Thêm hoặc cập nhật ảnh vào cơ sở dữ liệu
                if (input.Id == null)
                {
                    var image = new Images
                    {
                        ImgUrl = imagePath,
                        ProductId = productId
                    };
                    await _imageRepo.InsertAsync(image);
                }
                else
                {
                    var image = await _imageRepo.FirstOrDefaultAsync(e => e.Id == input.Id);
                    if (image != null)
                    {
                        image.ImgUrl = imagePath;
                        await _imageRepo.UpdateAsync(image);
                    }
                }
            }
            else
            {
                // Trường hợp khác nếu ImgUrl không phải là Base64 (ví dụ đường dẫn URL đến hình ảnh)
                var imagePath = Path.Combine(wwwRootPath, "Image", input.ImgUrl);

                if (!Directory.Exists(Path.GetDirectoryName(imagePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
                }

                // Thêm hoặc cập nhật ảnh vào cơ sở dữ liệu
                if (input.Id == null)
                {
                    var image = new Images
                    {
                        ImgUrl = imagePath,
                        ProductId = productId
                    };
                    await _imageRepo.InsertAsync(image);
                }
                else
                {
                    var image = await _imageRepo.FirstOrDefaultAsync(e => e.Id == input.Id);
                    if (image != null)
                    {
                        image.ImgUrl = imagePath;
                        await _imageRepo.UpdateAsync(image);
                    }
                }
            }

        }

        private bool IsBase64String(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length % 4 != 0)
                return false;
            return input.All(c => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".Contains(c));
        }

        public async Task Delete(long Id)
        {
            await _productRepo.DeleteAsync(Id);
        }

        public async Task<PagedResultDto<GetProductForViewDto>> GetAll()
        {
            var product = await _productRepo.GetAllAsync();
            var category = await _categoreisRepo.GetAllAsync();
            var image = await _imageRepo.GetAllAsync();

            var query = from p in product
                        join c in category on p.CategoryId equals c.Id
                        join i in image on p.Id equals i.ProductId
                        where i.IsMain == true
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
