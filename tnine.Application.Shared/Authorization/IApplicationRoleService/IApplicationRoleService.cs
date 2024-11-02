using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.IApplicationRoleService.Dto;
using tnine.Core.Shared.Dto;

namespace tnine.Application.Shared.IApplicationRoleService
{
    public interface IApplicationRoleService
    {
        Task<List<GetRoleForViewDto>> GetAll();
        Task CreateOrEdit(CreateOrEditRoleDto input);
        Task<GetRoleForEditOutputDto> GetById(EntityDto<long> input);
        Task Delete(EntityDto<long> input);
    }
}
