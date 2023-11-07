using Core.Auth;
using Core.Model;

namespace Core.Command;

/// <summary>
///   A command that can be executed to perform an action on either the server of the client.
/// </summary>
public interface ICommand
{
    /// <summary>
    ///  The name of the command.
    /// </summary>
    string Name { get; }
    /// <summary>
    /// A short description of the command.
    /// </summary>
    string Description { get; }
    /// <summary>
    /// A list of aliases for the command.
    /// </summary>
    string[] Aliases { get; }
    
    /// <summary>
    /// The <see cref="Auth.AuthenticationRequirement"/> required to execute the command.
    /// </summary>
    AuthenticationRequirement AuthenticationRequirement { get; }

    /// <summary>
    ///  Executes the command.
    /// </summary>
    /// <param name="args">
    /// The arguments passed to the command.
    /// </param>
    void Execute(params object[] args);
}