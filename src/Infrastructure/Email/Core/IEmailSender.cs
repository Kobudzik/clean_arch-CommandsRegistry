using System.Threading.Tasks;

namespace CommandsRegistry.Infrastructure.Email.Core
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string text);
    }
}