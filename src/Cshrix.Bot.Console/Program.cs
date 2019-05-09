// <copyright file="Program.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Bot.Console
{
    using System;
    using System.Threading.Tasks;

    using Configuration;

    using DependencyInjection;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Serilog;
    using Serilog.Formatting.Json;

    using ILogger = Microsoft.Extensions.Logging.ILogger;

    /// <summary>
    /// Main program class containing the entry point.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Entry point for the program.
        /// </summary>
        /// <param name="args">Arguments passed to the program, if any.</param>
        /// <returns>A <see cref="Task" /> representing program progress.</returns>
        public static async Task Main(string[] args)
        {
            ServiceProvider provider = null;
            ILogger log = null;

            try
            {
                var configuration = BuildConfiguration(args);

                var services = new ServiceCollection();

                services.AddLogging(
                    builder =>
                    {
                        var logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
                        Log.Logger = logger;
                        builder.AddSerilog(logger, true);
                    });

                services.AddCshrixServices(configuration, "ClientConfiguration");

                services.AddTransient<Bot>();

                provider = services.BuildServiceProvider();

                log = provider.GetRequiredService<ILoggerFactory>().CreateLogger("MAIN");

                var bot = provider.GetRequiredService<Bot>();

                await bot.TestAsync();

                Console.WriteLine("Press <ENTER> to exit");
                Console.ReadLine();

                log.LogInformation("<ENTER> pressed, disposing service provider and exiting");
                provider.Dispose();
            }
            catch (Exception ex)
            {
                if (log == null)
                {
                    Console.WriteLine($"Unhandled exception {ex.GetType()} in main: {ex.Message}");
                    Console.WriteLine(ex.StackTrace);
                }
                else
                {
                    log.LogCritical(ex, "Unhandled exception in main");
                }

                throw;
            }
            finally
            {
                provider?.Dispose();
            }
        }

        private static IConfigurationRoot BuildConfiguration(string[] args) =>
            new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsettings.local.json", true, true)
                .AddEnvironmentVariables("CSHRIXBOT_")
                .AddCommandLine(args)
                .Build();
    }
}
