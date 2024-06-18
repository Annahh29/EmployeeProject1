using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using EmployeeProject1.Controllers;

namespace EmployeeProject1.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : EmployeeProject1ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
