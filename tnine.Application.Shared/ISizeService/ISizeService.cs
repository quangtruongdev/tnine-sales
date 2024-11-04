using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.ISizeService.Dto;

namespace tnine.Application.Shared.ISizeService
{
    public interface ISizeService
    {
        Task<List<GetSizeForViewDto>> GetAll();
        Task CreateOrEdit(CreateOrEditSizeDto input);
        Task Delete(long id);
        Task<GetSizeForEditOutputDto> GetById(long id);
    }
}
