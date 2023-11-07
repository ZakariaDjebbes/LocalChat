using Core.Model;

namespace Core.Auth;

public record SignInResult(bool Succeeded, string Token, User User, IEnumerable<string> Errors);