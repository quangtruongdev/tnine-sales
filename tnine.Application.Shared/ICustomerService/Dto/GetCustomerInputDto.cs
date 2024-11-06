using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.ICustomerService.Dto
{
    public class GetCustomerInputDto : PagedAndSortedResultRequestDto
    {
         public string FilterText { get; set; }
    }
}
