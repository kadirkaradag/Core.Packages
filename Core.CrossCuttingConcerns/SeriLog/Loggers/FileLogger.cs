using Core.CrossCuttingConcerns.SeriLog.ConfigurationModels;
using Core.CrossCuttingConcerns.SeriLog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Core.CrossCuttingConcerns.SeriLog.Loggers;

public class FileLogger : LoggerServiceBase
{
    private readonly IConfiguration _configuration; // appsettings den veri okuyabilmek icin

    public FileLogger(IConfiguration configuration)
    {
        _configuration = configuration;

        FileLogConfiguration logConfig = configuration.GetSection("SeriLogConfigurations:FileLogConfiguration").Get<FileLogConfiguration>() ?? throw new Exception(SeriLogMessages.NullOptionsMessage);
        //get() ile mapping yapıyoruz. microsoft.binder kütüphanesi ile yapılıyor. yani git appsettings den veriyi al git bu neysene maple diyoruz.

        string logFilePath = string.Format(format: "{0}{1}", arg0: Directory.GetCurrentDirectory() + logConfig.FolderPath, arg1: ".txt");

        Logger = new LoggerConfiguration().WriteTo.File(
            logFilePath,
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit:null,
            fileSizeLimitBytes:5000000,
            outputTemplate:"{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{level}] {Message}{NewLine}{Exception}"
            ).CreateLogger();
        //rollingInterval ne kadar sürede bir yeni dosya olustursun, retainedFileCountLimit dosyalar ne zaman silinsin,fileSizeLimitBytes dosya boyutu maks bu kadar olsun, outputTemplate nasıl tutulacak.
    }
}
