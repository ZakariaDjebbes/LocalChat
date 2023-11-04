using System.Reflection;
using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public sealed class LocalChatDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    public LocalChatDbContext(DbContextOptions<LocalChatDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}