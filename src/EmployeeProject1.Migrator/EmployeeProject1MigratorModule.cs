using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using EmployeeProject1.Configuration;
using EmployeeProject1.EntityFrameworkCore;
using EmployeeProject1.Migrator.DependencyInjection;

namespace EmployeeProject1.Migrator
{
    [DependsOn(typeof(EmployeeProject1EntityFrameworkModule))]
    public class EmployeeProject1MigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public EmployeeProject1MigratorModule(EmployeeProject1EntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(EmployeeProject1MigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                EmployeeProject1Consts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EmployeeProject1MigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
