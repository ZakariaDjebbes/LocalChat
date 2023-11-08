namespace Core.Auth;

/// <summary>
///     The resource containing the information of the user to sign up.
/// </summary>
/// <param name="Username">
///     The username of the user to sign up.
/// </param>
/// <param name="Password">
///     The password of the user to sign up.
/// </param>
public record SignUpResource(string Username, string Password);