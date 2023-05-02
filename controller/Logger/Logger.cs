using log4net;
using log4net.Config;

class Logger<T>
{
    private delegate void LoggerBank(string Message);
    private readonly ILog Log = LogManager.GetLogger(typeof(T));
    private readonly List<LoggerBank> logger = new(){
            Log.Info,
            Log.Warn,
            Log.Error,
            Log.Fatal,
        };

    public static void Config()
    {
        XmlConfigurator.Configure(new FileInfo("controller/Logger/log4net.config"));
    }

    public static void Message(string message, LogLevel LogLevel)
    {
        logger[(int)LogLevel](message);
    }
}

public enum LogLevel
{
    Info = 0,
    Warn = 1,
    Error = 2,
    Fatal = 3,
}