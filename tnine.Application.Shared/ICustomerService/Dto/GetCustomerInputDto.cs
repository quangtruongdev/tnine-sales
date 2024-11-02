using tnine.Core.Shared.Dto;

namespace tnine.Application.Shared.ICustomerService.Dto
{
    public class GetCustomerInputDto : PagedAndSortedResultRequestDto
    {
         public string FilterText { get; set; }
    }
}
