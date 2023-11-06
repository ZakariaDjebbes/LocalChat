using ZConsole.Service;

namespace ZConsole.Implementation;

public class ConsoleService : IConsoleService
{
    public string ReadLine() => Console.ReadLine() ?? string.Empty;
    
    public void LogCustom(string message, ConsoleColor color, bool newLine = true)
    {
        Console.ForegroundColor = color;
        
        if(newLine)
            Console.WriteLine(message);
        else
            Console.Write(message);
        
        Console.ResetColor();
    }

    public void Log(string message, bool newLine = true)
        => LogCustom(message, ConsoleColor.White, newLine);

    public void LogInfo(string message, bool newLine = true) 
        => LogCustom(message, ConsoleColor.Blue, newLine);
    
    public void LogSuccess(string message, bool newLine = true) 
        => LogCustom(message, ConsoleColor.Green, newLine);
    
    public void LogWarning(string message, bool newLine = true) 
        => LogCustom(message, ConsoleColor.Yellow, newLine);

    public void LogError(string message, bool newLine = true)
        => LogCustom(message, ConsoleColor.Red, newLine);

    public void LogCritical(string message, bool newLine = true) 
        => LogCustom(message, ConsoleColor.Magenta, newLine);

    public void Clear() => Console.Clear();
}