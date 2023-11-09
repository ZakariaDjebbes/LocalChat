using Core.Auth;
using Core.Command;
using Core.Context;
using ZConsole.Service;

namespace LocalChat.Command;

public class WhoAmICommand : ICommand
{
    private readonly ILoggerService _loggerService;

    private readonly IUserContext _userContext;

    public WhoAmICommand(IUserContext userContext,
        ILoggerService loggerService)
    {
        Name = "whoami";
        Description = "Displays the current user.";
        Aliases = new[] { "me" };
        AuthenticationRequirement = AuthenticationRequirement.Authenticated;
        _userContext = userContext;
        _loggerService = loggerService;
    }

    public string Name { get; }
    public string Description { get; }
    public string[] Aliases { get; }
    public AuthenticationRequirement AuthenticationRequirement { get; }

    public void Execute(params object[] args)
    {
        var user = _userContext.ContextResource.User;
        var message = $"You are currently logged in as {user.Username}.";
        _loggerService.LogSuccess(message);
    }
}