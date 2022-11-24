using Microsoft.Extensions.Configuration;
using System.IO;

namespace CommandsRegistry.Infrastructure.Email.Core.TemplateReader
{
    internal sealed class MailTemplateReader : IMailTemplateReader
    {
        private readonly IConfiguration _configuration;

        public MailTemplateReader(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Read(string relativePathToTemplate)
        {
            var rootTemplatePath = _configuration.GetSection("Mail").GetValue<string>("TemplatesPath");
            var pathToTemplate = Path.Combine(rootTemplatePath, relativePathToTemplate);
            return File.ReadAllText(pathToTemplate);
        }
    }
}