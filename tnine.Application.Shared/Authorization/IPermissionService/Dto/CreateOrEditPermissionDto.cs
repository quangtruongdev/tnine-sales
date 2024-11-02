using tnine.Core.Shared.Dto;

namespace tnine.Application.Shared.Authorization.IPermissionService.Dto
{
    public class CreateOrEditPermissionDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
