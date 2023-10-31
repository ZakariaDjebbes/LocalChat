namespace Core.Model;

public class User : IEntity
{
    public int Id { get; init; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<Role> Roles { get; init; } = new List<Role>();
}