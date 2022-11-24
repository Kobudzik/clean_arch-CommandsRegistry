using System;
using System.Threading.Tasks;
using CommandsRegistry.Domain.Entities;

namespace CommandsRegistry.Infrastructure.Identity.Jwt.RefreshTokens
{
    internal interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByUserPublicIdOrDefaultAsync(Guid userPublicId);
        Task AddAsync(RefreshToken refreshToken);
        Task RemoveAsync(RefreshToken refreshToken);
        Task RemoveForUserAsync(Guid userPublicId);
    }
}