using System;
using System.Collections.Generic;
using CommandsRegistry.Application.Authentication.DTOs;

namespace CommandsRegistry.Application.Authentication
{
    public interface IJwtTokenManagementService
    {
        string GenerateJwtToken(UserDto userDetails, IList<string> roles);
        Guid GetUserIdFromToken(string accessToken);
    }
}