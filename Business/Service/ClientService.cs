using System.Net;
using System.Net.Sockets;
using Core.Service;
using Microsoft.Extensions.Logging;

namespace Business.Service;

public class ClientService : IClientService
{
    private TcpClient _client;
    private NetworkStream _networkStream;
    private StreamWriter _writer;
    private StreamReader _reader;

    private readonly ILogger<ClientService> _logger;

    public ClientService(ILogger<ClientService> logger)
    {
        _logger = logger;
    }

    public bool Connect(string address, int port)
    {
        _client = new TcpClient();
        try
        {
            _client.Connect(IPAddress.Parse(address), port);
            _networkStream = _client.GetStream();
            _writer = new StreamWriter(_networkStream);
            _reader = new StreamReader(_networkStream);
            _logger.LogInformation("Connected to the server");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured during connection {Message}", e.Message);
            return false;
        }
    }

    public void Disconnect()
    {
        if (_client is null) return;
        _client.Close();
        _logger.LogInformation("Disconnected from the server");
    }

    public bool Send(string message)
    {
        try
        {
            if (_writer is null)
            {
                _logger.LogError("Client is not connected to the server");
                return false;
            }

            _writer.WriteLine(message);
            _writer.Flush();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error sending message: {Message}", e.Message);
            return false;
        }
    }

    public bool Send(byte[] bytes)
    {
        throw new NotImplementedException();
    }

    public bool Send(object obj)
    {
        throw new NotImplementedException();
    }

    public string Receive()
    {
        try
        {
            if (_reader is not null) return _reader.ReadLine();
            _logger.LogError("Client is not connected to the server");
            return null;
        }
        catch (Exception e)
        {
            _logger.LogError("Error receiving message: {Message}", e.Message);
            return null;
        }
    }
}