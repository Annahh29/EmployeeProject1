using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using EmployeeProject1.EntityFrameworkCore;
using EmployeeProject1.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace EmployeeProject1.Web.Tests
{
    [DependsOn(
        typeof(EmployeeProject1WebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class EmployeeProject1WebTestModule : AbpModule
    {
        public EmployeeProject1WebTestModule(EmployeeProject1EntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EmployeeProject1WebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(EmployeeProject1WebMvcModule).Assembly);
        }
    }
}