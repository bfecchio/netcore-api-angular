using System;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace FullStack.Api
{
    public static class Program
    {
        #region Public Static Methods

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Debug()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting hosting");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex) { Log.Fatal(ex, "Host terminated unexpectedly"); }
            finally { Log.CloseAndFlush(); }

        }

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((ctx, config) =>
                {
                    var env = ctx.HostingEnvironment;

                    config
                        .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                        .AddCommandLine(args)
                        .AddEnvironmentVariables()
                        .Build();
                })
                .ConfigureLogging((ctx, logging) =>
                {
                    logging
                        .AddConfiguration(ctx.Configuration.GetSection("Logging"))
                        .AddDebug()
                        .AddConsole()
                        .AddSerilog()
                        .AddEventSourceLogger();
                })
                .UseSerilog((ctx, config) =>
                {
                    config
                        .ReadFrom.Configuration(ctx.Configuration)
                        .WriteTo.Debug()
                        .WriteTo.Console();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseKestrel()
                        .UseStartup<Startup>();
                });

        #endregion
    }
}
