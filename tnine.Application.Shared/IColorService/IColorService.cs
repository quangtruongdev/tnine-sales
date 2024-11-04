using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Core.Shared.IColorService.Dto;

namespace tnine.Application.Shared.IColorService
{
    public interface IColorService
    {
        Task<List<GetColorForViewDto>> GetAll();
        Task CreateOrEdit(CreateOrEditColorDto input);
        Task Delete(long id);
        Task<GetColorForEditOutputDto> GetById(long id);
    }
}
