using Microsoft.Extensions.Configuration;

namespace CommandsRegistry.Infrastructure.Configuration
{
    public abstract class ConfigurationBase
    {
        private readonly IConfiguration _configuration;
        protected IConfigurationSection configurationSection;

        protected ConfigurationBase(IConfiguration configuration, string sectionName)
        {
            _configuration = configuration;
            configurationSection = configuration.GetSection(sectionName);
        }
    }
}