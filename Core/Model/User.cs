namespace Core.Model;

/// <summary>
///     A user of the application.
/// </summary>
public class User : IEntity
{
    /// <summary>
    ///     The username of the user.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    ///     The hashed password of the user.
    /// </summary>
    public string PasswordHash { get; set; }

    /// <summary>
    ///     The salt used to hash the password.
    /// </summary>
    public string PasswordSalt { get; set; }

    public ICollection<UserRoleInServer> UserRolesInServers { get; set; } = new List<UserRoleInServer>();
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}