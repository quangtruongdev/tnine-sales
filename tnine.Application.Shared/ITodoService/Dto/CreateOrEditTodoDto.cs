using tnine.Core.Shared.Dto;

namespace tnine.Application.Shared.ITodoService.Dto
{
    public class CreateOrEditTodoDto : EntityDto<long>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
