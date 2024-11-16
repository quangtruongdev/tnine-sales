using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.IImageService.Dto;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IImageService
{
    public interface IIamgeService
    {
        Task<PagedResultDto<GetImageForViewDto>> GetImageByProductId(long productId);
        Task Delete(long Id);
        Task CreateOrEdit(List<CreateOrEditImageDto> input);
    }
}
