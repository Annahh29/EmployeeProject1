using System.Threading.Tasks;
using EmployeeProject1.Configuration.Dto;

namespace EmployeeProject1.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
