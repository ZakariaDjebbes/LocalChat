namespace Core;

/// <summary>
/// Interface for all entities.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// The unique identifier of the entity.
    /// </summary>
    public Guid Id { get; }
    /// <summary>
    /// The date and time the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; }
    /// <summary>
    /// The date and time the entity was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; }
}