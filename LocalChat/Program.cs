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

namespace LocalChat;

internal class Program
{
    private static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((_, config) => { config.AddJsonFile("appsettings.json"); })
            .ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                var configBuilder = new ConfigurationBuilder(configuration);
                configBuilder.ConfigureServices(services);
            })
            .Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var configuration = services.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            Console.WriteLine($"Connection String: {connectionString}");
        }

        // Run the application
        host.Run();
    }


    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddJsonFile("appsettings.json");
            });
}