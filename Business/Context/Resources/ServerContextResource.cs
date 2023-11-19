using Core.Context;
using Core.Model;
using SimpleTCP;

namespace Business.Context.Resources;

public class ServerContextResource : DisposableResource
{
    /// <summary>
    ///     Creates a new instance of the <see cref="ServerContextResource" /> class.
    /// </summary>
    public ServerContextResource()
    {
    }

    /// <summary>
    ///     The servers currently registered in the context.
    /// </summary>
    public IDictionary<Server, SimpleTcpServer> Servers { get; } = new Dictionary<Server, SimpleTcpServer>();
    
    public SimpleTcpServer Add(Server server)
    {
        var exists = Servers.Keys.FirstOrDefault(x => x.Id == server.Id);
        
        if (exists is not null) return Servers[exists];

        var tcpListener = new SimpleTcpServer();
        Servers.Add(server, tcpListener);

        return tcpListener;
    }

    public void Remove(Server server) => Servers.Remove(server);

    protected override void Dispose(bool disposing)
    {
        if (!disposing) return;

        foreach (var (_, tcpListener) in Servers) tcpListener.Stop();

        Servers.Clear();
    }
}