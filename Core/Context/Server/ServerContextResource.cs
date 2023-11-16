using System.Net;
using System.Net.Sockets;
using Core.Model;

namespace Core.Context;

public class ServerContextResource : DisposableResource
{
    /// <summary>
    /// The servers currently registered in the context.
    /// </summary>
    public IDictionary<Server, TcpListener> Servers { get; } = new Dictionary<Server, TcpListener>();

    /// <summary>
    /// Creates a new instance of the <see cref="ServerContextResource"/> class.
    /// </summary>
    public ServerContextResource()
    {
    }

    public TcpListener Add(Server server)
    {
        var listener = new TcpListener(IPAddress.Parse(server.Address), server.Port);
        Servers.Add(server, listener);
        return listener;
    }

    public void Remove(Server server)
        => Servers.Remove(server);

    protected override void Dispose(bool disposing)
    {
        if (!disposing) return;

        foreach (var (_, tcpListener) in Servers)
        {
            tcpListener.Stop();
        }

        Servers.Clear();
    }
}