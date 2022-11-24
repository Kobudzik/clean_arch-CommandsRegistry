using System;

namespace CommandsRegistry.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        Guid UserGuid();
        string[] GetCurrentUserRoles();
        bool IsAdmin { get; }
        bool IsClient { get; }
    }
}