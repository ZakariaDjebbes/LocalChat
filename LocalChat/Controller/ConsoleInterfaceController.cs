using Core.Model;
using Core.Service;
using Microsoft.Extensions.Hosting;

namespace LocalChat.Controller;

public class ConsoleInterfaceController : IHostedService
{
    private readonly IConsoleService _consoleService;
    private readonly IServerService _serverService;
    private readonly IClientService _clientService;

    public ConsoleInterfaceController(IConsoleService consoleService,
        IServerService serverService,
        IClientService clientService)
    {
        _consoleService = consoleService;
        _serverService = serverService;
        _clientService = clientService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _consoleService.LogInfo("My Console App started. Enter 'exit' to quit.");
        _serverService.Initialize(new Server { Port = 5000, Address = "127.0.0.1", Name = "My Server" });
        Task.Run(() => _serverService.Start(), cancellationToken);
        _clientService.Connect("127.0.0.1", 5000);

        while (true)
        {
            var input = _consoleService.ReadLine();
            if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
                break;
            _clientService.Send(input);
        }

        _consoleService.LogInfo("My Console App stopped.");

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}