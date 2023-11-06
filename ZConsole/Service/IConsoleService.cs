namespace ZConsole.Service;

/// <summary>
/// A service that provides console I/O functionality.
/// </summary>
public interface IConsoleService
{
    /// <summary>
    /// Reads the next line of characters from the standard input stream.
    /// </summary>
    /// <returns>The string read from the standard input stream.</returns>
    string ReadLine();

    /// <summary>
    /// Writes the specified string value to the standard output stream.
    /// </summary>
    /// <param name="message">The value to write.</param>
    /// <param name="color">The color of the text.</param>
    /// <param name="newLine">Whether to write a new line after the message. Default is true.</param>
    void LogCustom(string message, ConsoleColor color, bool newLine = true);

    /// <summary>
    /// Writes the specified string value to the standard output stream with a default color.
    /// </summary>
    /// <param name="message">The value to write.</param>
    /// <param name="newLine">Whether to write a new line after the message. Default is true.</param>
    void Log(string message, bool newLine = true);

    /// <summary>
    /// Writes the specified string value to the standard output stream with an information color.
    /// </summary>
    /// <param name="message">The value to write.</param>
    /// <param name="newLine">Whether to write a new line after the message. Default is true.</param>
    void LogInfo(string message, bool newLine = true);

    /// <summary>
    /// Writes the specified string value to the standard output stream with a success color.
    /// </summary>
    /// <param name="message">The value to write.</param>
    /// <param name="newLine">Whether to write a new line after the message. Default is true.</param>
    void LogSuccess(string message, bool newLine = true);

    /// <summary>
    /// Writes the specified string value to the standard output stream with a warning color.
    /// </summary>
    /// <param name="message">The value to write.</param>
    /// <param name="newLine">Whether to write a new line after the message. Default is true.</param>
    void LogWarning(string message, bool newLine = true);

    /// <summary>
    /// Writes the specified string value to the standard output stream with an error color.
    /// </summary>
    /// <param name="message">The value to write.</param>
    /// <param name="newLine">Whether to write a new line after the message. Default is true.</param>
    void LogError(string message, bool newLine = true);

    /// <summary>
    /// Writes the specified string value to the standard output stream with a critical color.
    /// </summary>
    /// <param name="message">The value to write.</param>
    /// <param name="newLine">Whether to write a new line after the message. Default is true.</param>
    void LogCritical(string message, bool newLine = true);
    
    /// <summary>
    /// Clears the console buffer and corresponding console window of display information.
    /// </summary>
    void Clear();
}