namespace Core.Model;

/// <summary>
///     Represents a role of a <see cref="User" /> in a <see cref="Server" />.
/// </summary>
public class Role : IEntity
{
    /// <summary>
    ///     The name of the role.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    public ICollection<UserRoleInServer> UserRolesInServers { get; set; } = new List<UserRoleInServer>();
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}