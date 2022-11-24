﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommandsRegistry.Application.Common.Interfaces;
using CommandsRegistry.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommandsRegistry.Infrastructure.Identity.Jwt.RefreshTokens
{
    internal class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IApplicationDbContext _context;

        public RefreshTokenRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> GetByUserPublicIdOrDefaultAsync(Guid userPublicId) 
            => await _context.RefreshTokens.SingleOrDefaultAsync(t => t.UserPublicId == userPublicId);

        public async Task AddAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task RemoveAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Remove(refreshToken);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task RemoveForUserAsync(Guid userPublicId)
        {
            var tokensToDelete = await _context.RefreshTokens
                .Where(t => t.UserPublicId == userPublicId)
                .ToListAsync();

            _context.RefreshTokens.RemoveRange(tokensToDelete);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}