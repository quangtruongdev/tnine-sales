using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.IProductVariationDto.Dto;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IProductVariationDto
{
    public interface IProductVariationService
    {
        Task<PagedResultDto<GetProductVariationForViewDto>> GetProductVariationByProductId(GetProductVariationInputDto input);
        Task CreateOrEdit(List<CreateOrEditProductVariaionDto> input);
        Task Delete(CreateOrEditProductVariaionDto input);
        Task<List<GetProductVariationForEditDto>> GetProductVariationById(long productId);
    }
}
