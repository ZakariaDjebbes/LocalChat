using ZConsole.Service;
using ZConsole.Utils;

namespace ZConsole.Implementation;

public class LoggerService : ILoggerService
{
    private readonly IConsoleService _consoleService;

    public LoggerService(IConsoleService consoleService)
    {
        _consoleService = consoleService;
    }

    /// <summary>
    ///     Whether to show the timestamp in the log messages.
    ///     Default is true.
    /// </summary>
    public bool ShowTimestamp { get; set; } = true;

    /// <summary>
    ///     The format of the timestamp.
    ///     Default is "yyyy-MM-dd HH:mm:ss".
    /// </summary>
    public string TimestampFormat { get; set; } = "yyyy-MM-dd HH:mm:ss";

    /// <summary>
    ///     Whether to show the log level in the log messages.
    ///     Default is true.
    /// </summary>
    public bool ShowLevel { get; set; } = true;


    public void LogCustom(string message, LogLevel level)
    {
        var timestamp = ShowTimestamp
            ? DateTime.Now.ToString(TimestampFormat)
            : string.Empty;
        var levelString = ShowLevel
            ? $"{level.ToString().ToUpper()}"
            : string.Empty;
        var color = level switch
        {
            LogLevel.Info => ConsoleColor.Blue,
            LogLevel.Success => ConsoleColor.Green,
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.Red,
            LogLevel.Debug => ConsoleColor.Cyan,
            LogLevel.Critical => ConsoleColor.Magenta,
            LogLevel.Default => ConsoleColor.White,
            _ => ConsoleColor.White
        };

        var logMessage = $"{timestamp} [{levelString}]: {message}";
        _consoleService.WriteCustom(logMessage, color);
    }

    public void Log(string message)
    {
        LogCustom(message, LogLevel.Default);
    }

    public void LogSuccess(string message)
    {
        LogCustom(message, LogLevel.Success);
    }

    public void LogError(string message)
    {
        LogCustom(message, LogLevel.Error);
    }

    public void LogWarning(string message)
    {
        LogCustom(message, LogLevel.Warning);
    }

    public void LogInfo(string message)
    {
        LogCustom(message, LogLevel.Info);
    }

    public void LogDebug(string message)
    {
        LogCustom(message, LogLevel.Debug);
    }

    public void LogCritical(string message)
    {
        LogCustom(message, LogLevel.Critical);
    }
}