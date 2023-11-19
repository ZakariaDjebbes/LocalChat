using Core.Auth;
using Core.Command;
using ZConsole.Service;

namespace LocalChat.Command;

public class ClearCommand : ICommand
{
    private readonly IConsoleService _consoleService;

    public ClearCommand(IConsoleService consoleService)
    {
        Name = "clear";
        Description = "Clear the console";
        Aliases = Array.Empty<string>();
        AuthenticationRequirement = AuthenticationRequirement.None;

        _consoleService = consoleService;
    }

    public string Name { get; }
    public string Description { get; }
    public string[] Aliases { get; }
    public AuthenticationRequirement AuthenticationRequirement { get; }

    public void Execute(params object[] args)
    {
        _consoleService.Clear();
    }
}