using tnine.Core.Shared.Dto;

namespace tnine.Application.Shared.IProductService.Dto
{
    public class GetProductInputDto : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }
    }
}
