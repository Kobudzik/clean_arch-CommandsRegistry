using FluentValidation;

namespace CommandsRegistry.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.FirstName).MaximumLength(255).NotEmpty();
            RuleFor(x => x.LastName).MaximumLength(255).NotEmpty();
        }
    }
}