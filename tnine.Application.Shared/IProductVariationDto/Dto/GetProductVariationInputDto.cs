using tnine.Core.Shared.Dto;

namespace tnine.Application.Shared.IProductVariationDto.Dto
{
    public class GetProductVariationInputDto : PagedAndSortedResultRequestDto
    {
        public long ProductId { get; set; }
    }
}
