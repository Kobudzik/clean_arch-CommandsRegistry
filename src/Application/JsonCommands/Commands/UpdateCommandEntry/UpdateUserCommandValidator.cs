using CommandsRegistry.Application.Users.Commands.UpdateUser;
using FluentValidation;

namespace CommandsRegistry.Application.JsonCommands.Commands.UpdateCommandEntry
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