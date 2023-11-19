namespace Core.Context;

/// <summary>
///     The user context interface as a singleton for the current user.
/// </summary>
public interface IUserContext<T> : IContext<T> where T : DisposableResource
{
    /// <summary>
    ///     Tests if the user is authenticated.
    /// </summary>
    /// <returns>
    ///     True if the user is authenticated. False otherwise.
    /// </returns>
    bool IsAuthenticated();
}