using System.Collections.Generic;
using EmployeeProject1.Roles.Dto;

namespace EmployeeProject1.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}