using Microsoft.Extensions.Configuration;

namespace CommandsRegistry.Infrastructure.Configuration.Application
{
    internal sealed class AplicationConfiguration : ConfigurationBase, IAplicationConfiguration
    {
        public AplicationConfiguration(IConfiguration configuration) : base(configuration, "Application")
        {
        }

        public string FrontendUrl => configurationSection.GetValue<string>(nameof(FrontendUrl));
    }
}