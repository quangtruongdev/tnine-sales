using System.Threading.Tasks;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IImageService
{
    public interface IIamgeService
    {
        Task<PagedResultDto<GetImageForViewDto>> GetImageByProductId(long productId);
        Task Delete(long Id);
    }
}
