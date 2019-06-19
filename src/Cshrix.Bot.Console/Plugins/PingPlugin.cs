// <copyright file="PingPlugin.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Bot.Console.Plugins
{
    /// <summary>
    /// A plugin that response to ping requests in the chat.
    /// </summary>
    public class PingPlugin : IPlugin
    {
        /// <inheritdoc />
        public string Id => "ping-pong";

        /// <inheritdoc />
        public string Name => "Ping";

        /// <inheritdoc />
        public string Description => "Responds to ping commands sent in chat";
    }
}
