using Core.Context;

namespace Business.Context.Resources;

public class ClientContextResource : DisposableResource
{
    
    
    protected override void Dispose(bool disposing)
    {
        if (!disposing) return;
        
    }
}