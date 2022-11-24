using System.Threading.Tasks;

namespace CommandsRegistry.Application.Common.Interfaces.Email.User.ConfirmEmail
{
    public interface IConfirmEmailSender
    {
        Task SendConfirmationEmail(string email, string username, string userId, string token);
    }
}