// ReSharper disable CheckNamespace

using Core.Model;

namespace Core.Context;

public interface IServerContext : IContext<ServerContextResource>
{
    public Task Start(Server server);
    public Task Stop(Server server);
}