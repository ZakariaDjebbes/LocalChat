using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        services.AddDbContext<LocalChatDbContext>(ctx =>
        {
            ctx.UseSqlite(_config.GetConnectionString("DefaultConnection"));
        });
    }
    
    
}