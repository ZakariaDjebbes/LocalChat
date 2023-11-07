using Core.Command;
using Core.Model;
using Core.Repository;
using ZConsole.Service;

namespace Business.Command;

public class SignUpCommand : ICommand
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string[] Aliases { get; init; }

    // private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<User> _userRepository;
    private readonly IConsoleService _consoleService;
    private readonly IConsolePromptService _consolePromptService;

    public SignUpCommand(IConsoleService consoleService,
        IConsolePromptService consolePromptService,
        IRepository<User> userRepository)
    {
        Name = "sign-up";
        Description = "Signs up a new user.";
        Aliases = new[] { "register" };
        // _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _consoleService = consoleService;
        _consolePromptService = consolePromptService;
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

        var user = _userRepository.GetAll();
        if (user.Any(u => u.Username == username))
        {
            _consoleService.LogError("Username already exists.");
            return;
        }

        _userRepository.Add(new User
        {
            Username = username,
            Password = password
        });

        var res = _userRepository.Commit();
        _consoleService.LogSuccess("User created successfully. " + res);
    }
}