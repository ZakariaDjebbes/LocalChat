using System.Net.Sockets;
using Core.Model;

namespace Core.Service;

/// <summary>
///     Represents a service for the TCP server.
/// </summary>
public interface IServerService
{
    void AcceptClients(TcpListener listener);
    void HandleClient(object clientObject);
}