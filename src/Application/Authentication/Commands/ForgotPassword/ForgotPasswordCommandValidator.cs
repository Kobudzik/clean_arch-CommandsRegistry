using FluentValidation;

namespace CommandsRegistry.Application.Authentication.Commands.ForgotPassword
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