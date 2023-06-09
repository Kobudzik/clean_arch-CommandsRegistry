﻿using System.Threading;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Interfaces.User;
using MediatR;

namespace CommandsRegistry.Application.Authentication.Commands.ForgotPassword
{
    public sealed class ForgotPasswordCommand : IRequest
    {
        public string UserName { get; set; }

        internal sealed class Handler : IRequestHandler<ForgotPasswordCommand>
        {
            private readonly IPasswordsManagementService _passwordsManagementService;
            private readonly IUserManagementService _userManagementService;

            public Handler(IPasswordsManagementService passwordsManagementService,
                IUserManagementService userManagementService)
            {
                _passwordsManagementService = passwordsManagementService;
                _userManagementService = userManagementService;
            }

            public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
            {
                var userExists = await _userManagementService.UserExists(request.UserName);
                if (!userExists)
                {
                    return Unit.Value;
                }

                var userDetails = await _userManagementService.GetUserDetailsAsync(request.UserName);
                await _passwordsManagementService.ForgotPasswordAsync(userDetails.Id);

                return Unit.Value;
            }
        }
    }
}