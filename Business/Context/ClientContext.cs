using System.Net.Sockets;
using Business.Context.Resources;
using Core.Context;
using Core.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleTCP;

namespace Business.Context;

public class ClientContext : IClientContext<ClientContextResource>
{
    private readonly ILogger<ServerContext> _logger;
    private readonly IUserContext<UserContextResource> _userContext;

    public ClientContext(IServiceProvider serviceProvider,
        IUserContext<UserContextResource> userContext)
    {
        ContextId = Guid.NewGuid();
        ContextResource = new ClientContextResource();
        _logger = serviceProvider.GetRequiredService<ILogger<ServerContext>>();
        _userContext = userContext;
    }

    public Guid ContextId { get; init; }
    public ClientContextResource ContextResource { get; set; }

    public void Clear()
    {
        ContextResource.Dispose();
    }

    public void Set(ClientContextResource data)
    {
        ContextResource = data ?? throw new ArgumentNullException(nameof(data));
    }

    public bool Start(Server server)
    {
        try
        {
            var tcpClient = new SimpleTcpClient().Connect(server.Address, server.Port);
            tcpClient.DataReceived += OnDataReceived;
            Set(new ClientContextResource
            {
                Client = tcpClient
            });
        }
        catch (SocketException e)
        {
            _logger.LogError("Error connecting to server: {ErrorMessage}", e.Message);
            return false;
        }

        return true;
    }

    private void OnDataReceived(object sender, Message e)
    {
        var message = e.MessageString;
        _logger.LogInformation("Received message from server: {Message}", message);
    }
}