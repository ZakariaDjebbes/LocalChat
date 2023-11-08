using Core.Auth;
using Core.Command;
using Core.Service;
using ZConsole.Service;

namespace LocalChat.Command;

public class SignUpCommand : ICommand
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IConsolePromptService _consolePromptService;

    private readonly IConsoleService _consoleService;

    public SignUpCommand(IConsoleService consoleService,
        IConsolePromptService consolePromptService,
        IAuthenticationService authenticationService)
    {
        Name = "sign-up";
        Description = "Signs up a new user.";
        Aliases = new[] { "register" };
        AuthenticationRequirement = AuthenticationRequirement.Unauthenticated;
        _consoleService = consoleService;
        _consolePromptService = consolePromptService;
        _authenticationService = authenticationService;
    }

    public string Name { get; }
    public string Description { get; }
    public string[] Aliases { get; }
    public AuthenticationRequirement AuthenticationRequirement { get; }

    public void Execute(params object[] args)
    {
        var username = _consolePromptService.Prompt("Choose a username : ");
        var password = _consolePromptService.Password("Choose a password : ");
        var passwordConfirmation = _consolePromptService.Password("Confirm your password : ");

        if (password != passwordConfirmation)
        {
            _consoleService.LogError("Passwords do not match.");
            return;
        }

        var signUpResult = _authenticationService.SignUp(new SignUpResource(username, password));

        if (!signUpResult.Succeeded)
        {
            signUpResult.Errors.ToList().ForEach(e => _consoleService.LogError(e));
            return;
        }

        _consoleService.LogSuccess($"User {username} registered successfully.");
        _consoleService.LogInfo("You can now log in using the 'login' command.");
    }
}