namespace ZConsole.Service;

/// <summary>
///     A service that provides console I/O functionality.
/// </summary>
public interface IConsoleService
{
    /// <summary>
    ///     Reads the next line of characters from the standard input stream.
    /// </summary>
    /// <returns>The string read from the standard input stream.</returns>
    string ReadLine();

    /// <summary>
    ///     Reads the next line of characters from the standard input stream masking the input with the given character.
    /// </summary>
    /// <param name="mask">The character to mask the input with. Default is '*'</param>
    /// <returns>
    ///     The string read from the standard input stream.
    /// </returns>
    string ReadPassword(char mask = '*');

    /// <summary>
    ///     Writes the specified string value to the standard output stream.
    /// </summary>
    /// <param name="message">The value to write.</param>
    /// <param name="color">The color of the text.</param>
    /// <param name="newLine">Whether to write a new line after the message. Default is true.</param>
    void WriteCustom(string message, ConsoleColor color, bool newLine = true);

    /// <summary>
    ///     Writes the specified strings values to the standard output stream.
    ///     The strings will be colored with the corresponding colors.
    /// </summary>
    /// <param name="messages">
    ///     The values to write.
    /// </param>
    /// <param name="colors">
    ///     The colors of the text.
    /// </param>
    /// <param name="newLine">
    ///     Whether to write a new line after the message. Default is true.
    /// </param>
    /// <exception cref="NullReferenceException">
    ///     Thrown if <paramref name="messages" /> or <paramref name="colors" /> is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     Thrown if the number of messages and colors is not the same.
    /// </exception>
    void WriteCustom(IEnumerable<string> messages, IEnumerable<ConsoleColor> colors, bool newLine = true);

    /// <summary>
    ///     Writes the specified string value to the standard output stream with a default color.
    /// </summary>
    /// <param name="message">
    ///     The value to write.
    /// </param>
    /// <param name="newLine">
    ///     Whether to write a new line after the message. Default is true.
    /// </param>
    void Write(string message, bool newLine = true);

    /// <summary>
    ///     Writes the specified string value to the standard output stream with
    /// </summary>
    void BreakLine();

    /// <summary>
    ///     Clears the console buffer and corresponding console window of display information.
    /// </summary>
    void Clear();
}