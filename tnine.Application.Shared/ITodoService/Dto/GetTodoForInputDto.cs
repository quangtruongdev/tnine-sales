using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.ITodoService.Dto
{
    public class GetTodoForInputDto : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }
    }
}
