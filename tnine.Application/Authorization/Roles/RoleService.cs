using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.IRoleService;
using tnine.Application.Shared.IRoleService.Dto;
using tnine.Core;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _applicationRoleRepo;
        private IMapper _mapper;

        public RoleService(
            IRoleRepository applicationRoleRepo,
            IMapper mapper
            )
        {
            _applicationRoleRepo = applicationRoleRepo;
            _mapper = mapper;
        }

        public async Task<List<GetRoleForViewDto>> GetAll()
        {
            var roles = await _applicationRoleRepo.GetAllAsync();
            return roles.Select(r => new GetRoleForViewDto
            {
                Id = r.RoleId,
                Name = r.RoleName,
            }).ToList();
        }

        public async Task CreateOrEdit(CreateOrEditRoleDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Edit(input);
            }
        }

        private async Task Create(CreateOrEditRoleDto input)
        {
            var role = _mapper.Map<ApplicationRole>(input);
            await _applicationRoleRepo.InsertAsync(role);
        }

        private async Task Edit(CreateOrEditRoleDto input)
        {
            var role = await _applicationRoleRepo.GetSingleByIdAsync(input.Id.Value);
            _mapper.Map(input, role);
            await _applicationRoleRepo.UpdateAsync(role);
        }

        public async Task<GetRoleForEditOutputDto> GetById(long id)
        {
            var role = await _applicationRoleRepo.GetSingleByIdAsync(id);
            var output = new GetRoleForEditOutputDto
            {
                Role = _mapper.Map<CreateOrEditRoleDto>(role)
            };
            return output;
        }

        public async Task Delete(long id)
        {
            await _applicationRoleRepo.DeleteAsync(id);
        }
    }
}
