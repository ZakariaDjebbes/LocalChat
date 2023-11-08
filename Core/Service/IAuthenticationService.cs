using Core.Auth;

namespace Core.Service;

/// <summary>
///     Authentication service interface
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    ///     Signs up a new user.
    /// </summary>
    /// <param name="signUpResource">
    ///     The resource containing the information of the user to sign up.
    /// </param>
    /// <returns>The result of the sign up.</returns>
    SignUpResult SignUp(SignUpResource signUpResource);

    /// <summary>
    ///     Signs in a user.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <returns>The result of the sign in.</returns>
    SignInResult SignIn(string username, string password);
}