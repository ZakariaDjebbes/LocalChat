using Core.Auth;
using Core.Command;
using Core.Service;
using ZConsole.Service;

namespace LocalChat.Command;

public class SignUpCommand : ICommand
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ILoggerService _loggerService;
    private readonly IPromptService _promptService;

    public SignUpCommand(ILoggerService loggerService,
        IPromptService promptService,
        IAuthenticationService authenticationService)
    {
        Name = "sign-up";
        Description = "Signs up a new user.";
        Aliases = new[] { "register" };
        AuthenticationRequirement = AuthenticationRequirement.Unauthenticated;
        _loggerService = loggerService;
        _promptService = promptService;
        _authenticationService = authenticationService;
    }

    public string Name { get; }
    public string Description { get; }
    public string[] Aliases { get; }
    public AuthenticationRequirement AuthenticationRequirement { get; }

    public void Execute(params object[] args)
    {
        var username = _promptService.Prompt("Choose a username : ");
        var password = _promptService.Password("Choose a password : ");
        var passwordConfirmation = _promptService.Password("Confirm your password : ");

        if (password != passwordConfirmation)
        {
            _loggerService.LogError("Passwords do not match.");
            return;
        }

        var signUpResult = _authenticationService.SignUp(new SignUpResource(username, password));

        if (!signUpResult.Succeeded)
        {
            signUpResult.Errors.ToList().ForEach(e => _loggerService.LogError(e));
            return;
        }

        _loggerService.LogSuccess($"User {username} registered successfully.");
        _loggerService.LogInfo("You can now log in using the 'login' command.");
    }
}