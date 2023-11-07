using ZConsole.Service;

namespace ZConsole.Implementation;

public class ConsolePromptService : IConsolePromptService
{
    private readonly IConsoleService _consoleService;
    
    public ConsolePromptService(IConsoleService consoleService)
    {
        _consoleService = consoleService;
    }
    
    public string Prompt(string promptMessage)
    {
        _consoleService.Log(promptMessage, false);
        return _consoleService.ReadLine();
    }

    public string PromptLine(string promptMessage)
    {
        _consoleService.Log(promptMessage, true);
        return _consoleService.ReadLine();
    }

    public string Password(string promptMessage)
    {
        _consoleService.Log(promptMessage, false);
        return _consoleService.ReadPassword();
    }

    public string PasswordLine(string promptMessage)
    {
        _consoleService.Log(promptMessage, true);
        return _consoleService.ReadPassword();
    }

    public string PromptOrDefault(string promptMessage)
    {
        _consoleService.Log(promptMessage, false);
        var input = _consoleService.ReadLine();
        return string.IsNullOrWhiteSpace(input) ? "" : input;
    }

    public string PromptPasswordOrDefault(string promptMessage)
    {
        _consoleService.Log(promptMessage, false);
        var input = _consoleService.ReadPassword();
        return string.IsNullOrWhiteSpace(input) ? "" : input;
    }

    public T Prompt<T>(string promptMessage)
    {
        _consoleService.Log(promptMessage, false);
        var input = _consoleService.ReadLine();
        return (T) Convert.ChangeType(input, typeof(T));
    }

    public T PromptOrDefault<T>(string promptMessage)
    {
        _consoleService.Log(promptMessage, false);
        var input = _consoleService.ReadLine();
        return string.IsNullOrWhiteSpace(input) ? default : (T) Convert.ChangeType(input, typeof(T));
    }

    public T Prompt<T>(string promptMessage, Func<string, T> converter)
    {
        _consoleService.Log(promptMessage, false);
        var input = _consoleService.ReadLine();
        return converter(input);
    }

    public T PromptOrDefault<T>(string promptMessage, Func<string, T> converter)
    {
        _consoleService.Log(promptMessage, false);
        var input = _consoleService.ReadLine();
        return string.IsNullOrWhiteSpace(input) ? default : converter(input);
    }

    public int Choose(string promptMessage, IEnumerable<string> choices, bool keepPrompt = false)
    {
        var selectedIndex = 0;
        var selectionMade = false;
        var choicesArray = choices.ToArray();

        do
        {
            _consoleService.Clear();
            _consoleService.Log(promptMessage);

            for (var i = 0; i < choicesArray.Length; i++)
            {
                _consoleService.LogSuccess(i == selectedIndex ? "> " : "  ", false);
                _consoleService.Log(choicesArray.ElementAt(i));
            }

            var keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow: case ConsoleKey.Z:
                    selectedIndex = Math.Max(0, selectedIndex - 1);
                    break;
                case ConsoleKey.DownArrow: case ConsoleKey.S:
                    selectedIndex = Math.Min(choicesArray.Length - 1, selectedIndex + 1);
                    break;
                case ConsoleKey.Enter:
                    selectionMade = true;
                    break;
            }
        } while (!selectionMade);
        
        if(!keepPrompt)
            _consoleService.Clear();
        
        return selectedIndex;
        
    }

    public int Choose(string promptMessage, params string[] choices)
        => Choose(promptMessage, choices.AsEnumerable());
}