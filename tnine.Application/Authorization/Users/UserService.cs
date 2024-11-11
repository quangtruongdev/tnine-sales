using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using tnine.Application.Shared.Authorization.IUserService;
using tnine.Application.Shared.Authorization.IUserService.Dto;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IUserRoleRepository _userRoleRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepo,
            IUserRoleRepository userRoleRepo,
            IRoleRepository roleRepo,
            IMapper mapper
            )
        {
            _userRepo = userRepo;
            _userRoleRepo = userRoleRepo;
            _roleRepo = roleRepo;
            _mapper = mapper;
        }

        public async Task<List<GetUserForViewDto>> GetAll(GetUserForInputDto input)
        {
            var users = await _userRepo.GetAllAsync();
            var userDtos = _mapper.Map<List<GetUserForViewDto>>(users);
            return userDtos;
        }
    }
}
