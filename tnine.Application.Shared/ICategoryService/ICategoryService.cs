using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.ICategoryService.Dto;

namespace tnine.Application.Shared.ICategoryService
{
    public interface ICategoryService
    {
        Task<List<GetCategoryForViewDto>> GetAll();
    }
}
