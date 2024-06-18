using Abp.AutoMapper;
using EmployeeProject1.Roles.Dto;
using EmployeeProject1.Web.Models.Common;

namespace EmployeeProject1.Web.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool HasPermission(FlatPermissionDto permission)
        {
            return GrantedPermissionNames.Contains(permission.Name);
        }
    }
}
