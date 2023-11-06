using Business.Service;
using Core.Repository;
using Core.Service;
using Infrastructure;
using Infrastructure.Repository;
using LocalChat.Controller;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZConsole.Implementation;
using ZConsole.Service;

namespace LocalChat;

/// <summary>
/// This class is responsible for configuring the application's services
/// </summary>
public class ConfigurationBuilder
{
    /// <summary>
    /// The internal configuration object
    /// </summary>
    private readonly IConfiguration _config;

    /// <summary>
    /// Constructs a new instance of <see cref="ConfigurationBuilder"/>
    /// </summary>
    /// <param name="config"></param>
    public ConfigurationBuilder(IConfiguration config)
    {
        _config = config;
    }   
    
    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services">The services collection</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContextFactory<LocalChatDbContext>(ctx =>
        {
            ctx.UseSqlite(_config.GetConnectionString("LocalChatDbConnection"));
        });
        services.AddHostedService<ConsoleInterfaceController>();
        services.AddScoped<IConsoleService, ConsoleService>();
        services.AddScoped<IConsolePromptService, ConsolePromptService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IServerService, ServerService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}