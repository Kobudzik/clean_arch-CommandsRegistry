using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CommandsRegistry.Domain.Entities;
using CommandsRegistry.Domain.Entities.Commands;
using CommandsRegistry.Domain.Entities.Core;

namespace CommandsRegistry.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<UserAccount> UserAccounts { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
        DbSet<CommandEntry> CommandEntries { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
