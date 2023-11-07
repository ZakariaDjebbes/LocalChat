using Core.Auth;
using Core.Command;
using Core.Context;
using ZConsole.Service;

namespace Business.Command;

public class WhoAmICommand : ICommand
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string[] Aliases { get; init; }
    public AuthenticationRequirement AuthenticationRequirement { get; }

    private readonly IUserContext _userContext;
    private readonly IConsoleService _consoleService;
    
    public WhoAmICommand(IUserContext userContext, IConsoleService consoleService)
    {
        Name = "whoami";
        Description = "Displays the current user.";
        Aliases = new[] { "me" };
        AuthenticationRequirement = AuthenticationRequirement.Authenticated;
        _userContext = userContext;
        _consoleService = consoleService;
    }
    
    public void Execute(params object[] args)
    {
        _consoleService.LogSuccess($"You are currently logged in as {_userContext.ContextResource.User.Username}.");
    }
}