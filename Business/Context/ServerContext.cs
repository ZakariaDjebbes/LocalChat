using System.Net;
using System.Net.Sockets;
using Business.Context.Resources;
using Core.Context;
using Core.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleTCP;

namespace Business.Context;

public class ServerContext : IServerContext<ServerContextResource>
{
    private readonly ILogger<ServerContext> _logger;

    public ServerContext(IServiceProvider serviceProvider)
    {
        ContextId = Guid.NewGuid();
        ContextResource = new ServerContextResource();
        _logger = serviceProvider.GetRequiredService<ILogger<ServerContext>>();
    }

    public Guid ContextId { get; init; }
    public ServerContextResource ContextResource { get; set; }

    public void Clear()
    {
        ContextResource.Dispose();
    }

    public void Set(ServerContextResource data)
    {
        ContextResource = data ?? throw new ArgumentNullException(nameof(data));
    }

    public void Start(Server server)
    {
        var listener = ContextResource.Add(server);

        if (listener == null)
        {
            _logger.LogError("TcpListener already exists for server {ServerName}", server.Name);
            return;
        }

        if (listener.IsStarted)
        {
            _logger.LogError("TcpListener already started for server {ServerName}", server.Name);
            return;
        }

        try
        {
            listener.Start(IPAddress.Parse(server.Address), server.Port);

            listener.ClientConnected += (_, client) => OnClientConnected(listener, client);
            listener.DataReceived += (_, msg) => OnDataReceived(listener, msg);

            _logger.LogInformation("TcpListener setup and started successfully");
        }
        catch (SocketException e)
        {
            _logger.LogError("Error starting TcpListener: {ErrorMessage}", e.Message);
        }
    }

    public void Stop(Server server)
    {
        var listener = ContextResource.Servers
            .FirstOrDefault(x => x.Key.Id == server.Id)
            .Value;

        if (listener == null)
        {
            _logger.LogError("No TcpListener found for server {ServerName}", server.Name);
            return;
        }

        try
        {
            listener.Stop();
            _logger.LogInformation("TcpListener stopped");
        }
        catch (SocketException e)
        {
            _logger.LogError("Error stopping TcpListener: {ErrorMessage}", e.Message);
        }
    }

    private void OnDataReceived(object sender, Message msg)
    {
        var listener = (SimpleTcpServer)sender;
        _logger.LogInformation("Received data from {@RemoteEndPoint}: {Data}",
            msg.TcpClient.Client.RemoteEndPoint, msg.Data);
        listener.BroadcastLine("Someone said: " + msg.MessageString);
    }

    private void OnClientConnected(object sender, TcpClient e)
    {
        var listener = (SimpleTcpServer)sender;
        listener.BroadcastLine($"{e.Client.RemoteEndPoint} connected");
        _logger.LogInformation("Client connected: {@RemoteEndPoint}", e.Client.RemoteEndPoint);
    }
}