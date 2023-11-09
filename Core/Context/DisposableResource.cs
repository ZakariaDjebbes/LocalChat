namespace Core.Context;

public abstract class DisposableResource : IDisposable
{
    private bool _disposed;

    /// <summary>
    /// Disposes the current instance of the <see cref="UserContextResource"/> class.
    /// </summary>
    /// <param name="disposing">
    /// Indicates whether the current instance is disposing or not.
    /// </param>
    protected abstract void Dispose(bool disposing);
    
    /// <summary>
    /// Disposes the current instance of the <see cref="UserContextResource"/> class.
    /// </summary>
    public void Dispose()
    {
        if(_disposed) return;
        Dispose(true);
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}