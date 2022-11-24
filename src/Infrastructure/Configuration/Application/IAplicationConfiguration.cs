using Microsoft.Extensions.Configuration;

namespace CommandsRegistry.Infrastructure.Configuration
{
    public interface IAplicationConfiguration
    {
        string FrontendUrl { get; }
    }
}