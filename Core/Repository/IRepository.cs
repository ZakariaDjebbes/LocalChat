using System.Linq.Expressions;
using Core.Model;

namespace Core.Repository;

/// <summary>
///     Marks a class as a repository for a given <see cref="IEntity" />
/// </summary>
/// <typeparam name="T">
///     The type of <see cref="IEntity" /> that this repository is for
/// </typeparam>
public interface IRepository<T> where T : IEntity
{
    /// <summary>
    ///     Gets all entities of type <typeparamref name="T" />
    /// </summary>
    /// <returns>
    ///     A collection of all entities of type <typeparamref name="T" />
    /// </returns>
    IEnumerable<T> GetAll();

    /// <summary>
    ///     Gets all entities of type <typeparamref name="T" /> with the given include properties
    /// </summary>
    /// <param name="includeProperties">
    ///     The properties to include in the query
    /// </param>
    /// <returns>
    ///     A collection of all entities of type <typeparamref name="T" /> with the given include properties
    /// </returns>
    public IEnumerable<T> GetAllWithInclude(params Expression<Func<T, object>>[] includeProperties);

    /// <summary>
    ///    Gets all entities of type <typeparamref name="T" /> with the given include properties
    /// </summary>
    /// <param name="includeProperties">
    ///    The properties to include in the query
    /// </param>
    /// <returns>
    ///   A collection of all entities of type <typeparamref name="T" /> with the given include properties
    /// </returns>
    public IEnumerable<T> GetAllWithInclude(params string[] includeProperties);
    
    /// <summary>
    ///     Gets an entity of type <typeparamref name="T" /> by its ID
    /// </summary>
    /// <param name="id">The ID of the entity to get</param>
    /// <returns>
    ///     The entity with the given ID, or <see langword="null" /> if no such entity exists
    /// </returns>
    T GetById(Guid id);

    /// <summary>
    ///     Adds a new entity of type <typeparamref name="T" /> to the repository
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>The added entity</returns>
    void Add(T entity);

    /// <summary>
    ///     Updates an existing entity of type <typeparamref name="T" /> in the repository
    /// </summary>
    /// <param name="entity">The entity to update</param>
    /// <returns>The updated entity</returns>
    void Update(T entity);

    /// <summary>
    ///     Deletes an existing entity of type <typeparamref name="T" /> from the repository
    /// </summary>
    /// <param name="entity">The entity to delete</param>
    void Delete(T entity);

    /// <summary>
    ///     Commits all changes to the repository
    /// </summary>
    /// <returns>
    ///     The number of entities that were changed
    /// </returns>
    int Commit();
}