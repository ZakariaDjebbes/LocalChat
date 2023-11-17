using Core.Auth;
using Core.Command;
using Core.Context;
using Core.Model;
using Core.Repository;
using ZConsole.Service;

namespace LocalChat.Command;

public class StopServerCommand : ICommand
{
    private readonly ILoggerService _loggerService;
    private readonly IPromptService _promptService;
    private readonly IServerContext _serverContext;
    private readonly IRepository<Server> _serverRepository;
    private readonly IUserContext _userContext;
    
    public StopServerCommand(ILoggerService loggerService, 
        IPromptService promptService,
        IServerContext serverContext, 
        IRepository<Server> serverRepository, 
        IUserContext userContext)
    {
        Name = "stop-server";
        Description = "Stop a server";
        Aliases = null;
        AuthenticationRequirement = AuthenticationRequirement.Authenticated;
        
        _loggerService = loggerService;
        _promptService = promptService;
        _serverContext = serverContext;
        _serverRepository = serverRepository;
        _userContext = userContext;
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

        _loggerService.LogSuccess($"Stopping server {server.Name}...");
        
        _serverContext.Stop(server);
        
        _loggerService.LogSuccess($"Server {server.Name} stopped!");
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