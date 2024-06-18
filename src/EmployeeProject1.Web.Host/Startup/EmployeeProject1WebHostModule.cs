using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using EmployeeProject1.Configuration;

namespace EmployeeProject1.Web.Host.Startup
{
    [DependsOn(
       typeof(EmployeeProject1WebCoreModule))]
    public class EmployeeProject1WebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public EmployeeProject1WebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EmployeeProject1WebHostModule).GetAssembly());
        }
    }
}
