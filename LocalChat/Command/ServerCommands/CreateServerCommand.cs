using Business.Context.Resources;
using Core.Auth;
using Core.Command;
using Core.Context;
using Core.Model;
using Core.Repository;
using ZConsole.Service;

namespace LocalChat.Command.ServerCommands;

public class CreateServerCommand : ICommand
{
    private readonly ILoggerService _loggerService;
    private readonly IPromptService _promptService;
    private readonly IRepository<Role> _roleRepository;

    private readonly IRepository<Server> _serverRepository;
    private readonly IUserContext<UserContextResource> _userContext;

    public CreateServerCommand(IRepository<Server> serverRepository,
        IRepository<Role> roleRepository,
        ILoggerService loggerService,
        IPromptService promptService,
        IUserContext<UserContextResource> userContext)
    {
        Name = "create-server";
        Description = "Create a server for the current user";
        Aliases = new[] { "cs" };
        AuthenticationRequirement = AuthenticationRequirement.Authenticated;

        _serverRepository = serverRepository;
        _roleRepository = roleRepository;
        _loggerService = loggerService;
        _promptService = promptService;
        _userContext = userContext;
    }

    public string Name { get; }
    public string Description { get; }
    public string[] Aliases { get; }
    public AuthenticationRequirement AuthenticationRequirement { get; }

    public void Execute(params object[] args)
    {
        var serverName = _promptService.Prompt("Server name: ");
        var serverAddress = _promptService.Prompt("Server host: ");
        var serverPort = _promptService.Prompt<int>("Server port: ");

        var server = CreateServer(serverName, serverAddress, serverPort);

        _loggerService.LogSuccess($"Server {server.Name} created successfully!");
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