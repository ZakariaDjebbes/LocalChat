namespace Core.Model;

public class User : IEntity
{
    public Guid Id { get; init; }
    /// <summary>
    /// The username of the user.
    /// </summary>
    public string Username { get; set; } = string.Empty;
    /// <summary>
    /// The password of the user.
    /// </summary>
    public string Password { get; set; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    /// <summary>
    /// The roles of the user.
    /// </summary>
    public ICollection<Role> Roles { get; init; } = new List<Role>();
}