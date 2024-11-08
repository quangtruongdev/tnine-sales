using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IShopService.Dto
{
    public class GetShopForInputDto : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }
    }
}
