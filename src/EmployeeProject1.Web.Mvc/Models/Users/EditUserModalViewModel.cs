using System.Collections.Generic;
using System.Linq;
using EmployeeProject1.Roles.Dto;
using EmployeeProject1.Users.Dto;

namespace EmployeeProject1.Web.Models.Users
{
    public class EditUserModalViewModel
    {
        public UserDto User { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }

        public bool UserIsInRole(RoleDto role)
        {
            return User.RoleNames != null && User.RoleNames.Any(r => r == role.NormalizedName);
        }
    }
}
