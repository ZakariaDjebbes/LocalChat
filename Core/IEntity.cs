namespace Core;

/// <summary>
/// Interface for all entities.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// The unique identifier of the entity.
    /// </summary>
    public int Id { get; init; }
}