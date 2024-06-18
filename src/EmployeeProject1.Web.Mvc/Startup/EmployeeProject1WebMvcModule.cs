using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using EmployeeProject1.Configuration;

namespace EmployeeProject1.Web.Startup
{
    [DependsOn(typeof(EmployeeProject1WebCoreModule))]
    public class EmployeeProject1WebMvcModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public EmployeeProject1WebMvcModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<EmployeeProject1NavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EmployeeProject1WebMvcModule).GetAssembly());
        }
    }
}
