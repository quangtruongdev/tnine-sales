using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.ICategoryService;
using tnine.Application.Shared.ICategoryService.Dto;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoreisRepository _categoreisRepo;

        public CategoryService(ICategoreisRepository categoreisRepo)
        {
            _categoreisRepo = categoreisRepo;
        }

        public async Task<List<GetCategoryForViewDto>> GetAll()
        {
            var query = await _categoreisRepo.GetAllAsync();

            return query.Select(x => new GetCategoryForViewDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();
        }
    }
}
