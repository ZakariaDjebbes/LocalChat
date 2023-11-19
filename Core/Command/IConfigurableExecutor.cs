namespace Core.Command;

/// <summary>
///     A service that provides command execution functionality.
/// </summary>
public interface IConfigurableExecutor : ICommandExecutor
{
    /// <summary>
    ///     Adds a command to the executor.
    /// </summary>
    /// <param name="command">
    ///     The command to add.
    /// </param>
    public void AddCommand(ICommand command);

    /// <summary>
    ///     Adds a collection of commands to the executor.
    /// </summary>
    /// <param name="commands">
    ///     The commands to add.
    /// </param>
    public void AddCommands(IEnumerable<ICommand> commands);

    /// <summary>
    ///     Removes a command from the executor.
    /// </summary>
    /// <param name="commandName">
    ///     The name of the command to remove.
    /// </param>
    public void RemoveCommand(string commandName);

    /// <summary>
    ///     Removes a collection of commands from the executor.
    /// </summary>
    /// <param name="commandNames">
    ///     The names of the commands to remove.
    /// </param>
    public void RemoveCommands(IEnumerable<string> commandNames);
}