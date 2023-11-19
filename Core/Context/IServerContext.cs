// ReSharper disable CheckNamespace

using Core.Model;

namespace Core.Context;

public interface IServerContext<T> : IContext<T> where T : IDisposable
{
    public void Start(Server server);
    public void Stop(Server server);
}