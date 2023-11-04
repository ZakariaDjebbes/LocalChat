using Core.Model;
using Core.Repository;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LocalChat.Controller;

public class ServerController : IHostedService
{
    private readonly ILogger<ServerController> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    
    public ServerController(ILogger<ServerController> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("ServerController started");
        
        using var scope = _serviceScopeFactory.CreateScope();
        var services = scope.ServiceProvider;
        var repo = services.GetRequiredService<IRepository<User>>();
        
        
        var user = new User
        {
            Username = "test",
            Password = "test"
        };
        
        repo.Add(user);
        repo.Commit();
        var users = repo.GetAll();
        Console.WriteLine("Users:" + users.Count());
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("ServerController stopped");
        return Task.CompletedTask;
    }
}