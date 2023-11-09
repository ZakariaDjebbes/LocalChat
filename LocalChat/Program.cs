using Infrastructure;
using Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ZConsole.Service;

namespace LocalChat;

internal class Program
{
    private static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();
            var consoleService = services.GetRequiredService<IConsoleService>();

            try
            {
                var context = services.GetRequiredService<LocalChatDbContext>();
                logger.LogInformation("Migrating...");
                context.Database.Migrate();
                logger.LogInformation("Seeding the database if empty...");
                LocalChatDbContextSeed.Seed(context, loggerFactory);
                consoleService.Clear();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured during migration...");
            }
        }

        host.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config)
                =>
            {
                var environmentName = context.HostingEnvironment.EnvironmentName.ToLower();
                config.AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                var configBuilder = new ConfigurationBuilder(configuration);
                configBuilder.ConfigureServices(services);
            });
    }
}