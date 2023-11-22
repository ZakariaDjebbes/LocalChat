using Core.Context;
using SimpleTCP;

namespace Business.Context.Resources;

public class ClientContextResource : DisposableResource
{
    public SimpleTcpClient Client { get; init; }

    protected override void Dispose(bool disposing)
    {
        if (!disposing) return;

        Client?.Dispose();
    }
}