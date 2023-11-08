using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Business.Service;

public class TokenService : ITokenService
{
    private readonly string _audience;
    private readonly string _issuer;
    private readonly string _secretKey;
    private readonly int _validityPeriodInMinutes;

    public TokenService(IConfiguration configuration)
    {
        _secretKey = configuration["Jwt:SecretKey"] ?? throw new ArgumentNullException(nameof(configuration));
        _issuer = configuration["Jwt:Issuer"] ?? throw new ArgumentNullException(nameof(configuration));
        _audience = configuration["Jwt:Audience"] ?? throw new ArgumentNullException(nameof(configuration));
        _validityPeriodInMinutes = Convert.ToInt32(configuration["Jwt:ValidityPeriodInMinutes"] ??
                                                   throw new ArgumentNullException(nameof(configuration)));
    }

    public string GenerateToken(string username)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, username)
        };

        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_validityPeriodInMinutes),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

            var validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = securityKey,
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = true,
                ValidAudience = _audience,
                ValidateLifetime = true, // Validate the token expiry
                LifetimeValidator = (_, expires, _, _) => expires.HasValue && DateTime.UtcNow <= expires.Value
            };

            tokenHandler.ValidateToken(token, validationParameters, out _);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
            Console.WriteLine(e.InnerException?.Message);
            return false;
        }
    }
}