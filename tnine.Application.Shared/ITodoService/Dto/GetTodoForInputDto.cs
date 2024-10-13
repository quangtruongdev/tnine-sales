using tnine.Core.Shared.Dto;

namespace tnine.Application.Shared.ITodoService.Dto
{
    public class GetTodoForInputDto : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }
    }
}
