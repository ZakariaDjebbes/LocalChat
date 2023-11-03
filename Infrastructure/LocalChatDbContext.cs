using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class LocalChatDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    public LocalChatDbContext(DbContextOptions<LocalChatDbContext> options) : base(options)
    {
    }
}