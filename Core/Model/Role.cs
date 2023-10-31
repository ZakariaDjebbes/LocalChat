namespace Core.Model;

public class Role : IEntity
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public ICollection<User> Users { get; set; } = new List<User>();
}