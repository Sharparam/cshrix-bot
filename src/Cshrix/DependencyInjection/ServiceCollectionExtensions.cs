// <copyright file="ServiceCollectionExtensions.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.DependencyInjection
{
    using Configuration;

    using JetBrains.Annotations;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Contains extension methods for the <see cref="IServiceCollection" /> interface.
    /// </summary>
    [PublicAPI]
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Cshrix services to the dependency injection system.
        /// </summary>
        /// <param name="services">The instance of <see cref="IServiceCollection" /> to register Cshrix into.</param>
        /// <returns>
        /// The same instance of <see cref="IServiceCollection" /> that was passed in, but with Cshrix services added.
        /// </returns>
        /// <remarks>
        /// Cshrix expects the <see cref="MatrixClientConfiguration" /> to have been registered as an options item.
        /// </remarks>
        public static IServiceCollection AddCshrixServices(this IServiceCollection services)
        {
            services.AddHttpClient<IMatrixClient, MatrixClient>();
            return services;
        }

        /// <summary>
        /// Adds Cshrix services to the dependency injection system.
        /// </summary>
        /// <param name="services">The instance of <see cref="IServiceCollection" /> to register Cshrix into.</param>
        /// <param name="configuration">Configuration object.</param>
        /// <returns>
        /// The same instance of <see cref="IServiceCollection" /> that was passed in, but with Cshrix services added.
        /// </returns>
        /// <remarks>
        /// This method also takes an <see cref="IConfiguration" /> object containing the
        /// <see cref="MatrixClientConfiguration" /> for Cshrix.
        /// </remarks>
        public static IServiceCollection AddCshrixServices(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddCshrixServices(configuration, MatrixClientConfiguration.DefaultSectionName);

        /// <summary>
        /// Adds Cshrix services to the dependency injection system.
        /// </summary>
        /// <param name="services">The instance of <see cref="IServiceCollection" /> to register Cshrix into.</param>
        /// <param name="configuration">Configuration object.</param>
        /// <param name="sectionName">
        /// Name of the section inside <paramref name="configuration" /> that contains the
        /// <see cref="MatrixClientConfiguration" /> for Cshrix.
        /// </param>
        /// <returns>
        /// The same instance of <see cref="IServiceCollection" /> that was passed in, but with Cshrix services added.
        /// </returns>
        /// <remarks>
        /// This method also takes an <see cref="IConfiguration" /> object and a section name pointing
        /// to a <see cref="MatrixClientConfiguration" /> inside that configuration object.
        /// </remarks>
        public static IServiceCollection AddCshrixServices(
            this IServiceCollection services,
            IConfiguration configuration,
            string sectionName)
        {
            services.Configure<MatrixClientConfiguration>(configuration.GetSection(sectionName));
            return services.AddCshrixServices();
        }
    }
}
