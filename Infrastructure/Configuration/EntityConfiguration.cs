using Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IEntity
{
    void IEntityTypeConfiguration<T>.Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(d => d.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(d => d.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(d => d.Id)
            .HasMaxLength(36);
        builder.HasKey(d => d.Id);
    }

    protected abstract void Configure(EntityTypeBuilder<T> builder);
}