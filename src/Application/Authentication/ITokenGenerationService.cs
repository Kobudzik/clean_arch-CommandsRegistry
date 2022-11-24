using System.Collections.Generic;
using CommandsRegistry.Application.Authentication.DTOs;

namespace CommandsRegistry.Application.Authentication
{
    public interface ITokenGenerationService
    {
        string GenerateToken(UserDto userDetails, IEnumerable<string> roles);
    }
}