using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CommandsRegistry.Domain.Entities;
using CommandsRegistry.Infrastructure.Identity;

namespace CommandsRegistry.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<UserAccount> UserAccounts { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
