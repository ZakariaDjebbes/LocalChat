namespace Core.Model;

/// <summary>
///     Represents a server in the database.
/// </summary>
public class Server : IEntity
{
    /// <summary>
    ///     The name of the server.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    ///     The address IPV4 of the server.
    /// </summary>
    public string Address { get; init; }

    /// <summary>
    ///     The port of the server.
    /// </summary>
    public int Port { get; init; }

    /// <summary>
    ///     The status of the server.
    /// </summary>
    public bool IsRunning { get; set; }

    public ICollection<UserRoleInServer> UserRolesInServers { get; set; } = new List<UserRoleInServer>();
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}