using tnine.Application.Shared.Authorization.IAccountService;
using tnine.Core.Shared.Repositories;

namespace tnine.Application.Authorization.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IRoleRepository _applicationRoleRepo;
        private readonly IUserRoleRepository _applicationUserRoleRepo;

        public AccountService(
            IUserRoleRepository applicationUserRoleRepo,
            IRoleRepository applicationRoleRepo
            )
        {
            _applicationUserRoleRepo = applicationUserRoleRepo;
            _applicationRoleRepo = applicationRoleRepo;
        }
    }
}
