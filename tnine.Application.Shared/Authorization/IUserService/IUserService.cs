using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.Authorization.IUserService.Dto;

namespace tnine.Application.Shared.Authorization.IUserService
{
    public interface IUserService
    {
        Task<List<GetUserForViewDto>> GetAll(GetUserForInputDto input);
    }
}
