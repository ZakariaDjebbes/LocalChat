using System.Security;
using ZConsole.Service;

namespace ZConsole.Implementation;

public class ConsoleService : IConsoleService
{
    public string ReadLine() 
        => Console.ReadLine() ?? string.Empty;

    public string ReadPassword(char mask = '*')
    {
        var pwd = new SecureString();
        
        while (true)
        {
            var i = Console.ReadKey(true);
            if (i.Key == ConsoleKey.Enter) break;

            if (i.Key == ConsoleKey.Backspace)
            {
                if (pwd.Length <= 0) continue;
                pwd.RemoveAt(pwd.Length - 1); // Remove last char
                Write("\b \b", false);
            }
            // KeyChar == '\u0000' if the key pressed does not correspond
            // to a printable character, e.g. F1, Pause-Break, etc
            else if (i.KeyChar != '\u0000')
            {
                // Append char
                pwd.AppendChar(i.KeyChar);
                Write(mask.ToString(), false);
            }
        }

        BreakLine();
        return pwd.ToString();
    }

    public void WriteCustom(string message, ConsoleColor color, bool newLine = true)
    {
        Console.ForegroundColor = color;

        if (newLine)
            Console.WriteLine(message);
        else
            Console.Write(message);

        Console.ResetColor();
    }

    public void WriteCustom(IEnumerable<string> messages, IEnumerable<ConsoleColor> colors, bool newLine = true)
    {
        if (messages == null) throw new ArgumentNullException(nameof(messages));
        if (colors == null) throw new ArgumentNullException(nameof(colors));
        
        var messageArray = messages.ToArray();
        var colorArray = colors.ToArray();

        if (messageArray.Length != colorArray.Length)
            throw new ArgumentException("The number of messages and colors must be the same.");

        for (var i = 0; i < messageArray.Length; i++)
            WriteCustom(messageArray[i], colorArray[i], newLine);
    }

    public void Write(string message, bool newLine = true)
        => WriteCustom(message, ConsoleColor.White, newLine);

    public void BreakLine() 
        => Console.WriteLine();

    public void Clear()
        => Console.Clear();
}