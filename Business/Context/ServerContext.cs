using Business.Service;
using Core.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Business.Context;

public class ServerContext : IServerContext
{
    private readonly ILogger<ServerService> _logger;
    
    public ServerContext(IServiceProvider serviceProvider)
    {
        _logger = serviceProvider.GetRequiredService<ILogger<ServerService>>();
    }
    
    public Guid ContextId { get; init; }
    public ServerContextResource ContextResource { get; set; }
    
    public void Clear()
        => ContextResource.Dispose();

    public void Set(ServerContextResource data) 
        => ContextResource = data ?? throw new ArgumentNullException(nameof(data));

    public void Start()
    {
        
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }
}