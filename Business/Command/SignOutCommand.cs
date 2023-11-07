using Core.Auth;
using Core.Command;
using Core.Context;
using ZConsole.Service;

namespace Business.Command;

public class SignOutCommand : ICommand
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string[] Aliases { get; init; }
    public AuthenticationRequirement AuthenticationRequirement { get; }

    private readonly IUserContext _userContext;
    private readonly IConsoleService _consoleService;
    
    public SignOutCommand(IUserContext userContext, IConsoleService consoleService)
    {
        Name = "sign-out";
        Description = "Signs out the current user.";
        Aliases = new[] { "logout" };
        AuthenticationRequirement = AuthenticationRequirement.Authenticated;
        _userContext = userContext;
        _consoleService = consoleService;
    }
    
    public void Execute(params object[] args)
    {
        _userContext.Clear();
        _consoleService.LogSuccess("You have been signed out.");
    }
}