using System.Threading.Tasks;
using tnine.Application.Shared.IApplicationUserService.Dto;
using tnine.Core.Shared.Dto;

namespace tnine.Application.Shared.IApplicationUserService
{
    public interface IApplicationUserService
    {
        Task<GetApplicationUserForViewDto> GetUserInfo(EntityDto<long> input);
        //Task<GetUserWithRoleForViewDto> GetUserWithRole(EntityDto<long> input);
    }
}
