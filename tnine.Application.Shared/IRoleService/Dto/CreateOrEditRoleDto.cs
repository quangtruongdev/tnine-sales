using tnine.Core.Shared.Dto;

namespace tnine.Application.Shared.IRoleService.Dto
{
    public class CreateOrEditRoleDto : EntityDto<long>
    {
        public string Name { get; set; }
    }
}
