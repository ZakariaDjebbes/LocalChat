using Business.Context.Resources;
using Core.Auth;
using Core.Command;
using Core.Context;
using Core.Model;
using Core.Repository;
using ZConsole.Service;

namespace LocalChat.Command.ServerCommands;

public class StartServerCommand : ICommand
{
    private readonly ILoggerService _loggerService;
    private readonly IPromptService _promptService;
    private readonly IServerContext<ServerContextResource> _serverContext;
    private readonly IRepository<Server> _serverRepository;
    private readonly IUserContext<UserContextResource> _userContext;

    public StartServerCommand(IRepository<Server> serverRepository,
        ILoggerService loggerService,
        IPromptService promptService,
        IUserContext<UserContextResource> userContext,
        IServerContext<ServerContextResource> serverContext)
    {
        Name = "start-server";
        Description = "Start a server";
        Aliases = new[] { "ss" };
        AuthenticationRequirement = AuthenticationRequirement.Authenticated;

        _serverRepository = serverRepository;
        _loggerService = loggerService;
        _promptService = promptService;
        _userContext = userContext;
        _serverContext = serverContext;
    }

    public string Name { get; }
    public string Description { get; }
    public string[] Aliases { get; }
    public AuthenticationRequirement AuthenticationRequirement { get; }

    public void Execute(params object[] args)
    {
        var user = _userContext.ContextResource.User;
        var servers = GetServersOwnedByUser(user).ToList();

        var serverIndex = _promptService.Choose("Which Server to start?",
            servers.Select(x => x.Name));

        var server = servers[serverIndex];

        _loggerService.LogSuccess($"Starting server {server.Name}...");

        _serverContext.Start(server);

        _loggerService.LogSuccess($"Server {server.Name} started!");
    }

    private IEnumerable<Server> GetServersOwnedByUser(IEntity user)
    {
        var servers = _serverRepository.GetAllWithInclude(
            "UserRolesInServers",
            "UserRolesInServers.Role",
            "UserRolesInServers.User");

        var serversOfUser = servers.Where(x
            => x.UserRolesInServers.Any(y => y.UserId == user.Id && y.Role.Name == "Owner"));

        return serversOfUser.ToList();
    }
}