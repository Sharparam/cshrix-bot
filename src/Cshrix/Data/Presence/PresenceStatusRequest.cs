// <copyright file="PresenceStatusRequest.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Presence
{
    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Describes a new presence state to set on a user.
    /// </summary>
    public readonly struct PresenceStatusRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PresenceStatusRequest" /> structure.
        /// </summary>
        /// <param name="presence">The new presence to set.</param>
        /// <param name="statusMessage">State message to set.</param>
        [JsonConstructor]
        public PresenceStatusRequest(Presence presence, [CanBeNull] string statusMessage = null)
            : this()
        {
            Presence = presence;
            StatusMessage = statusMessage;
        }

        /// <summary>
        /// Gets the new presence to set.
        /// </summary>
        [JsonProperty("presence")]
        public Presence Presence { get; }

        /// <summary>
        /// Gets a state message to set, or <c>null</c> if it should be left blank.
        /// </summary>
        [JsonProperty(
            "status_msg",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string StatusMessage { get; }
    }
}
