using AutoMapper;
using System.Threading.Tasks;
using tnine.Application.Shared.IApplicationUserService;
using tnine.Application.Shared.IApplicationUserService.Dto;
using tnine.Core.Shared.Dto;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IApplicationUserRoleRepository _applicationUserRoleRepository;
        private readonly IApplicationRoleRepository _applicationRoleRepository;
        private readonly IMapper _mapper;

        public ApplicationUserService(
            IApplicationUserRepository applicationUserRepository,
            IApplicationUserRoleRepository applicationUserRoleRepository,
            IApplicationRoleRepository applicationRoleRepository,
            IMapper mapper
            )
        {
            _applicationUserRepository = applicationUserRepository;
            _applicationUserRoleRepository = applicationUserRoleRepository;
            _applicationRoleRepository = applicationRoleRepository;
            _mapper = mapper;
        }

        public async Task<GetApplicationUserForViewDto> GetUserInfo(EntityDto<long> input)
        {
            var user = await _applicationUserRepository.GetSingleByIdAsync(input.Id.Value);
            return _mapper.Map<GetApplicationUserForViewDto>(user);
        }

        //public async Task<GetUserWithRoleForViewDto> GetUserWithRole(EntityDto<long> input)
        //{
        //    var user = await _applicationUserRepository.GetSingleByIdAsync(input.Id.Value);
        //    var userRole = await _applicationUserRoleRepository.GetAllAsync();
        //    var role = await _applicationRoleRepository.GetAllAsync();

        //    var userWithRole = from u in userRole
        //                       join r in role on u.RoleId equals r.Id
        //                       where u.UserId == user.Id
        //                       select new GetUserWithRoleForViewDto
        //                       {
        //                           UserName = user.UserName,
        //                           RoleName = r.Name
        //                       };
        //    return userWithRole;
        //}
    }
}
