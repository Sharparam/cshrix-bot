// <copyright file="IPlugin.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Bot.Console.Plugins
{
    /// <summary>
    /// A plugin that can be loaded into the bot.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Gets the ID of the plugin.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets the display name of the plugin.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the description of the plugin.
        /// </summary>
        string Description { get; }
    }
}
