using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using EmployeeProject1.Configuration.Dto;

namespace EmployeeProject1.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : EmployeeProject1AppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
