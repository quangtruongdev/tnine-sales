using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.ITodoService.Dto;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.ITodo
{
    public interface ITodoService
    {
        Task<List<GetTodoForViewDto>> GetAll();
        //Task<PagedResultDto<GetTodoForViewDto>> GetAll(GetTodoForInputDto input);
        Task CreateOrEdit(CreateOrEditTodoDto input);
        Task<GetTodoForEditOutputDto> GetById(EntityDto<long> input);
        Task Delete(EntityDto<long> input);
    }
}
