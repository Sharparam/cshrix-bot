// <copyright file="ServiceCollectionExtensions.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Bot.Console
{
    using System;

    using DependencyInjection;

    using JetBrains.Annotations;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Plugins;

    /// <summary>
    /// Contains bot-related extensions to <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds bot-related services to the <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The services object.</param>
        /// <param name="configuration">Application configuration.</param>
        /// <returns>The modified services object.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="services" /> is <c>null</c>.
        /// </exception>
        public static IServiceCollection AddBotServices(
            [NotNull] this IServiceCollection services,
            IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddCshrixServices(configuration, "ClientConfiguration");
            services.AddPluginServices();
            services.AddTransient<Bot>();
            return services;
        }
    }
}
