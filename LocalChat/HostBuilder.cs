// internal class Program
// {
//     public static void Main(string[] args)
//     {
//         var configuration = new ConfigurationBuilder()
//             .AddJsonFile("appsettings.json")
//             .Build();
//
//         var connectionString = configuration.GetConnectionString("LocalChatDbConnection");
//
//         // var options = new DbContextOptionsBuilder<LocalChatDbContext>()
//         //     .UseSqlite(connectionString)
//         //     .Options;
//         //
//         // using var context = new LocalChatDbContext();
//         // context.Users.Add(new User
//         // {
//         //     Username = "admin",
//         // });
//         // context.SaveChanges();
//         // context.Database.EnsureCreated();
//     }
// }

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LocalChat;


internal class HostBuilder
{
    private static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using var scope = host.Services.CreateScope();

        Program.Startup(scope);
        host.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((_, config) 
                => { config.AddJsonFile("appsettings.json"); })
            .ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                var configBuilder = new ConfigurationBuilder(configuration);
                configBuilder.ConfigureServices(services);
            });
}