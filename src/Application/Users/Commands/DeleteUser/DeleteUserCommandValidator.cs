using FluentValidation;

namespace CommandsRegistry.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(v => v.PublicId).NotEmpty();
        }
    }
}