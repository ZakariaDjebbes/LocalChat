using Core.Context;
using Core.Service;

namespace Business.Context;

public class UserContext : IUserContext
{
    public Guid ContextId { get; init; }
    public UserContextResource ContextResource { get; set; }

    private readonly ITokenService _tokenService;

    public UserContext(ITokenService tokenService)
    {
        ContextId = Guid.NewGuid();
        _tokenService = tokenService;
    }

    public bool IsAuthenticated() =>
        ContextResource != null && !string.IsNullOrEmpty(ContextResource.Token) &&
        _tokenService.ValidateToken(ContextResource.Token);

    public void Clear()
        => ContextResource = null;


    public void Set(UserContextResource data)
    {
        ContextResource = data;
    }
}