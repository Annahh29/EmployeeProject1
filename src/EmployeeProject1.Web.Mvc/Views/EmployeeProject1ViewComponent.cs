using Abp.AspNetCore.Mvc.ViewComponents;

namespace EmployeeProject1.Web.Views
{
    public abstract class EmployeeProject1ViewComponent : AbpViewComponent
    {
        protected EmployeeProject1ViewComponent()
        {
            LocalizationSourceName = EmployeeProject1Consts.LocalizationSourceName;
        }
    }
}
