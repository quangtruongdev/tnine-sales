using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IOrderService.Dto
{
    public class GetOrderInputDto : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }
    }
}
