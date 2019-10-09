using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WebAPI3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
               .Enrich.FromLogContext()
               .WriteTo.Console()
               .CreateLogger();
            try
            {
                Log.Information("Info: Starting web host");
                Log.Debug("Debug: Starting web host");
                
                
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog((hostingCotext, loggerConfigration) => loggerConfigration
                    .ReadFrom.Configuration(hostingCotext.Configuration)
                    .Enrich.FromLogContext()
                    .WriteTo.Debug()
                    .WriteTo.File("c:\\temp\\WebAPI\\log.txt", rollingInterval: RollingInterval.Day)
                    .WriteTo.Console(
                         outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {NewLine}{Exception}"));
    }
}
