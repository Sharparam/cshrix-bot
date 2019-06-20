// <copyright file="IPluginManager.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Bot.Console.Plugins
{
    using System.Threading.Tasks;

    using Data;

    /// <summary>
    /// Manages plugins.
    /// </summary>
    public interface IPluginManager
    {
        /// <summary>
        /// Handles a message.
        /// </summary>
        /// <param name="message">The message to handle.</param>
        /// <returns><c>true</c> if the message was handled by a plugin; otherwise, <c>false</c>.</returns>
        Task<bool> HandleMessage(Message message);
    }
}
