using Core.Command;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZConsole.Service;

namespace LocalChat.Controller;

public class ConsoleInterfaceHost : IHostedService
{
    private readonly ICommandExecutor _commandExecutor;
    private readonly IPromptService _promptService;
    private readonly ILoggerService _loggerService;

    public ConsoleInterfaceHost(IServiceProvider serviceProvider)
    {
        _promptService = serviceProvider.GetRequiredService<IPromptService>();
        _loggerService = serviceProvider.GetRequiredService<ILoggerService>();
        _commandExecutor = serviceProvider.GetRequiredService<ICommandExecutor>();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _loggerService.LogInfo("Welcome to LocalChat!");
        _loggerService.LogInfo("Type 'help' to see a list of commands.");
        
        var choices = _promptService.ChooseValues(
            "What would you like to do?",
            "Start a new chat",
            "Join an existing chat",
            "Exit"
        );
    
        choices.ToList().ForEach(_loggerService.LogInfo);
        
        while (true)
            try
            {
                var input = _promptService.Prompt("[LocalChat#guest]>");
                if (input == "help") _loggerService.Log(_commandExecutor.ToString());
                else if (input == "exit") break;
                else _commandExecutor.Execute(input);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

        _loggerService.LogInfo("Terminating...");
        Environment.Exit(0);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}