
namespace Getnet.Services;

public class LogService
{
    private readonly string _logDirectory;

    public LogService(
        string? basePath = null
    )
    {
        _logDirectory = Path.Combine(basePath ?? AppContext.BaseDirectory, "Logs");
        Directory.CreateDirectory(_logDirectory);
    }

    public void LogError(string message)
    {
        string logDate = DateTime.Now.ToString("dd-MM-yyyy");
        string timestamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

        string logMessage = $"[ERRO]: [{timestamp}] - {message}";
        string logFile = Path.Combine(_logDirectory, $"{logDate}.log");
        
        File.AppendAllText(logFile, logMessage + Environment.NewLine);
    }
}
