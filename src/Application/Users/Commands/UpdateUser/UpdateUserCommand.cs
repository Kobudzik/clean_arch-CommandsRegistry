using System;
using System.Threading;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Interfaces;
using CommandsRegistry.Application.Common.Interfaces.User;
using CommandsRegistry.Application.Users.Commands.UpdateUser.Request;
using MediatR;

namespace CommandsRegistry.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ThemePrimaryColor { get; set; }

        internal sealed class Handler : IRequestHandler<UpdateUserCommand>
        {
            private readonly IUserManagementService _userManagementService;

            public Handler(IUserManagementService userManagementService)
            {
                _userManagementService = userManagementService;
            }

            public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var updateModel = new UpdateUserModel(
                    request.UserId,
                    request.FirstName,
                    request.LastName,
                    request.ThemePrimaryColor
                );

                await _userManagementService.UpdateAsync(updateModel);
                return Unit.Value;
            }
        }
    }
}
