using Core.Model;

namespace Core.Service;

/// <summary>
/// Token service interface, used to generate and validate tokens.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates a token for the given username and roles.
    /// </summary>
    /// <param name="username">
    /// The username of the user to generate the token for.
    /// </param>
    /// <param name="roles">
    /// The roles of the user to generate the token for.
    /// </param>
    /// <returns>
    /// The generated token.
    /// </returns>
    string GenerateToken(string username, IEnumerable<string> roles);
    
    /// <summary>
    /// Validates the given token.
    /// </summary>
    /// <param name="token">
    /// The token to validate.
    /// </param>
    /// <returns>
    /// Whether the token is valid or not.
    /// </returns>
    bool ValidateToken(string token);
}