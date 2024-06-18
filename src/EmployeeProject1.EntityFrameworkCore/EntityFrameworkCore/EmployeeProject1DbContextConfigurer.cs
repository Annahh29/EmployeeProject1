using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProject1.EntityFrameworkCore
{
    public static class EmployeeProject1DbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<EmployeeProject1DbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<EmployeeProject1DbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
