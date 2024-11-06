using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IProductService.Dto
{
    public class GetProductInputDto : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }
    }
}
