using System.Threading.Tasks;
using tnine.Application.Shared.IImageService.Dto;
using tnine.Core.Shared.Dto;

namespace tnine.Application.Shared.IImageService
{
    public interface IIamgeService
    {
        Task<PagedResultDto<GetImageForViewDto>> GetImageByProductId(GetImageInputDto input);
        Task Delete(long Id);
    }
}
