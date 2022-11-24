using CommandsRegistry.Infrastructure.Identity;
using CommandsRegistry.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommandsRegistry.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.Property(t => t.ThemePrimaryColor)
                .HasMaxLength(30);
        }
    }
}