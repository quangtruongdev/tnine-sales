using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.IProductVariationService.Dto;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IProductVariationService
{
    public interface IProductVariationService
    {
        Task<PagedResultDto<GetProductVariationForViewDto>> GetProductVariationByProductId(GetProductVariationInputDto input);
        Task CreateOrEdit(List<CreateOrEditProductVariaionDto> input);
        Task Delete(long productId, long colorId, long sizeId);
        Task<List<GetProductVariationForEditDto>> GetProductVariationById(long productId);
    }
}
