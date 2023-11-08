using Core.Auth;
using Core.Command;
using Core.Context;
using Core.Model;
using Core.Repository;
using ZConsole.Service;

namespace LocalChat.Command;

public class CreateServerCommand : ICommand
{
    private readonly IConsolePromptService _consolePromptService;
    private readonly IConsoleService _consoleService;
    private readonly IRepository<Role> _roleRepository;

    private readonly IRepository<Server> _serverRepository;
    private readonly IUserContext _userContext;

    public CreateServerCommand(IRepository<Server> serverRepository,
        IRepository<Role> roleRepository,
        IConsoleService consoleService,
        IConsolePromptService consolePromptService,
        IUserContext userContext)
    {
        Name = "create-server";
        Description = "Create a server for the current user";
        Aliases = new[] { "cs" };
        AuthenticationRequirement = AuthenticationRequirement.Authenticated;

        _serverRepository = serverRepository;
        _roleRepository = roleRepository;
        _consoleService = consoleService;
        _consolePromptService = consolePromptService;
        _userContext = userContext;
    }

    public string Name { get; }
    public string Description { get; }
    public string[] Aliases { get; }
    public AuthenticationRequirement AuthenticationRequirement { get; }

    public void Execute(params object[] args)
    {
        var serverName = _consolePromptService.Prompt("Server name: ");
        var serverAddress = _consolePromptService.Prompt("Server host: ");
        var serverPort = _consolePromptService.Prompt<int>("Server port: ");

        var server = CreateServer(serverName, serverAddress, serverPort);

        _consoleService.LogSuccess($"Server {server.Name} created successfully!");
    }

    private Server CreateServer(string serverName, string serverAddress, int serverPort)
    {
        var user = _userContext.ContextResource.User;
        var ownerRole = _roleRepository.GetAll().First(x => x.Name == "Owner");

        var server = new Server
        {
            Name = serverName,
            Address = serverAddress,
            Port = serverPort,
            UserRolesInServers = new List<UserRoleInServer>
            {
                new()
                {
                    RoleId = ownerRole.Id,
                    UserId = user.Id
                }
            }
        };

        _serverRepository.Add(server);
        _serverRepository.Commit();
        return server;
    }
}