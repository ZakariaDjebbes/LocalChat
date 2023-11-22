using Business.Context.Resources;
using Core.Command;
using Core.Context;
using ZConsole.Service;

namespace LocalChat;

public static class ConsoleInterface
{
    public static bool RunCommandExecutor(IPromptService promptService,
        ILoggerService loggerService,
        IUserContext<UserContextResource> userContext,
        ICommandExecutor commandExecutor)
    {
        var prompt = userContext.IsAuthenticated()
            ? userContext.ContextResource.User.Username
            : "Localchat";

        var input = promptService.Prompt($"[{prompt}]>");

        if (input == "help") loggerService.Log(commandExecutor.ToString());
        else if (input == "exit") return false;
        else commandExecutor.Execute(input);

        return true;
    }

    public static bool RunChat(IPromptService promptService,
        ILoggerService loggerService,
        IUserContext<UserContextResource> userContext,
        IClientContext<ClientContextResource> clientContext)
    {
        var input = promptService.Prompt(">");

        if (!userContext.IsAuthenticated())
        {
            loggerService.LogError("You are not logged in.");
            return false;
        }

        var clientTcp = clientContext.ContextResource.Client;

        if (clientTcp is null)
        {
            loggerService.LogError("You are not connected to a server.");
            return false;
        }


        clientTcp.WriteLine(input);
        return true;
    }
}