using Core.Auth;
using Core.Model;
using Core.Repository;
using Core.Service;
using Microsoft.Extensions.Configuration;

namespace Business.Service;

public class AuthenticationService : IAuthenticationService
{
    private readonly int _iteration;
    private readonly string _pepper;
    private readonly ITokenService _tokenService;
    private readonly IRepository<User> _userRepository;

    public AuthenticationService(IRepository<User> userRepository,
        IConfiguration configuration,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _pepper = configuration["Auth:Pepper"] ?? string.Empty;
        _iteration = int.Parse(configuration["Auth:Iterations"] ?? throw new InvalidOperationException());
    }

    public SignUpResult SignUp(SignUpResource signUpResource)
    {
        var user = new User
        {
            Username = signUpResource.Username,
            PasswordSalt = PasswordHasher.GenerateSalt()
        };

        user.PasswordHash = PasswordHasher.ComputeHash(signUpResource.Password, user.PasswordSalt, _pepper, _iteration);
        _userRepository.Add(user);
        var changes = _userRepository.Commit();

        return changes == 0
            ? new SignUpResult(false, new[] { "An error occurred while signing up." })
            : new SignUpResult(true, null);
    }

    public SignInResult SignIn(string username, string password)
    {
        var user = _userRepository.GetAll()
            .FirstOrDefault(x => x.Username == username);

        if (user == null)
            return new SignInResult(false, null, null, new[] { "Username or password did not match." });


        var passwordHash = PasswordHasher.ComputeHash(password, user.PasswordSalt, _pepper, _iteration);

        if (user.PasswordHash != passwordHash)
            return new SignInResult(false, null, null, new[] { "Username or password did not match." });

        var token = _tokenService.GenerateToken(user.Username); // expires in 1 hour
        var tokenValid = _tokenService.ValidateToken(token);

        return tokenValid
            ? new SignInResult(true, token, user, null)
            : new SignInResult(false, null, null, new[] { "An error occurred while signing in." });
    }
}