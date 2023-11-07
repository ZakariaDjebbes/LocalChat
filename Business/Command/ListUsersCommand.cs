using Core.Command;
using Core.Model;
using Core.Repository;
using ZConsole.Service;

namespace Business.Command;

public class ListUsersCommand : ICommand
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string[] Aliases { get; init; }
    
    private readonly IConsoleService _consoleService;
    private readonly IRepository<User> _userRepository;
    
    public ListUsersCommand(IConsoleService consoleService, IRepository<User> userRepository)
    {
        Name = "list-users";
        Description = "Lists all users.";
        Aliases = new[] { "list", "users" };
        _consoleService = consoleService;
        _userRepository = userRepository;
    }
    
    public void Execute(params object[] args)
    {
        var users = _userRepository.GetAll();
        foreach (var user in users)
        {
            _consoleService.LogInfo($"Username: {user.Username}, Password: {user.Password}");
        }
    }
}