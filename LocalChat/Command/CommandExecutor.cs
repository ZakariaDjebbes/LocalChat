using System.Text;
using Core.Auth;
using Core.Command;
using Core.Context;
using ZConsole.Service;

namespace LocalChat.Command;

public class CommandExecutor : ICommandExecutor
{
    private readonly IEnumerable<ICommand> _commands;
    private readonly IConsoleService _consoleService;
    private readonly IUserContext _userContext;

    public CommandExecutor(IEnumerable<ICommand> commands,
        IConsoleService consoleService,
        IUserContext userContext)
    {
        _consoleService = consoleService;
        _commands = commands;
        _userContext = userContext;
    }

    public void Execute(string commandName, params object[] args)
    {
        var command = _commands.FirstOrDefault(c => c.Name == commandName || c.Aliases.Contains(commandName));

        if (command == null)
        {
            _consoleService.LogError($"Command '{commandName}' not found.");
            return;
        }

        switch (command.AuthenticationRequirement)
        {
            case AuthenticationRequirement.Authenticated when !_userContext.IsAuthenticated():
                _consoleService.LogError("You must be authenticated to execute this command.");
                return;
            case AuthenticationRequirement.Unauthenticated when _userContext.IsAuthenticated():
                _consoleService.LogError("You are already authenticated.");
                return;
            case AuthenticationRequirement.None:
            default:
                command.Execute(args);
                break;
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine("Available commands:");

        _commands
            .OrderBy(c => c.Name)
            .ToList()
            .ForEach(command =>
            {
                var aliases = string.Join(",", command.Aliases);
                sb.AppendLine($"    {command.Name} " +
                              $"Aliases=[{aliases}] " +
                              $"Auth=[{command.AuthenticationRequirement}] " +
                              $"Description=[{command.Description}]");
                sb.AppendLine();
            });

        sb.AppendLine("    help - Displays this message.");

        return sb.ToString();
    }
}