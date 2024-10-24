using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.ITodo;
using tnine.Application.Shared.ITodoService.Dto;
using tnine.Core;
using tnine.Core.Shared.Dto;
using tnine.Core.Shared.Infrastructure;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TodoService(
            ITodoRepository todoRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _todoRepository = todoRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetTodoForViewDto>> GetAll()
        {
            var todos = await _todoRepository.GetAllAsync();

            return todos.Select(t => new GetTodoForViewDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description
            }).ToList();
        }

        //public async Task<PagedResultDto<GetTodoForViewDto>> GetAll(GetTodoForInputDto input)
        //{
        //    var todos = from todo in _todoRepository.GetAll()
        //                select new GetTodoForViewDto
        //                {
        //                    Id = todo.Id,
        //                    Title = todo.Title,
        //                    Description = todo.Description
        //                };

        //    var totalCount = todos.Count();
        //    var items = todos.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

        //    return new PagedResultDto<GetTodoForViewDto>
        //    {
        //        TotalCount = totalCount,
        //        //Results = items
        //    };
        //}

        public async Task CreateOrEdit(CreateOrEditTodoDto input)
        {
            if (input.Id.HasValue)
            {
                await Edit(input);
            }
            else
            {
                await Create(input);
            }
        }

        private async Task Create(CreateOrEditTodoDto input)
        {
            var todo = _mapper.Map<Todo>(input);
            await _todoRepository.InsertAsync(todo);
        }

        private async Task Edit(CreateOrEditTodoDto input)
        {
            var todo = await _todoRepository.GetSingleByIdAsync(input.Id.Value);
            _mapper.Map(input, todo);
            await _todoRepository.UpdateAsync(todo);
        }

        public async Task<GetTodoForEditOutputDto> GetById(EntityDto<long> input)
        {
            var todo = await _todoRepository.GetSingleByIdAsync(input.Id.Value);
            var output = new GetTodoForEditOutputDto
            {
                Todo = _mapper.Map<CreateOrEditTodoDto>(todo)
            };
            return output;
        }

        public async Task Delete(EntityDto<long> input)
        {
            await _todoRepository.DeleteAsync(input.Id.Value);
        }
    }
}