using tnine.Core.Shared.Dto;

namespace tnine.Application.Shared.IApplicationRoleService.Dto
{
    public class GetRoleForViewDto : EntityDto<long>
    {
        public string Name { get; set; }
    }
}
