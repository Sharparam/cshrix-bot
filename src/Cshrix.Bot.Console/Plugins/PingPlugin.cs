// <copyright file="PingPlugin.cs">
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

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// A plugin that response to ping requests in the chat.
    /// </summary>
    public sealed class PingPlugin : IPlugin
    {
        /// <summary>
        /// The logger instance for this class.
        /// </summary>
        private readonly ILogger _log;

        /// <summary>
        /// Initializes a new instance of the <see cref="PingPlugin" /> class.
        /// </summary>
        /// <param name="log">A logger instance for the class.</param>
        public PingPlugin(ILogger<PingPlugin> log) => _log = log;

        /// <inheritdoc />
        public string Name => "Ping";

        /// <inheritdoc />
        public string Description => "Responds to ping commands sent in chat";

        /// <inheritdoc />
        public IReadOnlyCollection<string> Commands =>
            new[]
            {
                "ping", "marco"
            };

        /// <inheritdoc />
        public async Task HandleCommand(Message message, string command, string text)
        {
            switch (command)
            {
                case "ping":
                    _log.LogDebug("Received ping, replying with pong");
                    await message.Room.SendAsync("Pong!");
                    break;

                case "marco":
                    _log.LogDebug("Received Marco, replying with Polo");
                    await message.Room.SendAsync("Polo!");
                    break;

                default:
                    _log.LogError("Received command {Command} but I don't know how to handle it", command);
                    break;
            }
        }
    }
}
