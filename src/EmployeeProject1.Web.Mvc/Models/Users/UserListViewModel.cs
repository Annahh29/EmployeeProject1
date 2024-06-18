using System.Collections.Generic;
using EmployeeProject1.Roles.Dto;

namespace EmployeeProject1.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
