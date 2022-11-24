using System;
using MediatR;

namespace CommandsRegistry.Application.Users.Commands.ChangeUserPassword
{
    public class UserPasswordChangedNotification : INotification
    {
        public Guid PublicId { get; set; }
    }
}