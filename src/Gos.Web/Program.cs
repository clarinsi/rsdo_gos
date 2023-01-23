using System;
using System.Linq;
using Autofac.Extensions.DependencyInjection;
using Gos.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Gos.Web
{
    public class Program
    {
        public static int Main(string[] args)
        {
            ConfigureSerilog();

            try
            {
                Log.Information("Starting host.");
                CreateHostBuilder(args).Build().Run();
                Log.Information("Host terminated successfully.");
                return 0;
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Host terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static void ConfigureSerilog()
        {
            var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
                .WriteTo.File(
                    path: configuration[ConfigurationKey.Logging.LogPath],
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} {Properties}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}