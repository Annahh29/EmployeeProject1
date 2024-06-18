using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace EmployeeProject1.Web.Views
{
    public abstract class EmployeeProject1RazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected EmployeeProject1RazorPage()
        {
            LocalizationSourceName = EmployeeProject1Consts.LocalizationSourceName;
        }
    }
}
