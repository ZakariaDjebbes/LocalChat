namespace Core.Repository;

/// <summary>
///   A unit of work that provides a way to commit changes to the database.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Gets the repository for the specified entity.
    /// If the repository does not exist, it is created.
    /// Otherwise, the existing repository is returned.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the <see cref="IEntity"/>.
    /// </typeparam>
    /// <returns>
    /// The <see cref="IRepository{T}"/> for the specified entity.
    /// </returns>
    IRepository<T> GetRepository<T>() where T : class, IEntity;
    /// <summary>
    /// Commits all changes made to the database.
    /// </summary>
    /// <returns>
    /// The number of state entries written to the database.
    /// </returns>
    int Commit();
    /// <summary>
    /// Discards all changes made to the database.
    /// </summary>
    void Rollback();
}