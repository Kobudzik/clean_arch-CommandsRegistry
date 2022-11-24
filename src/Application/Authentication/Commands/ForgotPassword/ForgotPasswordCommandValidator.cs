using FluentValidation;

namespace CommandsRegistry.Application.Authentication.Commands.ConfirmAccount.ForgotPassword
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(f => f.UserName)
                .NotEmpty();
        }
    }
}