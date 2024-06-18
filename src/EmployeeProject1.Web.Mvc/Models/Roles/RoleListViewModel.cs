using System.Collections.Generic;
using EmployeeProject1.Roles.Dto;

namespace EmployeeProject1.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
