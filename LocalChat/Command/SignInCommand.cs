using Core.Auth;
using Core.Command;
using Core.Context;
using Core.Service;
using ZConsole.Service;

namespace LocalChat.Command;

public class SignInCommand : ICommand
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IConsolePromptService _consolePromptService;

    private readonly IConsoleService _consoleService;
    private readonly IUserContext _userContext;

    public SignInCommand(IConsoleService consoleService,
        IConsolePromptService consolePromptService,
        IAuthenticationService authenticationService,
        IUserContext userContext)
    {
        Name = "sign-in";
        Description = "Signs in a user.";
        Aliases = new[] { "login" };
        AuthenticationRequirement = AuthenticationRequirement.Unauthenticated;

        _consoleService = consoleService;
        _consolePromptService = consolePromptService;
        _authenticationService = authenticationService;
        _userContext = userContext;
    }

    public string Name { get; }
    public string Description { get; }
    public string[] Aliases { get; }
    public AuthenticationRequirement AuthenticationRequirement { get; }

    public void Execute(params object[] args)
    {
        var username = _consolePromptService.Prompt("Username: ");
        var password = _consolePromptService.Password("Password: ");

        var signInResult = _authenticationService.SignIn(username, password);

        if (!signInResult.Succeeded)
        {
            signInResult.Errors.ToList().ForEach(e => _consoleService.LogError(e));
            return;
        }

        _userContext.Set(new UserContextResource(signInResult.User, signInResult.Token));
        _consoleService.LogSuccess($"User {username} signed in successfully.");
    }
}