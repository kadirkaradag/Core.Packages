using Serilog;

namespace Core.CrossCuttingConcerns.SeriLog;

public abstract class LoggerServiceBase // abstract olma sebebi, mongo istersem mongoya dosyaya istersem dosyaya loglama yapması icin
{
    protected ILogger Logger { get; set; }  //protected olmasının sebebi: abstract sadece inheritance ile kullanılabilir. bunu da sadece inherit eden yer görsün diye protected. protectedlar bu şekilde public gibi yazılır

    protected LoggerServiceBase()
    {
        Logger = null;
    }

    protected LoggerServiceBase(ILogger logger)
    {
        Logger = logger;
    }

    public void Verbose(string message) => Logger.Verbose(message); //mesaj vermek istediğinde detaylı log icin
    public void Fatal(string message) => Logger.Fatal(message);
    public void Info(string message) => Logger.Information(message);
    public void Warn(string message) => Logger.Warning(message);
    public void Debug(string message) => Logger.Debug(message);
    public void Error(string message) => Logger.Error(message);

}
