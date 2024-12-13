using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IProductVariationService.Dto
{
    public class GetProductVariationInputDto : PagedAndSortedResultRequestDto
    {
        public long ProductId { get; set; }
    }
}
