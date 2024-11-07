using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IInvoiceService.Dto
{
    public class GetInvoiceInputDto : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }
    }
}
