using Business.Context.Resources;
using Business.Context.State;
using Core.Command;
using Core.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZConsole.Service;

namespace LocalChat.Hots;

public class ConsoleInterfaceHost : IHostedService
{
    private readonly IClientContext<ClientContextResource> _clientContext;
    private readonly ICommandExecutor _commandExecutor;
    private readonly IContext<ConsoleContextResource> _consoleContext;
    private readonly ILoggerService _loggerService;
    private readonly IPromptService _promptService;
    private readonly IUserContext<UserContextResource> _userContext;

    public ConsoleInterfaceHost(IServiceProvider serviceProvider)
    {
        _promptService = serviceProvider.GetRequiredService<IPromptService>();
        _loggerService = serviceProvider.GetRequiredService<ILoggerService>();
        _commandExecutor = serviceProvider.GetRequiredService<ICommandExecutor>();
        _userContext = serviceProvider.GetRequiredService<IUserContext<UserContextResource>>();
        _clientContext = serviceProvider.GetRequiredService<IClientContext<ClientContextResource>>();
        _consoleContext = serviceProvider.GetRequiredService<IContext<ConsoleContextResource>>();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _loggerService.LogInfo("Welcome to LocalChat!");
        _loggerService.LogInfo("Type 'help' to see a list of commands.");

        var running = true;

        do
        {
            try
            {
                switch (_consoleContext.ContextResource.ConsoleState)
                {
                    case ConsoleState.Command:
                        running = ConsoleInterface.RunCommandExecutor(_promptService,
                            _loggerService,
                            _userContext,
                            _commandExecutor);
                        break;
                    case ConsoleState.Chat:
                        running = ConsoleInterface.RunChat(_promptService,
                            _loggerService,
                            _userContext,
                            _clientContext);
                        break;
                    case ConsoleState.Server:
                        _loggerService.LogError("Server console not implemented yet.");
                        break;
                }
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }
        } while (running);

        _loggerService.LogInfo("Terminating...");
        Environment.Exit(0);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}