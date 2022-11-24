using System;
using System.Threading.Tasks;
using CommandsRegistry.Domain.Entities.Core;

namespace CommandsRegistry.Infrastructure.Identity.Jwt.RefreshTokens.Repository
{
    internal interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByUserPublicIdOrDefaultAsync(Guid userPublicId);
        Task AddAsync(RefreshToken refreshToken);
        Task RemoveAsync(RefreshToken refreshToken);
        Task RemoveForUserAsync(Guid userPublicId);
    }
}