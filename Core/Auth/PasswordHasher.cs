using System.Security.Cryptography;
using System.Text;

namespace Core.Auth;

public static class PasswordHasher
{
    /// <summary>
    ///     Computes the hash of a password.
    /// </summary>
    /// <param name="password">
    ///     The password to hash.
    /// </param>
    /// <param name="salt">
    ///     The salt to hash.
    /// </param>
    /// <param name="pepper">
    ///     The pepper to hash.
    /// </param>
    /// <param name="iteration">
    ///     The number of iteration to hash.
    /// </param>
    /// <returns>
    ///     The hashed password.
    /// </returns>
    public static string ComputeHash(string password, string salt, string pepper, int iteration)
    {
        while (true)
        {
            if (iteration <= 0) return password;

            using var sha256 = SHA256.Create();
            var passwordSaltPepper = $"{password}{salt}{pepper}";
            var byteValue = Encoding.UTF8.GetBytes(passwordSaltPepper);
            var byteHash = sha256.ComputeHash(byteValue);
            var hash = Convert.ToBase64String(byteHash);
            password = hash;
            iteration -= 1;
        }
    }

    /// <summary>
    ///     Generates a salt.
    /// </summary>
    /// <returns>
    ///     The generated salt.
    /// </returns>
    public static string GenerateSalt()
    {
        using var rng = RandomNumberGenerator.Create();
        var byteSalt = new byte[16];
        rng.GetBytes(byteSalt);
        var salt = Convert.ToBase64String(byteSalt);
        return salt;
    }
}