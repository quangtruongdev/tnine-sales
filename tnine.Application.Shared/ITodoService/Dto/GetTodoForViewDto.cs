using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.ITodoService.Dto
{
    public class GetTodoForViewDto : EntityDto<long>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
