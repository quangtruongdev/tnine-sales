using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.Authorization.IPermissionService;
using tnine.Application.Shared.Authorization.IPermissionService.Dto;
using tnine.Core;
using tnine.Core.Shared.Dto;
using tnine.Core.Shared.Repositories;

namespace tnine.Application.Authorization.Permissions
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private IMapper _mapper;

        public PermissionService(
            IPermissionRepository permissionRepository,
            IMapper mapper
            )
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<List<GetPermissionForViewDto>> GetAll()
        {
            var permissions = await _permissionRepository.GetAllAsync();

            return permissions.Select(x => new GetPermissionForViewDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();
        }

        public Task CreateOrEdit(CreateOrEditPermissionDto input)
        {
            if (input.Id == null)
            {
                return Create(input);
            }
            else
            {
                return Edit(input);
            }
        }

        private async Task Create(CreateOrEditPermissionDto input)
        {
            var permission = _mapper.Map<Permission>(input);
            await _permissionRepository.InsertAsync(permission);
        }

        private async Task Edit(CreateOrEditPermissionDto input)
        {
            var permission = await _permissionRepository.GetSingleByIdAsync(input.Id.Value);
            _mapper.Map(input, permission);
            await _permissionRepository.UpdateAsync(permission);
        }

        public async Task<GetPermissionForEditOutputDto> GetById(EntityDto<long> input)
        {
            var permission = await _permissionRepository.GetSingleByIdAsync(input.Id.Value);
            var output = new GetPermissionForEditOutputDto
            {
                Permission = _mapper.Map<CreateOrEditPermissionDto>(permission)
            };
            return output;
        }

        public async Task Delete(EntityDto<long> input)
        {
            await _permissionRepository.DeleteAsync(input.Id.Value);
        }
    }
}
