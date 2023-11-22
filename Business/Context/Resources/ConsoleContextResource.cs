using Business.Context.State;
using Core.Context;

namespace Business.Context.Resources;

public class ConsoleContextResource : DisposableResource
{
    public ConsoleState ConsoleState { get; set; } = ConsoleState.Command;

    protected override void Dispose(bool disposing)
    {
        if (!disposing) return;
    }
}