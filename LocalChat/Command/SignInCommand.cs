using Core.Auth;
using Core.Command;
using Core.Context;
using Core.Service;
using ZConsole.Service;

namespace LocalChat.Command;

public class SignInCommand : ICommand
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IPromptService _promptService;
    private readonly ILoggerService _loggerService;
    private readonly IUserContext _userContext;

    public SignInCommand(ILoggerService loggerService,
        IPromptService promptService,
        IAuthenticationService authenticationService,
        IUserContext userContext)
    {
        Name = "sign-in";
        Description = "Signs in a user.";
        Aliases = new[] { "login" };
        AuthenticationRequirement = AuthenticationRequirement.Unauthenticated;

        _loggerService = loggerService;
        _promptService = promptService;
        _authenticationService = authenticationService;
        _userContext = userContext;
    }

    public string Name { get; }
    public string Description { get; }
    public string[] Aliases { get; }
    public AuthenticationRequirement AuthenticationRequirement { get; }

    public void Execute(params object[] args)
    {
        var username = _promptService.Prompt("Username: ");
        var password = _promptService.Password("Password: ");

        var signInResult = _authenticationService.SignIn(username, password);

        if (!signInResult.Succeeded)
        {
            signInResult.Errors.ToList().ForEach(e => _loggerService.LogError(e));
            return;
        }

        _userContext.Set(new UserContextResource(signInResult.User, signInResult.Token));
        _loggerService.LogSuccess($"User {username} signed in successfully.");
    }
}