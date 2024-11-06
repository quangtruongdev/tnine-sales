using System.Threading.Tasks;
using tnine.Application.Shared.IProductVariationDto.Dto;
using tnine.Core.Shared.Dto;

namespace tnine.Application.Shared.IProductVariationDto
{
    public interface IProductVariationServiece
    {
        Task<PagedResultDto<GetProductVariationForViewDto>> GetProductVariationByProductId(GetProductVariationInputDto input);
        Task CreateOrEdit(CreateOrEditProductVariayionDto input);
        Task Delete(long productId, long colorId, long sizeId);
    }
}
