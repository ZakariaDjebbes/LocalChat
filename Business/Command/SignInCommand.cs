using Core.Command;
using Core.Model;
using Core.Repository;
using ZConsole.Service;

namespace Business.Command;

public class SignInCommand : ICommand
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string[] Aliases { get; init; }
    
    private readonly IConsoleService _consoleService;
    private readonly IConsolePromptService _consolePromptService;
    private readonly IRepository<User> _userRepository;
    
    public SignInCommand(IConsoleService consoleService,
        IConsolePromptService consolePromptService,
        IRepository<User> userRepository)
    {
        Name = "sign-in";
        Description = "Signs in a user.";
        Aliases = new[] { "login" };
        _userRepository = userRepository;
        _consoleService = consoleService;
        _consolePromptService = consolePromptService;
    }
    
    public void Execute(params object[] args)
    {
        var username = _consolePromptService.Prompt("Username: ");
        var password = _consolePromptService.Password("Password: ");
        
        var user = _userRepository
            .GetAll()
            .FirstOrDefault(u => u.Username == username && u.Password == password);
        
        if (user is null)
        {
            _consoleService.LogError("Invalid username or password.");
            return;
        }
        
        _consoleService.LogSuccess($"Welcome back, {user.Username}!");
    }
}