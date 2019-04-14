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
    using System.IO;
    using System.Threading.Tasks;

    using Configuration;

    using Data;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public static class Program
    {
        public static async Task Main(string[] args)
        {
            ServiceProvider provider = null;
            ILogger log = null;

            try
            {
                var configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false, true)
                    .AddJsonFile("appsettings.local.json", true, true)
                    .AddEnvironmentVariables("CSHRIX_BOT")
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
