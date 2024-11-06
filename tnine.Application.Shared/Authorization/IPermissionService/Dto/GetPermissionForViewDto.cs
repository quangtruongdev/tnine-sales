using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.Authorization.IPermissionService.Dto
{
    public class GetPermissionForViewDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
