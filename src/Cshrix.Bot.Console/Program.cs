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

            var basePath = AppContext.BaseDirectory;

            try
            {
                var configBuilder = new ConfigurationBuilder().SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", false, true)
                    .AddJsonFile("appsettings.local.json", true, true)
                    .AddEnvironmentVariables("CSHRIXBOT_")
                    .AddCommandLine(args);

                var configuration = configBuilder.Build();

                var services = new ServiceCollection();

                services.Configure<MatrixClientConfiguration>(configuration.GetSection("ClientConfiguration"));

                services.AddLogging(
                    b => b.AddConfiguration(configuration.GetSection("Logging"))
                        .AddDebug()
                        .AddConsole()
                        .AddEventSourceLogger()
                        .AddTraceSource("cshrix-bot"));

                services.AddCshrixServices();

                services.AddTransient<Bot>();

                provider = services.BuildServiceProvider();

                log = provider.GetRequiredService<ILoggerFactory>().CreateLogger("MAIN");

                var bot = provider.GetRequiredService<Bot>();

                await bot.TestAsync();
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
    }
}
