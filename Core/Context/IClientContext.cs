using Core.Model;

namespace Core.Context;

public interface IClientContext<T> : IContext<T> where T : IDisposable
{
    public void Start(Server server);
}