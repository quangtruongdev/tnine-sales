using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tnine.Application.Shared.IRoleService;
using tnine.Application.Shared.IRoleService.Dto;
using tnine.Core;
using tnine.Core.Shared.Dtos;
using tnine.Core.Shared.Repositories;

namespace tnine.Application
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _applicationRoleRepo;
        private readonly IPermissionRepository _permissionRepo;
        private readonly IRolePermissionRepository _rolePermissionRepo;
        private IMapper _mapper;

        public RoleService(
            IRoleRepository applicationRoleRepo,
            IPermissionRepository permissionRepo,
            IRolePermissionRepository rolePermissionRepo,
            IMapper mapper
            )
        {
            _applicationRoleRepo = applicationRoleRepo;
            _permissionRepo = permissionRepo;
            _rolePermissionRepo = rolePermissionRepo;
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

            var roleId = await _applicationRoleRepo.InsertAndGetIdAsync(role);

            role.RolePermissions = new List<RolePermission>();

            if (input.PermissionIds != null)
            {
                foreach (var permissionId in input.PermissionIds)
                {
                    var permission = await _permissionRepo.GetSingleByIdAsync(permissionId);

                    if (permission != null)
                    {
                        await _rolePermissionRepo.InsertAsync(new RolePermission
                        {
                            RoleId = roleId,
                            PermissionId = permission.Id
                        });
                    }
                }
            }
        }

        private async Task Edit(CreateOrEditRoleDto input)
        {
            var role = await _applicationRoleRepo.GetSingleByIdAsync(input.Id.Value);

            _mapper.Map(input, role);

            var existingRolePermissions = await _rolePermissionRepo.GetAllByConditionAsync(rp => rp.RoleId == role.RoleId);
            if (existingRolePermissions != null)
            {
                foreach (var rolePermission in existingRolePermissions)
                {
                    await _rolePermissionRepo.DeleteAsync(rolePermission.Id);
                }
            }

            role.RolePermissions = new List<RolePermission>();
            foreach (var permissionId in input.PermissionIds)
            {
                var permission = await _permissionRepo.GetSingleByIdAsync(permissionId);

                if (permission != null)
                {
                    await _rolePermissionRepo.InsertAsync(new RolePermission
                    {
                        RoleId = role.RoleId,
                        PermissionId = permission.Id
                    });
                }
            }
        }

        public async Task<GetRoleForEditOutputDto> GetById(EntityDto<long> input)
        {
            var role = await _applicationRoleRepo.GetSingleByIdAsync(input.Id.Value);
            var output = new GetRoleForEditOutputDto
            {
                Role = _mapper.Map<CreateOrEditRoleDto>(role)
            };
            return output;
        }

        public async Task Delete(EntityDto<long> input)
        {
            await _applicationRoleRepo.DeleteAsync(input.Id.Value);
        }
    }
}
