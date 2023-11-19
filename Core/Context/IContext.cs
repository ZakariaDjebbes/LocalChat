namespace Core.Context;

/// <summary>
///     A context is a class that holds data that is relevant to the current state of the application.
/// </summary>
/// <typeparam name="T">
///     The type of data that is held by the context.
/// </typeparam>
public interface IContext<T> where T : IDisposable
{
    /// <summary>
    ///     The unique identifier of the context.
    /// </summary>
    public Guid ContextId { get; init; }

    /// <summary>
    ///     The data that is held by the context.
    /// </summary>
    public T ContextResource { get; protected set; }

    /// <summary>
    ///     Clears the data that is held by the context.
    /// </summary>
    void Clear();

    /// <summary>
    ///     Sets the data that is held by the context.
    /// </summary>
    /// <param name="data">
    ///     The data that is held by the context.
    /// </param>
    void Set(T data);
}