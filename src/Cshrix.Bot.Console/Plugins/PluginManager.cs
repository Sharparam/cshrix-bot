// <copyright file="PluginManager.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Bot.Console.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Configuration;

    using Data;

    using Extensions;

    using JetBrains.Annotations;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Manages plugins for a bot instance.
    /// </summary>
    public class PluginManager : IPluginManager
    {
        /// <summary>
        /// The logger instance for this class.
        /// </summary>
        private readonly ILogger _log;

        /// <summary>
        /// The Matrix client instance.
        /// </summary>
        private readonly IMatrixClient _client;

        /// <summary>
        /// A collection of registered plugins.
        /// </summary>
        private readonly IReadOnlyCollection<IPlugin> _plugins;

        /// <summary>
        /// The bot configuration object.
        /// </summary>
        private readonly BotConfiguration _config;

        /// <summary>
        /// Keeps a reference to the date and time at which the plugin manager was initialized.
        /// </summary>
        private readonly DateTimeOffset _initTimestamp;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginManager" /> class.
        /// </summary>
        /// <param name="log">A logger instance for the class.</param>
        /// <param name="client">The Matrix client instance.</param>
        /// <param name="plugins">A collection of registered plugins.</param>
        /// <param name="options">Bot options (including plugin options).</param>
        public PluginManager(
            ILogger<PluginManager> log,
            IMatrixClient client,
            IEnumerable<IPlugin> plugins,
            IOptions<BotConfiguration> options)
        {
            _log = log;
            _client = client;
            _plugins = plugins.ToList().AsReadOnly();
            _config = options.Value;
            _initTimestamp = DateTimeOffset.UtcNow;
        }

        /// <inheritdoc />
        public async Task<bool> HandleMessage(Message message)
        {
            if (message.SenderId == _client.UserId)
            {
                _log.LogTrace("Ignoring message sent by current user");
                return false;
            }

            if (message.SentAt < _initTimestamp)
            {
                _log.LogTrace("Ignoring message that was sent before plugin manager init");
                return false;
            }

            var commandsProcessed = await ProcessCommands(message);
            return commandsProcessed;
        }

        /// <summary>
        /// Processes any commands in a message.
        /// </summary>
        /// <param name="message">The message to process.</param>
        /// <returns><c>true</c> if a command was found and processed; otherwise, <c>false</c>.</returns>
        private async Task<bool> ProcessCommands(Message message)
        {
            _log.LogTrace("Processing message for commands");
            var prefixEnabled = _config.EnableCommandPrefix;
            var prefix = _config.CommandPrefix;
            var body = message.Content.Body.Trim();
            string command = null;
            string rest = null;

            if (prefixEnabled && body.StartsWith(prefix))
            {
                var wsIndex = body.IndexOfAny(
                    new[]
                    {
                        ' ', '\t', '\n'
                    },
                    prefix.Length);

                command = wsIndex == -1 ? body.Substring(prefix.Length) : body.Substring(prefix.Length, wsIndex - 1);
                rest = wsIndex == -1 ? null : body.Substring(wsIndex);
            }

            if (command == null)
            {
                _log.LogTrace("No command found in the message, aborting");
                return false;
            }

            command = command.ToLowerInvariant();

            _log.LogTrace("Command found in message: {Command}", command);

            var plugin = GetPluginForCommand(command);

            if (plugin == null)
            {
                _log.LogTrace("No plugin found to handle command {Command}, aborting", command);
                return false;
            }

            _log.LogDebug("Calling plugin {Plugin} to handle command {Command}", plugin.GetId(), command);
            await plugin.HandleCommand(message, command, rest);

            return true;
        }

        /// <summary>
        /// Finds the first plugin that can handle the specified command.
        /// </summary>
        /// <param name="command">The command to find a plugin for.</param>
        /// <returns>The first plugin that is able to handle the command, or <c>null</c> if none were found.</returns>
        [CanBeNull]
        private IPlugin GetPluginForCommand(string command)
        {
            return _plugins.FirstOrDefault(p => p.Commands.Contains(command));
        }
    }
}
