namespace Core.Service;

public interface IConsoleService
{
    string ReadLine();
    void LogCustom(string message, ConsoleColor color);
    void Log(string message);
    void LogInfo(string message);
    void LogSuccess(string message);
    void LogWarning(string message);
    void LogError(string message);
    void LogCritical(string message);
}