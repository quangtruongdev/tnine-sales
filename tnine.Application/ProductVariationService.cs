using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.IProductVariationDto;
using tnine.Application.Shared.IProductVariationDto.Dto;
using tnine.Core;
using tnine.Core.Shared.Dto;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class ProductVariationService : IProductVariationServiece
    {

        private readonly IProductVariationsRepository _productVariationRepository;
        private readonly IMapper _mapper;
        private readonly IColorRepository _colorRepository;
        private readonly ISizeRepository _sizeRepository;

        public ProductVariationService(IProductVariationsRepository productVariationRepository, IMapper mapper, IColorRepository colorRepository, ISizeRepository sizeRepository)
        {
            _productVariationRepository = productVariationRepository;
            _mapper = mapper;
            _colorRepository = colorRepository;
            _sizeRepository = sizeRepository;
        }


        public async Task CreateOrEdit(CreateOrEditProductVariayionDto input)
        {
            var productVariation = await _productVariationRepository.FirstOrDefaultAsync(x => x.ProductId == input.ProductId && x.ColorId == input.ColorId && x.SizeId == input.SizeId);

            if (productVariation == null)
            {
                await Create(input);
            }
            else
            {
                await Edit(input);
            }
        }

        public async Task Delete(long productId, long colorId, long sizeId)
        {

            var variation = await _productVariationRepository.FirstOrDefaultAsync(x => x.ProductId == productId && x.ColorId == colorId && x.SizeId == sizeId);
            await _productVariationRepository.DeleteAsync(variation);
        }

        public async Task<PagedResultDto<GetProductVariationForViewDto>> GetProductVariationByProductId(GetProductVariationInputDto input)
        {
            var variations = await _productVariationRepository.GetAllAsync();
            var color = await _colorRepository.GetAllAsync();
            var size = await _sizeRepository.GetAllAsync();
            var query = from variation in variations
                        join c in color on variation.ColorId equals c.Id
                        join s in size on variation.SizeId equals s.Id
                        where variation.ProductId == input.ProductId
                        select new GetProductVariationForViewDto
                        {
                            ProductId = variation.ProductId,
                            ColorId = variation.ColorId,
                            SizeId = variation.SizeId,
                            Quantity = variation.Quantity,
                            ColorName = c.Code,
                            SizeName = s.Name
                        };
            var totalCount = query.Count();
            var result = query.Skip(input.SkipCount).Take(input.MaxResultCount);
            return new PagedResultDto<GetProductVariationForViewDto>(totalCount, result.ToList());
        }

        public async Task Create(CreateOrEditProductVariayionDto input)
        {
            var productVariation = _mapper.Map<ProductVariations>(input);
            await _productVariationRepository.InsertAsync(productVariation);
        }

        public async Task Edit(CreateOrEditProductVariayionDto input)
        {
            var productVariation = await _productVariationRepository.FirstOrDefaultAsync(x => x.ProductId == input.ProductId && x.ColorId == input.ColorId && x.SizeId == input.SizeId);
            _mapper.Map(input, productVariation);
        }
    }
}
