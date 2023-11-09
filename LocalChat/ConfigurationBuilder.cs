using Business.Context;
using Business.Service;
using Core.Command;
using Core.Context;
using Core.Repository;
using Core.Service;
using Infrastructure;
using Infrastructure.Repository;
using LocalChat.Command;
using LocalChat.Controller;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZConsole.Implementation;
using ZConsole.Service;

namespace LocalChat;

/// <summary>
///     This class is responsible for configuring the application's services
/// </summary>
public class ConfigurationBuilder
{
    /// <summary>
    ///     The internal configuration object
    /// </summary>
    private readonly IConfiguration _config;

    /// <summary>
    ///     Constructs a new instance of <see cref="ConfigurationBuilder" />
    /// </summary>
    /// <param name="config"></param>
    public ConfigurationBuilder(IConfiguration config)
    {
        _config = config;
    }

    /// <summary>
    ///     This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services">The services collection</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContextFactory<LocalChatDbContext>(ctx =>
        {
            ctx.UseSqlite(_config.GetConnectionString("LocalChatDbConnection"));
        });
        
        // Context
        services.AddSingleton<IUserContext, UserContext>();

        // Runners
        services.AddHostedService<ConsoleInterfaceHost>();

        // Console services
        services.AddScoped<IConsoleService, ConsoleService>();
        services.AddScoped<IPromptService, PromptService>();
        services.AddScoped<ILoggerService, LoggerService>();

        // Server services 
        services.AddScoped<IServerService, ServerService>();

        // Authentication services
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ITokenService, TokenService>();

        // Command services
        services.AddScoped<ICommandExecutor, CommandExecutor>();
        services.AddScoped<ICommand, SignInCommand>();
        services.AddScoped<ICommand, SignUpCommand>();
        services.AddScoped<ICommand, WhoAmICommand>();
        services.AddScoped<ICommand, SignOutCommand>();
        services.AddScoped<ICommand, CreateServerCommand>();
        services.AddScoped<ICommand, GetServersCommand>();

        // Repository services
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}