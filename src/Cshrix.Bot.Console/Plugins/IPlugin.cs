// <copyright file="IPlugin.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Bot.Console.Plugins
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Data;

    using JetBrains.Annotations;

    /// <summary>
    /// A plugin that can be loaded into the bot.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Gets the display name of the plugin.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the description of the plugin.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets a collection of commands this plugin wishes to handle.
        /// </summary>
        IReadOnlyCollection<string> Commands { get; }

        /// <summary>
        /// Handles a command.
        /// </summary>
        /// <param name="message">The message that triggered the command.</param>
        /// <param name="command">The command word.</param>
        /// <param name="text">The text following the command, if any.</param>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
        Task HandleCommand(Message message, string command, [CanBeNull] string text);
    }
}
