using Core.Service;

namespace Business.Service;

public class ConsoleService : IConsoleService
{
    public string ReadLine() => Console.ReadLine();
    
    public void LogCustom(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void Log(string message) => Console.WriteLine(message);

    public void LogInfo(string message) => LogCustom(message, ConsoleColor.Blue);
    
    public void LogSuccess(string message) => LogCustom(message, ConsoleColor.Green);
    
    public void LogWarning(string message) => LogCustom(message, ConsoleColor.Yellow);

    public void LogError(string message) => LogCustom(message, ConsoleColor.Red);

    public void LogCritical(string message) => LogCustom(message, ConsoleColor.Magenta);
}