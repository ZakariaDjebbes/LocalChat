using System.Text.Json;
using Core.Model;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Seed;

public static class LocalChatDbContextSeed
{
    private const string RolesPath = "Seed/roles.seed.json";

    public static void Seed(LocalChatDbContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            if (context.Roles.Any()) return;

            var rolesData = File.ReadAllText(RolesPath);
            var roles = JsonSerializer.Deserialize<List<Role>>(rolesData);

            roles.ForEach(role => context.Roles.Add(role));
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<LocalChatDbContext>();
            logger.LogError(ex, "An error occured while seeding data");
        }
    }
}