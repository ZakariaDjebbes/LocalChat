using Business.Context.Resources;
using Core.Context;

namespace Business.Context;

public class ConsoleContext : IContext<ConsoleContextResource>
{
    public ConsoleContext()
    {
        ContextId = Guid.NewGuid();
        ContextResource = new ConsoleContextResource();
    }

    public Guid ContextId { get; init; }
    public ConsoleContextResource ContextResource { get; set; }

    public void Clear()
    {
        ContextResource.Dispose();
    }

    public void Set(ConsoleContextResource data)
    {
        ContextResource = data ?? throw new ArgumentNullException(nameof(data));
    }
}