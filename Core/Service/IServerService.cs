using Core.Model;

namespace Core.Service;

/// <summary>
///     Represents a service for the TCP server.
/// </summary>
public interface IServerService
{
    bool Initialized { get; }

    /// <summary>
    ///     Initializes the TCP server.
    /// </summary>
    /// <param name="server">
    ///     The server to initialize.
    /// </param>
    /// <returns>
    ///     True if the server was initialized successfully, false otherwise.
    /// </returns>
    bool Initialize(Server server);

    /// <summary>
    ///     Starts the TCP server.
    /// </summary>
    void Start();

    /// <summary>
    ///     Stops the TCP server.
    /// </summary>
    void Stop();
}