using CommandsRegistry.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

namespace CommandsRegistry.Infrastructure.Email.Modules.User.ConfirmEmail.Configuration
{
    internal sealed class ConfirmEmailConfiguration : ConfigurationBase, IConfirmEmailConfiguration
    {
        public ConfirmEmailConfiguration(IConfiguration configuration)
            : base(configuration, "ConfirmationEmailConfiguration")
        {
        }

        public string Subject => configurationSection.GetValue<string>(nameof(Subject));

        public string PathToTemplate => configurationSection.GetValue<string>(nameof(PathToTemplate));
    }
}