using System.Text;
using ZConsole.Service;

namespace ZConsole.Implementation;

public class ConsoleService : IConsoleService
{
    public string ReadLine() => Console.ReadLine() ?? string.Empty;

    public string ReadPassword(char mask = '*')
    {
        var pwd = new StringBuilder();
        while (true)
        {
            var i = Console.ReadKey(true);
            if (i.Key == ConsoleKey.Enter)
            {
                break;
            }

            if (i.Key == ConsoleKey.Backspace)
            {
                if (pwd.Length <= 0) continue;
                pwd.Length--; // Remove last char
                Log("\b \b", false);
            }
            // KeyChar == '\u0000' if the key pressed does not correspond to a printable character, e.g. F1, Pause-Break, etc
            else if (i.KeyChar != '\u0000')
            {
                pwd.Append(i.KeyChar); // Append char
                Log(mask.ToString(), false);
            }
        }

        BreakLine();
        return pwd.ToString();
    }

    public void LogCustom(string message, ConsoleColor color, bool newLine = true)
    {
        Console.ForegroundColor = color;

        if (newLine)
            Console.WriteLine(message);
        else
            Console.Write(message);

        Console.ResetColor();
    }

    public void Log(string message, bool newLine = true)
        => LogCustom(message, ConsoleColor.White, newLine);

    public void BreakLine()
        => Console.WriteLine();

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