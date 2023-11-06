namespace ZConsole.Service
{
    /// <summary>
    /// A service that provides console prompting functionality.
    /// </summary>
    public interface IConsolePromptService
    {
        /// <summary>
        /// Prompts the user for input.
        /// </summary>
        /// <param name="promptMessage">The message to display to the user.</param>
        /// <returns>The user's input as a string.</returns>
        string Prompt(string promptMessage);

        /// <summary>
        /// Prompts the user for input with a default value.
        /// </summary>
        /// <param name="promptMessage">The message to display to the user.</param>
        /// <param name="defaultValue">The default value to display and return if the user provides no input.</param>
        /// <returns>The user's input as a string or the default value if no input is provided.</returns>
        string PromptOrDefault(string promptMessage, string defaultValue = "");

        /// <summary>
        /// Prompts the user for input and converts it to a specific type.
        /// </summary>
        /// <typeparam name="T">The type to which the user's input will be converted.</typeparam>
        /// <param name="promptMessage">The message to display to the user.</param>
        /// <returns>The user's input converted to the specified type.</returns>
        /// <exception cref="FormatException">Thrown if the user's input cannot be converted to the specified type.</exception>
        T Prompt<T>(string promptMessage);

        /// <summary>
        /// Prompts the user for input with a default value and converts it to a specific type.
        /// </summary>
        /// <typeparam name="T">The type to which the user's input will be converted.</typeparam>
        /// <param name="promptMessage">The message to display to the user.</param>
        /// <param name="defaultValue">The default value to display and return if the user provides no input.</param>
        /// <returns>The user's input converted to the specified type or the default value if no input is provided.</returns>
        T PromptOrDefault<T>(string promptMessage, T defaultValue = default);

        /// <summary>
        /// Prompts the user for input and converts it to a specific type using a custom converter.
        /// </summary>
        /// <typeparam name="T">The type to which the user's input will be converted.</typeparam>
        /// <param name="promptMessage">The message to display to the user.</param>
        /// <param name="converter">A function to convert the user's input to the specified type.</param>
        /// <returns>The user's input converted to the specified type using the custom converter.</returns>
        /// <exception cref="FormatException">Thrown if the user's input cannot be converted to the specified type.</exception>
        T Prompt<T>(string promptMessage, Func<string, T> converter);

        /// <summary>
        /// Prompts the user for input with a default value and converts it to a specific type using a custom converter.
        /// </summary>
        /// <typeparam name="T">The type to which the user's input will be converted.</typeparam>
        /// <param name="promptMessage">The message to display to the user.</param>
        /// <param name="converter">A function to convert the user's input to the specified type.</param>
        /// <param name="defaultValue">The default value to display and return if the user provides no input.</param>
        /// <returns>The user's input converted to the specified type using the custom converter or the default value if no input is provided.</returns>
        T PromptOrDefault<T>(string promptMessage, Func<string, T> converter, T defaultValue = default);

        /// <summary>
        /// Presents the user with a choice of options and allows them to select one.
        /// </summary>
        /// <param name="promptMessage">The message to display to the user.</param>
        /// <param name="choices">A collection of strings representing the available choices.</param>
        /// <param name="keepPrompt">
        /// Whether to keep the prompt message on the screen after the user has made a selection.
        /// </param>
        /// <returns>The index of the selected choice.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the user selects an out-of-range index.</exception>
        int Choose(string promptMessage, IEnumerable<string> choices, bool keepPrompt = false);

        /// <summary>
        /// Presents the user with a choice of options and allows them to select one.
        /// </summary>
        /// <param name="promptMessage">The message to display to the user.</param>
        /// <param name="choices">A params array of strings representing the available choices.</param>
        /// <returns>The index of the selected choice.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the user selects an out-of-range index.</exception>
        int Choose(string promptMessage, params string[] choices);
    }
}
