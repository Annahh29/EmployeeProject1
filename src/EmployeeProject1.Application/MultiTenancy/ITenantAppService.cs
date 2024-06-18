using Abp.Application.Services;
using EmployeeProject1.MultiTenancy.Dto;

namespace EmployeeProject1.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

