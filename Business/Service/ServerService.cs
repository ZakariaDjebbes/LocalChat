using System.Net;
using System.Net.Sockets;
using System.Text;
using Core.Model;
using Core.Service;
using Microsoft.Extensions.Logging;

namespace Business.Service;

public class ServerService : IServerService
{
    private readonly ILogger<ServerService> _logger;
    private Server _server;

    private TcpListener _tcpListener;

    public ServerService(ILogger<ServerService> logger)
    {
        _logger = logger;
    }

    public bool Initialized { get; private set; }

    public bool Initialize(Server server)
    {
        if (server == null)
        {
            _logger.LogError("Server is null");
            return false;
        }

        if (server.Address == null)
        {
            _logger.LogError("Server address is empty");
            return false;
        }

        if (server.Port == 0)
        {
            _logger.LogError("Server port is empty");
            return false;
        }

        _server = server;

        Initialized = true;
        _tcpListener = new TcpListener(IPAddress.Parse(_server.Address), _server.Port);
        _logger.LogInformation("Server initialized: {@Server}", _server);
        return true;
    }

    public void Start()
    {
        if (_server.IsRunning)
        {
            _logger.LogWarning("You're trying to start a server that is already running");
            return;
        }

        try
        {
            _tcpListener.Start();
            _server.IsRunning = true;
            _logger.LogInformation("Server is running on {ServerAddress}:{ServerPort}", _server.Address, _server.Port);

            AcceptClients();
        }
        catch (Exception e)
        {
            _logger.LogError("Error starting the server: {ErrorMessage}", e.Message);
        }
    }

    public void Stop()
    {
        if (!_server.IsRunning)
        {
            _logger.LogWarning("You're trying to stop a server that is not running");
            return;
        }

        try
        {
            _tcpListener.Stop();
            _server.IsRunning = false;
            _logger.LogInformation("Server stopped");
        }
        catch (Exception e)
        {
            _logger.LogError("Error stopping the server: {ErrorMessage}", e.Message);
        }
    }

    private void AcceptClients()
    {
        while (_server.IsRunning)
            try
            {
                var client = _tcpListener.AcceptTcpClient();
                _logger.LogInformation("Client connected: {@ClientRemoteEndPoint}", client.Client.RemoteEndPoint);
                ThreadPool.QueueUserWorkItem(HandleClient, client);
            }
            catch (SocketException e)
            {
                _logger.LogError("Error accepting client: {ErrorMessage}", e.Message);
                break;
            }
    }

    private void HandleClient(object clientObject)
    {
        var client = (TcpClient)clientObject;
        var stream = client.GetStream();

        // Read message from client
        var message = new byte[4096];
        var bytesRead = stream.Read(message, 0, message.Length);
        var messageString = Encoding.ASCII.GetString(message, 0, bytesRead);
        _logger.LogInformation("Message received from client: {Message}", messageString);
        // Close the client connection when done
        // client.Close();
        // Console.WriteLine($"Client disconnected: {client.Client.RemoteEndPoint}");
    }
}