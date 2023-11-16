using System.Net.Sockets;
using Core.Service;
using Microsoft.Extensions.Logging;

namespace Business.Service;

public class ServerService : IServerService
{
    private readonly ILogger<ServerService> _logger;

    public ServerService(ILogger<ServerService> logger)
    {
        _logger = logger;
    }
    public void AcceptClients(TcpListener listener)
    {
        while (true)
            try
            {
                var client = listener.AcceptTcpClient();
                _logger.LogInformation("Client connected: {@ClientRemoteEndPoint}", client.Client.RemoteEndPoint);
                ThreadPool.QueueUserWorkItem(HandleClient, client);
            }
            catch (SocketException e)
            {
                _logger.LogError("Error accepting client: {ErrorMessage}", e.Message);
                break;
            }
    }

    public void HandleClient(object clientObject)
    {
        var client = (TcpClient)clientObject;

        // Read message from client
        _logger.LogInformation("Reading message from client..." + client.Client.RemoteEndPoint);
        // Close the client connection when done
        // client.Close();
        // Console.WriteLine($"Client disconnected: {client.Client.RemoteEndPoint}");
    }
}