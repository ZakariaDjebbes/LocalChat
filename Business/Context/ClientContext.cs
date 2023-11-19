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
        => ContextResource.Dispose();

    public void Set(ClientContextResource data)
        => ContextResource = data ?? throw new ArgumentNullException(nameof(data));

    public void Start(Server server)
    {
        var tcpClient = new SimpleTcpClient().Connect(server.Address, server.Port);
        var replyMsg = tcpClient.WriteLineAndGetReply("Hello world!", TimeSpan.FromSeconds(3));
        _logger.LogInformation("Received reply from server: {Reply}", replyMsg.MessageString);
    }
}