using tnine.Core.Shared.Dto;

namespace tnine.Application.Shared.IImageService.Dto
{
    public class GetImageInputDto : PagedAndSortedResultRequestDto
    {
        public long ProductId { get; set; }
    }

}
