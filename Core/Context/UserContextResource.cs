using Core.Model;

namespace Core.Context;

public class UserContextResource : IDisposable
{
    private bool _disposed;
    
    public UserContextResource(User user, string token)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        Token = token ?? throw new ArgumentNullException(nameof(token));
    }
    
    public User User { get; private set; }
    public string Token { get; private set; }
    
    private void Dispose(bool disposing)
    {
        if (_disposed) return;
        
        if (disposing)
        {
            User = null;
            Token = null;
        }
        
        _disposed = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}