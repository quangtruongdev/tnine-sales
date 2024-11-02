using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.IApplicationRoleService;
using tnine.Application.Shared.IApplicationRoleService.Dto;
using tnine.Core;
using tnine.Core.Shared.Dto;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class ApplicationRoleService : IApplicationRoleService
    {
        private readonly IApplicationRoleRepository _applicationRoleRepository;
        private IMapper _mapper;

        public ApplicationRoleService(
            IApplicationRoleRepository applicationRoleRepository,
            IMapper mapper
            )
        {
            _applicationRoleRepository = applicationRoleRepository;
            _mapper = mapper;
        }

        public async Task<List<GetRoleForViewDto>> GetAll()
        {
            var roles = await _applicationRoleRepository.GetAllAsync();
            return roles.Select(r => new GetRoleForViewDto
            {
                Id = r.RoleId,
                Name = r.RoleName
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
            await _applicationRoleRepository.InsertAsync(role);
        }

        private async Task Edit(CreateOrEditRoleDto input)
        {
            var role = await _applicationRoleRepository.GetSingleByIdAsync(input.Id.Value);
            _mapper.Map(input, role);
            await _applicationRoleRepository.UpdateAsync(role);
        }

        public async Task<GetRoleForEditOutputDto> GetById(EntityDto<long> input)
        {
            var role = await _applicationRoleRepository.GetSingleByIdAsync(input.Id.Value);
            var output = new GetRoleForEditOutputDto
            {
                Role = _mapper.Map<CreateOrEditRoleDto>(role)
            };
            return output;
        }

        public async Task Delete(EntityDto<long> input)
        {
            await _applicationRoleRepository.DeleteAsync(input.Id.Value);
        }
    }
}
