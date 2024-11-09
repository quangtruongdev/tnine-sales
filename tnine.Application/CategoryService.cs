using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.ICategoryService;
using tnine.Application.Shared.ICategoryService.Dto;
using tnine.Core;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoreisRepository _categoreisRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoreisRepository categoreisRepo, IMapper mapper)
        {
            _categoreisRepo = categoreisRepo;
            _mapper = mapper;
        }

        public async Task CreateOrEdit(CreateOrEditCategoryDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Edit(input);
            }
        }

        private async Task Create(CreateOrEditCategoryDto input)
        {
            var category = _mapper.Map<Categories>(input);

            await _categoreisRepo.InsertAsync(category);
        }

        private async Task Edit(CreateOrEditCategoryDto input)
        {
            var category = await _categoreisRepo.FirstOrDefaultAsync(o => o.Id == input.Id);

            _mapper.Map(input, category);

            await _categoreisRepo.UpdateAsync(category);
        }

        public async Task Delete(long id)
        {
            var category = await _categoreisRepo.FirstOrDefaultAsync(o => o.Id == id);
            await _categoreisRepo.DeleteAsync(category);
        }

        public async Task<List<GetCategoryForViewDto>> GetAll()
        {
            var categories = await _categoreisRepo.GetAllAsync();

            var query = from category in categories
                        select new
                        {
                            category.Id,
                            category.Name,
                            category.Description,
                        };

            return query.Select(x => new GetCategoryForViewDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();
        }

        public async Task<GetCategoryForEditDto> GetById(long id)
        {
            var category = await _categoreisRepo.FirstOrDefaultAsync(o => o.Id == id);

            var categoryDto = _mapper.Map<CreateOrEditCategoryDto>(category);

            return new GetCategoryForEditDto { Category = categoryDto };
        }
    }
}
