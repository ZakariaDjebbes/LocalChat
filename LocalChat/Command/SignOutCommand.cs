using Core.Auth;
using Core.Command;
using Core.Context;
using ZConsole.Service;

namespace LocalChat.Command;

public class SignOutCommand : ICommand
{
    private readonly ILoggerService _loggerService;
    private readonly IUserContext _userContext;

    public SignOutCommand(IUserContext userContext, 
        ILoggerService loggerService)
    {
        Name = "sign-out";
        Description = "Signs out the current user.";
        Aliases = new[] { "logout" };
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
        _userContext.Clear();
        _loggerService.LogSuccess("You have been signed out.");
    }
}