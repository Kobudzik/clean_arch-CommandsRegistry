namespace CommandsRegistry.Infrastructure.Email.Modules.User.ConfirmEmail.Configuration
{
    internal interface IConfirmEmailConfiguration
    {
        string Subject { get; }
        string PathToTemplate { get; }
    }
}