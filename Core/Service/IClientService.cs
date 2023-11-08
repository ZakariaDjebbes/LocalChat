namespace Core.Service;

/// <summary>
///     Represents a service for the TCP client.
/// </summary>
public interface IClientService
{
    /// <summary>
    ///     Connects to the server.
    /// </summary>
    /// <param name="address">
    ///     The IP address of the server.
    /// </param>
    /// <param name="port">
    ///     The port of the server.
    /// </param>
    /// <returns>
    ///     True if the connection was successful, false otherwise.
    /// </returns>
    bool Connect(string address, int port);

    /// <summary>
    ///     Disconnects from the server.
    /// </summary>
    void Disconnect();

    /// <summary>
    ///     Sends a message to the server.
    /// </summary>
    /// <param name="message">
    ///     The message to send.
    /// </param>
    /// <returns>
    ///     True if the message was sent successfully, false otherwise.
    /// </returns>
    bool Send(string message);

    /// <summary>
    ///     Sends a message to the server.
    /// </summary>
    /// <param name="bytes">
    ///     The message to send.
    /// </param>
    /// <returns>
    ///     True if the message was sent successfully, false otherwise.
    /// </returns>
    bool Send(byte[] bytes);

    /// <summary>
    ///     Sends an object to the server.
    /// </summary>
    /// <param name="obj">
    ///     The object to send.
    /// </param>
    /// <returns>
    ///     True if the message was sent successfully, false otherwise.
    /// </returns>
    bool Send(object obj);

    /// <summary>
    ///     Receives a message from the server.
    /// </summary>
    /// <returns>
    ///     The message received.
    /// </returns>
    string Receive();
}