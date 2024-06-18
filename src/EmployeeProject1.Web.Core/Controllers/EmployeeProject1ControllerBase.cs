using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace EmployeeProject1.Controllers
{
    public abstract class EmployeeProject1ControllerBase: AbpController
    {
        protected EmployeeProject1ControllerBase()
        {
            LocalizationSourceName = EmployeeProject1Consts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
