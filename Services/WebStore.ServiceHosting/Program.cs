using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WebStore.Logger;

namespace WebStore.ServiceHosting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
               .ConfigureLogging(log => log.AddLog4Net());
    }
}
