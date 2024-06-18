using System.Threading.Tasks;
using Abp.Application.Services;
using EmployeeProject1.Sessions.Dto;

namespace EmployeeProject1.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
