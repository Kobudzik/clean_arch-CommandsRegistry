using CommandsRegistry.Application.Common.Mappings;
using CommandsRegistry.Domain.Entities.Core;

namespace CommandsRegistry.Application.Users.Queries.GetUserSettings
{
    public class SettingsVM : IMapFrom<UserAccount>
    {
        public string ThemePrimaryColor { get; set; }
    }
}
