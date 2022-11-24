﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Exceptions;
using CommandsRegistry.Application.Common.Interfaces;
using CommandsRegistry.Application.Common.Interfaces.User;
using CommandsRegistry.Application.Users.Commands.CreateUser.Request;
using MediatR;

namespace CommandsRegistry.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; }

        internal sealed class Handler : IRequestHandler<CreateUserCommand, string>
        {
            private readonly IMediator _mediator;
            private readonly IUserManagementService _userManagementService;

            public Handler(IMediator mediator, IUserManagementService userManagementService)
            {
                _mediator = mediator;
                _userManagementService = userManagementService;
            }

            public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var user = new CreateUserModel(
                    request.UserName,
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.Password,
                    request.IsActive
                );

                foreach (var roleName in request.Roles)
                {
                    var roleExists = await _userManagementService.GetRoleAsync(roleName);

                    if (!roleExists)
                        throw new CustomValidationException(nameof(roleName), $"Role {roleName} not found!");
                }

                var userEntity = await _userManagementService.CreateUserAsync(user);

                await _userManagementService.AddUserToRolesAsync(userEntity.Guid, request.Roles);

                var token = await _userManagementService.GenerateEmailConfirmationToken(userEntity);
                var @event = UserCreatedEvent.Create(userEntity.Guid, request.Email, request.UserName, token);

                await _mediator.Publish(@event, cancellationToken);

                return userEntity.Id;
            }
        }
    }
}
