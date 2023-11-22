using Business.Context.Resources;
using Business.Context.State;
using Core.Auth;
using Core.Command;
using Core.Context;
using Core.Model;
using ZConsole.Service;

namespace LocalChat.Command.ClientCommands;

public class ConnectToServerCommand : ICommand
{
    private readonly IClientContext<ClientContextResource> _clientContext;
    private readonly IContext<ConsoleContextResource> _consoleContext;
    private readonly ILoggerService _loggerService;
    private readonly IPromptService _promptService;

    public ConnectToServerCommand(IClientContext<ClientContextResource> clientContext,
        IPromptService promptService,
        ILoggerService loggerService,
        IContext<ConsoleContextResource> consoleContext)
    {
        Name = "Connect";
        Description = "Connect to a server";
        Aliases = new[] { "connect", "c" };
        AuthenticationRequirement = AuthenticationRequirement.Authenticated;

        _clientContext = clientContext;
        _promptService = promptService;
        _loggerService = loggerService;
        _consoleContext = consoleContext;
    }

    public string Name { get; }
    public string Description { get; }
    public string[] Aliases { get; }
    public AuthenticationRequirement AuthenticationRequirement { get; }

    public void Execute(params object[] args)
    {
        ConnectUsingAddressAndPort();
    }

    private void ConnectUsingAddressAndPort()
    {
        var serverAddress = _promptService.Prompt("Enter server address: ");
        var serverPort = _promptService.Prompt("Enter server port: ");

        var server = new Server
        {
            Address = serverAddress,
            Port = int.Parse(serverPort)
        };

        var connected = _clientContext.Start(server);

        if (connected)
        {
            _clientContext.ContextResource.Client.DataReceived += (sender, args) =>
            {
                _loggerService.LogInfo(args.MessageString);
            };
            _consoleContext.ContextResource.ConsoleState = ConsoleState.Chat;
            _loggerService.LogInfo($"Connected to server {server.Name}");
        }
        else
        {
            _consoleContext.ContextResource.ConsoleState = ConsoleState.Command;
            _loggerService.LogError($"Could not connect to server {server.Name}");
        }
    }
}