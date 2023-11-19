using System.Text;
using Business.Context.Resources;
using Core.Auth;
using Core.Command;
using Core.Context;
using ZConsole.Service;

namespace LocalChat.Command;

public class CommandExecutor : ICommandExecutor
{
    private readonly IEnumerable<ICommand> _commands;
    private readonly ILoggerService _loggerService;
    private readonly IUserContext<UserContextResource> _userContext;

    public CommandExecutor(IEnumerable<ICommand> commands,
        ILoggerService loggerService,
        IUserContext<UserContextResource> userContext)
    {
        _loggerService = loggerService;
        _commands = commands;
        _userContext = userContext;
    }

    public void Execute(string commandName, params object[] args)
    {
        var command = _commands.FirstOrDefault(c => c.Name == commandName || c.Aliases.Contains(commandName));

        if (command == null)
        {
            _loggerService.LogError($"Command '{commandName}' not found.");
            return;
        }

        switch (command.AuthenticationRequirement)
        {
            case AuthenticationRequirement.Authenticated
                when !_userContext.IsAuthenticated():
                _loggerService.LogError("You must be authenticated to execute this command.");
                return;
            case AuthenticationRequirement.Unauthenticated
                when _userContext.IsAuthenticated():
                _loggerService.LogError("You are already authenticated.");
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