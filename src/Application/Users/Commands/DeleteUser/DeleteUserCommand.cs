using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Interfaces;
using CommandsRegistry.Application.Common.Interfaces.User;

namespace CommandsRegistry.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public DeleteUserCommand(Guid publicId)
        {
            PublicId = publicId;
        }

        public Guid PublicId { get; }

        internal sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
        {
            private readonly IUserManagementService userManagementService;

            public DeleteUserCommandHandler(IUserManagementService userManagementService)
            {
                this.userManagementService = userManagementService;
            }

            public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                await userManagementService.RemoveUserAsync(request.PublicId.ToString());
                return Unit.Value;
            }
        }
    }
}