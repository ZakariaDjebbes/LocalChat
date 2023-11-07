namespace Core.Command;

/// <summary>
///   A service that provides command execution functionality.
/// </summary>
public interface ICommandExecutor
{
    /// <summary>
    ///  Executes the specified command with the specified arguments.
    /// </summary>
    /// <param name="commandName">
    /// The name of the command to execute.
    /// </param>
    /// <param name="args">
    /// The arguments to pass to the command.
    /// </param>
    public void Execute(string commandName, params object[] args);
}