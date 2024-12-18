using System.Collections.Generic;
using tnine.Core.Shared.Dtos;

namespace tnine.Application.Shared.Authorization.IUserService.Dto
{
    public class EditUserDto : EntityDto<long>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
    }
}
