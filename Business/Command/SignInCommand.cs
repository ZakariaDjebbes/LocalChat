using Core.Command;
using Core.Context;
using Core.Service;
using ZConsole.Service;

namespace Business.Command;

public class SignInCommand : ICommand
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string[] Aliases { get; init; }

    private readonly IConsoleService _consoleService;
    private readonly IConsolePromptService _consolePromptService;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserContext _userContext;

    public SignInCommand(IConsoleService consoleService,
        IConsolePromptService consolePromptService,
        IAuthenticationService authenticationService,
        IUserContext userContext)
    {
        Name = "sign-in";
        Description = "Signs in a user.";
        Aliases = new[] { "login" };
        _consoleService = consoleService;
        _consolePromptService = consolePromptService;
        _authenticationService = authenticationService;
        _userContext = userContext;
    }

    public void Execute(params object[] args)
    {
        if (_userContext.IsAuthenticated())
        {
            _consoleService.LogWarning("You are already authenticated.");
            return;
        }
        
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