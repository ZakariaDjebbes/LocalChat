using Core.Auth;
using Core.Command;
using Core.Context;
using Core.Service;
using ZConsole.Service;

namespace Business.Command;

public class SignUpCommand : ICommand
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string[] Aliases { get; init; }
    public AuthenticationRequirement AuthenticationRequirement { get; }

    private readonly IConsoleService _consoleService;
    private readonly IConsolePromptService _consolePromptService;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserContext _userContext;
    
    public SignUpCommand(IConsoleService consoleService,
        IConsolePromptService consolePromptService,
        IAuthenticationService authenticationService,
        IUserContext userContext)
    {
        Name = "sign-up";
        Description = "Signs up a new user.";
        Aliases = new[] { "register" };
        AuthenticationRequirement = AuthenticationRequirement.Unauthenticated;
        _consoleService = consoleService;
        _consolePromptService = consolePromptService;
        _authenticationService = authenticationService;
        _userContext = userContext;
    }

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