using System.Text;
using Core.Command;
using ZConsole.Service;

namespace Business.Command;

public class CommandExecutor : ICommandExecutor
{
    private readonly IEnumerable<ICommand> _commands;
    private readonly IConsoleService _consoleService;

    public CommandExecutor(IEnumerable<ICommand> commands, IConsoleService consoleService)
    {
        _consoleService = consoleService;
        _commands = commands;
    }

    public void Execute(string commandName, params object[] args)
    {
        var command = _commands.FirstOrDefault(c => c.Name == commandName || c.Aliases.Contains(commandName));

        if (command == null)
        {
            _consoleService.LogError($"Command '{commandName}' not found.");
            return;
        }

        command.Execute(args);
    }

    public IEnumerable<ICommand> GetCommands()
        => _commands;

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine("Available commands:");

        _commands
            .OrderBy(c => c.Name)
            .ToList()
            .ForEach(command =>
            {
                var aliases = string.Join(" ", command.Aliases);
                sb.AppendLine($"{command.Name} | {aliases} - {command.Description}");
            });

        sb.AppendLine("help - Displays this message.");

        return sb.ToString();
    }
}