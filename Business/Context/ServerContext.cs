using System.Net.Sockets;
using Core.Context;
using Core.Model;
using Core.Repository;
using Core.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Business.Context;

public class ServerContext : IServerContext
{
    private readonly ILogger<ServerContext> _logger;
    private readonly IServerService _serverService;

    public ServerContext(IServiceProvider serviceProvider)
    {
        ContextId = Guid.NewGuid();
        ContextResource = new ServerContextResource();
        _logger = serviceProvider.GetRequiredService<ILogger<ServerContext>>();
        _serverService = serviceProvider.GetRequiredService<IServerService>();
    }

    public Guid ContextId { get; init; }
    public ServerContextResource ContextResource { get; set; }

    public void Clear()
        => ContextResource.Dispose();

    public void Set(ServerContextResource data)
        => ContextResource = data ?? throw new ArgumentNullException(nameof(data));

    public Task Start(Server server)
    {
        var listener = ContextResource.Add(server);

        try
        {
            listener.Start();
            _logger.LogInformation("TcpListener started");
        }
        catch (SocketException e)
        {
            _logger.LogError("Error starting TcpListener: {ErrorMessage}", e.Message);
            return Task.CompletedTask;
        }

        return Task.Run(() => _serverService.AcceptClients(listener));
    }

    public Task Stop(Server server)
    {
        var listener = ContextResource.Servers
            .FirstOrDefault(x => x.Key.Id == server.Id)
            .Value;
        
        if (listener == null)
        {
            _logger.LogError("No TcpListener found for server {ServerName}", server.Name);
            return Task.CompletedTask;
        }

        try
        {
            listener.Stop();
            _logger.LogInformation("TcpListener stopped");
        }
        catch (SocketException e)
        {
            _logger.LogError("Error stopping TcpListener: {ErrorMessage}", e.Message);
            return Task.CompletedTask;
        }
        
        return Task.CompletedTask;
    }
}