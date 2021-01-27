using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebStore
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args)
           .Build()
           .Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(host => host.UseStartup<Startup>())
                //.ConfigureLogging((host, log) => log
                //    .ClearProviders()
                //    .AddEventLog(opt => opt.LogName = "WebStore.log")
                //    .AddFilter("Microsoft.Hosting", LogLevel.Error)
                //    .AddFilter((category, level) => !(category.StartsWith("Microsoft") && level >= LogLevel.Warning))
                // )
            ;
    }
}
