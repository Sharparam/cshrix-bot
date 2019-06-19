// <copyright file="BotConfiguration.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Bot.Console.Configuration
{
    using System.Collections.Generic;

    using Data;

    using JetBrains.Annotations;

    /// <summary>
    /// Contains bot configuration.
    /// </summary>
    public class BotConfiguration
    {
        /// <summary>
        /// The default name used to specify this configuration in an appsettings.json file.
        /// </summary>
        public const string DefaultSectionName = nameof(BotConfiguration);

        /// <summary>
        /// The default prefix to use for commands.
        /// </summary>
        public const string DefaultCommandPrefix = "!";

        /// <summary>
        /// Gets or sets the prefix required in front of a message to treat it as a command.
        /// </summary>
        public string CommandPrefix { get; set; } = DefaultCommandPrefix;

        /// <summary>
        /// Gets or sets a value indicating whether the command prefix should be used to activate commands.
        /// </summary>
        /// <remarks>
        /// If this is <c>false</c>, then <see cref="TreatMentionAsCommandPrefix" /> can be set to <c>true</c>
        /// to enable processing of commands <em>only</em> if the bot is mentioned.
        /// </remarks>
        public bool EnableCommandPrefix { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether to treat bot username mentions as equivalent to the prefix.
        /// </summary>
        public bool TreatMentionAsCommandPrefix { get; set; } = true;

        /// <summary>
        /// Gets or sets the collection of user IDs that are considered global super admins.
        /// </summary>
        [CanBeNull]
        public IEnumerable<UserId> SuperAdmins { get; set; }
    }
}
