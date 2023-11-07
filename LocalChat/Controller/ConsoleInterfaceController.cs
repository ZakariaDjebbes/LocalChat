using Core.Command;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZConsole.Service;

namespace LocalChat.Controller;

public class ConsoleInterfaceController : IHostedService
{
    private readonly IConsolePromptService _consolePromptService;
    private readonly IConsoleService _consoleService;
    private readonly ICommandExecutor _commandExecutor;
    
    public ConsoleInterfaceController(IServiceProvider serviceProvider)
    {
        _consolePromptService = serviceProvider.GetRequiredService<IConsolePromptService>();
        _consoleService = serviceProvider.GetRequiredService<IConsoleService>();
        _commandExecutor = serviceProvider.GetRequiredService<ICommandExecutor>();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _consoleService.LogInfo("Welcome to LocalChat!");
        _consoleService.LogInfo("Type 'help' to see a list of commands.");
        
        while (true)
        {
            var input = _consolePromptService.Prompt("[LocalChat#guest]>");
            if (input == "help") _consoleService.Log(_commandExecutor.ToString());
            else if(input == "exit") break;
            else _commandExecutor.Execute(input);
        }
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}