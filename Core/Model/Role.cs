namespace Core.Model;

public class Role : IEntity
{
    public Guid Id { get; init; }
    /// <summary>
    /// The name of the role.
    /// </summary>
    public string Name { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    /// <summary>
    /// The users that have this role.
    /// </summary>
    public ICollection<User> Users { get; set; } = new List<User>();
}