using Core.Context;
using Core.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Context;

public class UserContext : IUserContext
{
    private readonly ITokenService _tokenService;

    public UserContext(IServiceProvider serviceProvider)
    {
        ContextId = Guid.NewGuid();
        _tokenService = serviceProvider.GetRequiredService<ITokenService>();
    }

    public Guid ContextId { get; init; }
    public UserContextResource ContextResource { get; set; }

    public bool IsAuthenticated()
    {
        return ContextResource != null && !string.IsNullOrEmpty(ContextResource.Token) &&
               _tokenService.ValidateToken(ContextResource.Token);
    }

    public void Clear()
    {
        ContextResource = null;
    }


    public void Set(UserContextResource data)
    {
        ContextResource = data;
    }
}