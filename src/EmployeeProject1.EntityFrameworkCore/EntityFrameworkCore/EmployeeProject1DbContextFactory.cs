using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using EmployeeProject1.Configuration;
using EmployeeProject1.Web;

namespace EmployeeProject1.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class EmployeeProject1DbContextFactory : IDesignTimeDbContextFactory<EmployeeProject1DbContext>
    {
        public EmployeeProject1DbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EmployeeProject1DbContext>();
            
            /*
             You can provide an environmentName parameter to the AppConfigurations.Get method. 
             In this case, AppConfigurations will try to read appsettings.{environmentName}.json.
             Use Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") method or from string[] args to get environment if necessary.
             https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#args
             */
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            EmployeeProject1DbContextConfigurer.Configure(builder, configuration.GetConnectionString(EmployeeProject1Consts.ConnectionStringName));

            return new EmployeeProject1DbContext(builder.Options);
        }
    }
}
