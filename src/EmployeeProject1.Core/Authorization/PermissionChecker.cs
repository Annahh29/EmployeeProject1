using Abp.Authorization;
using EmployeeProject1.Authorization.Roles;
using EmployeeProject1.Authorization.Users;

namespace EmployeeProject1.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
