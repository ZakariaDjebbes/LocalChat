namespace Core.Context;

public interface IUserContext : IContext<UserContextResource>
{
    public new UserContextResource ContextResource { get; set; }

    bool IsAuthenticated();
}