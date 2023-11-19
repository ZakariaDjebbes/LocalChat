using ZConsole.Service;

namespace ZConsole.Implementation;

public class PromptService : IPromptService
{
    private readonly IConsoleService _consoleService;

    public PromptService(IConsoleService consoleService)
    {
        _consoleService = consoleService;
    }

    /// <summary>
    ///     Whether to keep the prompt after the user has entered input.
    ///     Default is true.
    /// </summary>
    public bool KeepPrompt { get; set; } = true;

    /// <summary>
    ///     The color of the prompt.
    ///     Default is white.
    /// </summary>
    public ConsoleColor PromptColor { get; set; } = ConsoleColor.White;

    public T Prompt<T>(string promptMessage)
    {
        _consoleService.WriteCustom(promptMessage, PromptColor, false);
        var input = _consoleService.ReadLine();

        if (!KeepPrompt)
            _consoleService.Clear();

        return (T)Convert.ChangeType(input, typeof(T));
    }

    public bool TryPrompt<T>(string promptMessage, out T input)
    {
        _consoleService.WriteCustom(promptMessage, PromptColor, false);
        var inputString = _consoleService.ReadLine();

        if (!KeepPrompt)
            _consoleService.Clear();

        try
        {
            input = (T)Convert.ChangeType(inputString, typeof(T));
            return true;
        }
        catch
        {
            input = default;
            return false;
        }
    }

    public T PromptOrDefault<T>(string promptMessage)
    {
        _consoleService.WriteCustom(promptMessage, PromptColor, false);
        var input = _consoleService.ReadLine();

        if (!KeepPrompt)
            _consoleService.Clear();

        T result;

        try
        {
            result = string.IsNullOrWhiteSpace(input) ? default : (T)Convert.ChangeType(input, typeof(T));
        }
        catch
        {
            result = default;
        }

        return result;
    }

    public T Prompt<T>(string promptMessage, Func<string, T> converter)
    {
        _consoleService.WriteCustom(promptMessage, PromptColor, false);
        var input = _consoleService.ReadLine();

        if (!KeepPrompt)
            _consoleService.Clear();

        return converter(input);
    }

    public T PromptOrDefault<T>(string promptMessage, Func<string, T> converter)
    {
        _consoleService.WriteCustom(promptMessage, PromptColor, false);
        var input = _consoleService.ReadLine();

        if (!KeepPrompt)
            _consoleService.Clear();

        T result;

        try
        {
            result = string.IsNullOrWhiteSpace(input) ? default : converter(input);
        }
        catch
        {
            result = default;
        }

        return result;
    }

    public string Prompt(string promptMessage)
    {
        return Prompt<string>(promptMessage);
    }

    public string Password(string promptMessage)
    {
        _consoleService.WriteCustom(promptMessage, PromptColor, false);

        if (!KeepPrompt)
            _consoleService.Clear();

        return _consoleService.ReadPassword();
    }

    public string PromptOrDefault(string promptMessage)
    {
        return PromptOrDefault<string>(promptMessage);
    }

    public string PromptPasswordOrDefault(string promptMessage)
    {
        _consoleService.WriteCustom(promptMessage, PromptColor, false);
        var input = _consoleService.ReadPassword();

        string result;

        try
        {
            result = string.IsNullOrWhiteSpace(input) ? default : input;
        }
        catch
        {
            result = default;
        }

        if (!KeepPrompt)
            _consoleService.Clear();

        return result;
    }

    public int Choose(string promptMessage, IEnumerable<string> choices)
    {
        var selectedIndex = 0;
        var selectionMade = false;
        var choicesArray = choices.ToArray();

        do
        {
            _consoleService.Clear();
            _consoleService.WriteCustom(promptMessage, PromptColor);

            for (var i = 0; i < choicesArray.Length; i++)
            {
                _consoleService.WriteCustom(i == selectedIndex ? "> " : "  ",
                    ConsoleColor.Green,
                    false);
                _consoleService.WriteCustom(choicesArray.ElementAt(i), PromptColor);
            }

            var keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.Z:
                    selectedIndex = Math.Max(0, selectedIndex - 1);
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    selectedIndex = Math.Min(choicesArray.Length - 1, selectedIndex + 1);
                    break;
                case ConsoleKey.Enter:
                    selectionMade = true;
                    break;
                case ConsoleKey.Escape:
                    selectedIndex = -1;
                    selectionMade = true;
                    break;
                default:
                    continue;
            }
        } while (!selectionMade);

        if (!KeepPrompt)
            _consoleService.Clear();

        return selectedIndex;
    }

    public int Choose(string promptMessage, params string[] choices)
    {
        return Choose(promptMessage, choices.AsEnumerable());
    }

    public string ChooseValue(string promptMessage, IEnumerable<string> choices)
    {
        var choicesList = choices.ToList();
        return choicesList.ElementAt(Choose(promptMessage, choicesList));
    }

    public IEnumerable<int> ChooseMultiple(string promptMessage, IEnumerable<string> choices)
    {
        var selectedIndex = 0;
        var selectionMade = false;
        var choicesArray = choices.ToArray();
        var selectedIndices = new List<int>();

        do
        {
            _consoleService.Clear();
            _consoleService.WriteCustom(promptMessage, PromptColor);

            for (var i = 0; i < choicesArray.Length; i++)
            {
                _consoleService.WriteCustom(i == selectedIndex ? "> " : "  ",
                    ConsoleColor.Green,
                    false);
                _consoleService.WriteCustom(selectedIndices.Contains(i) ? "[X] " : "[ ] ",
                    ConsoleColor.Green,
                    false);
                _consoleService.WriteCustom(choicesArray.ElementAt(i), PromptColor);
            }

            var keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.Z:
                    selectedIndex = Math.Max(0, selectedIndex - 1);
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    selectedIndex = Math.Min(choicesArray.Length - 1, selectedIndex + 1);
                    break;
                case ConsoleKey.Enter:
                    if (selectedIndices.Contains(selectedIndex))
                        selectedIndices.Remove(selectedIndex);
                    else
                        selectedIndices.Add(selectedIndex);
                    break;
                case ConsoleKey.E:
                    selectedIndex = -1;
                    selectionMade = true;
                    break;
                default:
                    continue;
            }
        } while (!selectionMade);

        if (!KeepPrompt)
            _consoleService.Clear();

        return selectedIndices;
    }

    public IEnumerable<int> ChooseMultiple(string promptMessage, params string[] choices)
    {
        return ChooseMultiple(promptMessage, choices.AsEnumerable());
    }

    public IEnumerable<string> ChooseValues(string promptMessage, IEnumerable<string> choices)
    {
        var choicesList = choices.ToList();
        var selectedIndices = ChooseMultiple(promptMessage, choicesList);
        return selectedIndices.Select(choicesList.ElementAt);
    }

    public IEnumerable<string> ChooseValues(string promptMessage, params string[] choices)
    {
        return ChooseValues(promptMessage, choices.AsEnumerable());
    }
}