using AutoMapper;
using tnine.Application.Shared.IUserService;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _applicationUserRepository;
        private readonly IUserRoleRepository _applicationUserRoleRepository;
        private readonly IRoleRepository _applicationRoleRepository;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository applicationUserRepository,
            IUserRoleRepository applicationUserRoleRepository,
            IRoleRepository applicationRoleRepository,
            IMapper mapper
            )
        {
            _applicationUserRepository = applicationUserRepository;
            _applicationUserRoleRepository = applicationUserRoleRepository;
            _applicationRoleRepository = applicationRoleRepository;
            _mapper = mapper;
        }
    }
}
