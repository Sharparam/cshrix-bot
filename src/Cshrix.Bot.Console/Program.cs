/* Copyright (c) 2019 by Adam Hellberg.
 *
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

namespace Cshrix.Bot.Console
{
    using System.IO;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsettings.local.json", true, true)
                .AddEnvironmentVariables("CSHRIX_BOT")
                .AddCommandLine(args);

            var configuration = configBuilder.Build();

            var services = new ServiceCollection();

            services.AddLogging(b => b.SetMinimumLevel(LogLevel.Warning).AddDebug().AddConsole());

            services.AddCshrixServices();

            var provider = services.BuildServiceProvider();

            var bot = provider.GetRequiredService<Bot>();
        }
    }
}
