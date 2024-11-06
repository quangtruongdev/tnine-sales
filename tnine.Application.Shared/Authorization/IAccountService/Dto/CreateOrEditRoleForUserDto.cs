using System.Collections.Generic;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.Authorization.IAccountService.Dto
{
    public class CreateOrEditRoleForUserDto : EntityDto<long>
    {
        public long UserId { get; set; }
        public List<long> RoleIds { get; set; }
    }
}
