using Core.Command;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZConsole.Service;

namespace LocalChat.Controller;

public class ConsoleInterfaceHost : IHostedService
{
    private readonly IConsolePromptService _consolePromptService;
    private readonly IConsoleService _consoleService;
    private readonly ICommandExecutor _commandExecutor;
    
    public ConsoleInterfaceHost(IServiceProvider serviceProvider)
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
            try
            {
                var input = _consolePromptService.Prompt("[LocalChat#guest]>");
                if (input == "help") _consoleService.Log(_commandExecutor.ToString());
                else if(input == "exit") break;
                else _commandExecutor.Execute(input);
            }
            catch (Exception e)
            {
                _consoleService.LogError(e.Message);
            }
        }
        
        _consoleService.LogInfo("Terminating...");
        Environment.Exit(0);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}