namespace Core.Model;

/// <summary>
///     The role of a user in a server.
/// </summary>
public class UserRoleInServer : IEntity
{
    /// <summary>
    ///     The <see cref="Guid" /> of the user that has this role.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///     The <see cref="User" /> that has this role.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    ///     The <see cref="Guid" /> of the server that the user has a role in.
    /// </summary>
    public Guid ServerId { get; set; }

    /// <summary>
    ///     The <see cref="Server" /> that the user has a role in.
    /// </summary>
    public Server Server { get; set; }

    /// <summary>
    ///     The <see cref="Guid" /> of the role that the user has.
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    ///     The <see cref="Role" /> that the user has.
    /// </summary>
    public Role Role { get; set; }

    public Guid Id { get; init; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}