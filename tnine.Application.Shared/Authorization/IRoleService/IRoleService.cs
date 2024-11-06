using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.IRoleService.Dto;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IRoleService
{
    public interface IRoleService
    {
        Task<List<GetRoleForViewDto>> GetAll();
        Task CreateOrEdit(CreateOrEditRoleDto input);
        Task<GetRoleForEditOutputDto> GetById(EntityDto<long> input);
        Task Delete(EntityDto<long> input);
    }
}
