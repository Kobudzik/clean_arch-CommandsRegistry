namespace CommandsRegistry.Infrastructure.Email.Core.TemplateReader
{
    public interface IMailTemplateReader
    {
        string Read(string relativePathToTemplate);
    }
}