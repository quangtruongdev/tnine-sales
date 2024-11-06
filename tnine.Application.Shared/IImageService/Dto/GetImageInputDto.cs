using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IImageService.Dto
{
    public class GetImageInputDto : PagedAndSortedResultRequestDto
    {
        public long ProductId { get; set; }
    }

}
