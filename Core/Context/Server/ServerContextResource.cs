using System.Net.Sockets;

namespace Core.Context;

public class ServerContextResource : DisposableResource
{
    /// <summary>
    /// The current <see cref="System.Net.Sockets.TcpListener"/> instance.
    /// </summary>
    public TcpListener TcpListener { get; private set; }
    
    /// <summary>
    /// Creates a new instance of the <see cref="ServerContextResource"/> class.
    /// </summary>
    /// <param name="tcpListener">
    /// The current <see cref="TcpListener"/> instance.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="tcpListener"/> is null.
    /// </exception>
    public ServerContextResource(TcpListener tcpListener) 
        => TcpListener = tcpListener ?? throw new ArgumentNullException(nameof(tcpListener));

    protected override void Dispose(bool disposing)
    {
        if (!disposing) return;
        
        TcpListener.Stop();
        TcpListener = null;
    }
}