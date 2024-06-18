using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using EmployeeProject1.Authorization;

namespace EmployeeProject1
{
    [DependsOn(
        typeof(EmployeeProject1CoreModule), 
        typeof(AbpAutoMapperModule))]
    public class EmployeeProject1ApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<EmployeeProject1AuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(EmployeeProject1ApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
