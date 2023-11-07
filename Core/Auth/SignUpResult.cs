namespace Core.Auth;

/// <summary>
/// The result of a sign up.
/// </summary>
/// <param name="Succeeded">
/// Whether or not the sign up succeeded.
/// </param>
/// <param name="Error">
/// The errors that occurred during the sign up.
/// </param>
public record SignUpResult(bool Succeeded, IEnumerable<string> Errors);