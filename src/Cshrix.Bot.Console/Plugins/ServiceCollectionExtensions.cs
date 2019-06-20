// <copyright file="ServiceCollectionExtensions.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Bot.Console.Plugins
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Contains plugin-related extensions to <see cref="IServiceCollection" />.
    /// </summary>
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds plugin-related services to an <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The services object.</param>
        /// <returns>The modified services object.</returns>
        internal static IServiceCollection AddPluginServices(this IServiceCollection services)
        {
            services.AddTransient<IPlugin, PingPlugin>();
            return services;
        }
    }
}
