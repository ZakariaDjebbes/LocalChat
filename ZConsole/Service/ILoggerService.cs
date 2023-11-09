using ZConsole.Utils;

namespace ZConsole.Service;

/// <summary>
///    A service that provides logging functionality.
/// </summary>
public interface ILoggerService
{
    /// <summary>
    ///    Whether to show the timestamp in the log messages.
    /// </summary>
    public bool ShowTimestamp { get; set; }

    /// <summary>
    ///  The format of the timestamp.
    /// </summary>
    public string TimestampFormat { get; set; }

    /// <summary>
    ///   Whether to show the log level in the log messages.
    /// </summary>
    public bool ShowLevel { get; set; }

    /// <summary>
    ///    Writes the specified string value to the standard output stream with
    /// a default color.
    /// </summary>
    /// <param name="message">
    ///    The value to write.
    /// </param>
    /// <param name="level">
    ///   The <see cref="LogLevel"/> of the message.
    /// </param>
    void LogCustom(string message, LogLevel level);
    
    /// <summary>
    ///    Writes the specified string value to the standard output stream with
    /// a default color.
    /// </summary>
    /// <param name="message">
    ///    The value to write.
    /// </param>
    void Log(string message);
    
    /// <summary>
    ///   Writes the specified string value to the standard output stream with
    /// <see cref="ConsoleColor.Green"/> color.
    /// </summary>
    /// <param name="message">
    ///   The value to write.
    /// </param>
    void LogSuccess(string message);

    /// <summary>
    ///  Writes the specified string value to the standard output stream with
    /// <see cref="ConsoleColor.Red"/> color.
    /// </summary>
    void LogError(string message);

    /// <summary>
    /// Writes the specified string value to the standard output stream with
    /// <see cref="ConsoleColor.Yellow"/> color.
    /// </summary>
    void LogWarning(string message);

    /// <summary>
    /// Writes the specified string value to the standard output stream with
    /// <see cref="ConsoleColor.Blue"/> color.
    /// </summary>
    /// <param name="message"></param>
    void LogInfo(string message);
    
    /// <summary>
    /// Writes the specified string value to the standard output stream with
    /// <see cref="ConsoleColor.Cyan"/> color.
    /// </summary>
    /// <param name="message"></param>
    void LogDebug(string message);

    /// <summary>
    /// Writes the specified string value to the standard output stream with
    /// <see cref="ConsoleColor.Magenta"/> color.
    /// </summary>
    /// <param name="message"></param>
    void LogCritical(string message);
}