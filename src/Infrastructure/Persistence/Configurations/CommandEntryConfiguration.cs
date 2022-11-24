using CommandsRegistry.Domain.Entities.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommandsRegistry.Infrastructure.Persistence.Configurations
{
    public class CommandEntryConfiguration : IEntityTypeConfiguration<CommandEntry>
    {
        public void Configure(EntityTypeBuilder<CommandEntry> builder)
        {
            builder.Property(t => t.JsonSchema)
                .HasMaxLength(8000);
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}