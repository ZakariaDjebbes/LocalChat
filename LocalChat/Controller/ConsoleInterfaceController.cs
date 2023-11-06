using Core.Model;
using Core.Repository;
using Core.Service;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZConsole.Service;

namespace LocalChat.Controller;

public class ConsoleInterfaceController : IHostedService
{
    private readonly IConsolePromptService _consolePromptService;
    private readonly IConsoleService _consoleService;
    private readonly IServerService _serverService;
    private readonly IUnitOfWork _unitOfWork;
    
    public ConsoleInterfaceController(IServiceProvider serviceProvider)
    {
        _consolePromptService = serviceProvider.GetRequiredService<IConsolePromptService>();
        _consoleService = serviceProvider.GetRequiredService<IConsoleService>();
        _serverService = serviceProvider.GetRequiredService<IServerService>();
        _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _consoleService.LogInfo("Welcome to LocalChat!");
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}