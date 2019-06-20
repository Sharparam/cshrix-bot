// <copyright file="PluginExtensions.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Bot.Console.Extensions
{
    using System;

    using JetBrains.Annotations;

    using Plugins;

    /// <summary>
    /// Contains extensions for the <see cref="IPlugin" /> interface.
    /// </summary>
    internal static class PluginExtensions
    {
        /// <summary>
        /// Gets the ID of a plugin.
        /// </summary>
        /// <param name="plugin">The plugin to get the ID for.</param>
        /// <returns>The ID of the plugin.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="plugin" /> is <c>null</c>.
        /// </exception>
        internal static string GetId([NotNull] this IPlugin plugin) =>
            plugin?.GetType().FullName ?? throw new ArgumentNullException(nameof(plugin));
    }
}
