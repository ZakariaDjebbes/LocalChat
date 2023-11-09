// ReSharper disable CheckNamespace
namespace Core.Context;

public interface IServerContext : IContext<ServerContextResource>
{
    public void Start();
    public void Stop();
}