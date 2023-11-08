using System.Reflection;
using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public sealed class LocalChatDbContext : DbContext
{
    public LocalChatDbContext(DbContextOptions<LocalChatDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Server> Servers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is IEntity && x.State is EntityState.Added or EntityState.Modified);

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow;

            if (entity.State == EntityState.Added) ((IEntity)entity.Entity).CreatedAt = now;

            ((IEntity)entity.Entity).UpdatedAt = now;
        }
    }
}