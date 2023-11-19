using Business.Context.Resources;
using Core.Auth;
using Core.Command;
using Core.Context;
using Core.Model;

namespace LocalChat.Command.ClientCommands;

public class ConnectToServerCommand : ICommand
{
    private readonly IClientContext<ClientContextResource> _clientContext;
    
    public ConnectToServerCommand(IClientContext<ClientContextResource> clientContext)
    {
        Name = "Connect";
        Description = "Connect to a server";
        Aliases = new[] {"connect", "c"};
        AuthenticationRequirement = AuthenticationRequirement.Authenticated;
        _clientContext = clientContext;
    }
    
    public string Name { get; }
    public string Description { get; }
    public string[] Aliases { get; }
    public AuthenticationRequirement AuthenticationRequirement { get; }
    
    public void Execute(params object[] args)
    {
        _clientContext.Start(new Server
        {
            Address = "127.0.0.1",
            Port = 25568
        });
    }
}