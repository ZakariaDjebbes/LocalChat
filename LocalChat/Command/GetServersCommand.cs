using Core.Auth;
using Core.Command;
using Core.Context;
using Core.Model;
using Core.Repository;
using ZConsole.Service;

namespace LocalChat.Command;

public class GetServersCommand : ICommand
{
    private readonly ILoggerService _loggerService;
    private readonly IRepository<Server> _serverRepository;
    private readonly IUserContext _userContext;

    public GetServersCommand(IRepository<Server> serverRepository,
        ILoggerService loggerService,
        IUserContext userContext)
    {
        Name = "get-servers";
        Description = "Get the servers owned by the current user";
        Aliases = new[] { "gs" };
        AuthenticationRequirement = AuthenticationRequirement.Authenticated;

        _serverRepository = serverRepository;
        _loggerService = loggerService;
        _userContext = userContext;
    }

    public string Name { get; }
    public string Description { get; }
    public string[] Aliases { get; }
    public AuthenticationRequirement AuthenticationRequirement { get; }

    public void Execute(params object[] args)
    {
        var user = _userContext.ContextResource.User;
        var servers = GetServersOwnedByUser(user);

        _loggerService.LogSuccess($"User {user.Username} owns {servers.Count} servers: ");
        servers.ForEach(server => _loggerService.LogSuccess($"- {server.Name}@{server.Address}:{server.Port}"));
    }

    private List<Server> GetServersOwnedByUser(IEntity user)
    {
        var servers = _serverRepository.GetAllWithInclude(x =>
                x.UserRolesInServers,
            x => x.UserRolesInServers.Select(y => y.Role),
            x => x.UserRolesInServers.Select(y => y.User));

        var serversOfUser = servers.Where(x
            => x.UserRolesInServers.Any(y => y.UserId == user.Id && y.Role.Name == "Owner"));

        return serversOfUser.ToList();
    }
}