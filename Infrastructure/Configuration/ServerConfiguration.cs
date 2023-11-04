using Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class ServerConfiguration : IEntityTypeConfiguration<Server>
{
    public void Configure(EntityTypeBuilder<Server> builder)
    {
        builder.HasIndex(s => s.Name).IsUnique();
        builder.HasIndex(s => new { s.Address, s.Port }).IsUnique();
        builder.Property(s => s.IsRunning).HasDefaultValue(false);
    }
}