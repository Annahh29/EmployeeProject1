using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EmployeeProject1.MultiTenancy;

namespace EmployeeProject1.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
