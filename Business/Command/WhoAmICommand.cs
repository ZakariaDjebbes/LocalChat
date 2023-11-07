using Core.Command;
using Core.Context;
using ZConsole.Service;

namespace Business.Command;

public class WhoAmICommand : ICommand
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string[] Aliases { get; init; }

    private readonly IUserContext _userContext;
    private readonly IConsoleService _consoleService;
    
    public WhoAmICommand(IUserContext userContext, IConsoleService consoleService)
    {
        Name = "whoami";
        Description = "Displays the current user.";
        Aliases = new[] { "me" };
        _userContext = userContext;
        _consoleService = consoleService;
    }
    
    public void Execute(params object[] args)
    {
        if (!_userContext.IsAuthenticated())
        {
            _consoleService.LogWarning("You are not authenticated currently.");
            return;
        }
        
        _consoleService.LogSuccess($"You are currently logged in as {_userContext.ContextResource.User.Username}.");
    }
}