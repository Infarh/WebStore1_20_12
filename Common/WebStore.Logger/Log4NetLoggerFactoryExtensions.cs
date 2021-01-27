using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace WebStore.Logger
{
    public static class Log4NetLoggerFactoryExtensions
    {
        private static string CheckFilePath(string File)
        {
            if (File is null) throw new ArgumentNullException(nameof(File));

            if (Path.IsPathRooted(File)) return File;

            var assembly = Assembly.GetEntryAssembly() ?? throw new InvalidOperationException("Не удалось определить сборку, содержащую точку входа в приложение");
            var dir = Path.GetDirectoryName(assembly.Location) ?? throw new InvalidOperationException("Получена пустая ссылка на строку пути к сборке с точкой входа в приложение");
            return Path.Combine(dir, File);

        }

        public static ILoggerFactory AddLog4Net(this ILoggerFactory Factory, string ConfigurationFile = "log4net.config")
        {
            Factory.AddProvider(new Log4NetLoggerProvider(CheckFilePath(ConfigurationFile)));
            return Factory;
        }

        public static ILoggingBuilder AddLog4Net(this ILoggingBuilder Builder, string ConfigurationFile = "log4net.config") => 
            Builder.AddProvider(new Log4NetLoggerProvider(CheckFilePath(ConfigurationFile)));
    }
}
