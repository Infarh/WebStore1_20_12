using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace WebStore.Logger
{
    public static class Log4NetLoggerFactoryExtensions
    {
        public static ILoggerFactory AddLog4Net(this ILoggerFactory Factory, string ConfigurationFile = "log4net.config")
        {
            if (ConfigurationFile is null) throw new ArgumentNullException(nameof(ConfigurationFile));

            if (!Path.IsPathRooted(ConfigurationFile))
            {
                var assembly = Assembly.GetEntryAssembly() ?? throw new InvalidOperationException("Не удалось определить сборку, содержащую точку входа в приложение");
                var dir = Path.GetDirectoryName(assembly.Location) ?? throw new InvalidOperationException("Получена пустая ссылка на строку пути к сборке с точкой входа в приложение");
                ConfigurationFile = Path.Combine(dir, ConfigurationFile);
            }

            Factory.AddProvider(new Log4NetLoggerProvider(ConfigurationFile));

            return Factory;
        }
    }
}
