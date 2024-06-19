using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using EmployeeProject1.Authorization.Roles;
using EmployeeProject1.Authorization.Users;
using EmployeeProject1.MultiTenancy;
using EmployeeProject1.Domain.Employees;
using EmployeeProject1.Domain.Skills;

namespace EmployeeProject1.EntityFrameworkCore
{
    public class EmployeeProject1DbContext : AbpZeroDbContext<Tenant, Role, User, EmployeeProject1DbContext>
    {
        /* Define a DbSet for each entity of the application */

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public EmployeeProject1DbContext(DbContextOptions<EmployeeProject1DbContext> options)
            : base(options)
        {


        }
    }
}
