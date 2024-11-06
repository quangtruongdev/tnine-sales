using System.Collections.Generic;

namespace tnine.Application.Shared.Authorization.IAccountService.Dto
{
    public class RegisterInput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<long> RoleIds { get; set; }
    }
}
