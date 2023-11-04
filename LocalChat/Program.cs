using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LocalChat;


internal static class Program
{
    private static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using var scope = host.Services.CreateScope();
        
        // var services = scope.ServiceProvider;
        // var logger = services.GetRequiredService<ILogger<Program>>();

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