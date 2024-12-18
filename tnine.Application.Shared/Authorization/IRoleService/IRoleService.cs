using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.IRoleService.Dto;

namespace tnine.Application.Shared.IRoleService
{
    public interface IRoleService
    {
        Task<List<GetRoleForViewDto>> GetAll();
        Task CreateOrEdit(CreateOrEditRoleDto input);
        Task<GetRoleForEditOutputDto> GetById(long id);
        Task Delete(long id);
    }
}
