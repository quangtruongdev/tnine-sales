using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.IRoleService.Dto
{
    public class GetRoleForViewDto : EntityDto<long>
    {
        public string Name { get; set; }
    }
}
