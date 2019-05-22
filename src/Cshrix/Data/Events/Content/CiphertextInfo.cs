// <copyright file="CiphertextInfo.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about a ciphertext.
    /// </summary>
    public readonly struct CiphertextInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CiphertextInfo" /> structure.
        /// </summary>
        /// <param name="body">The encrypted payload.</param>
        /// <param name="type">The Olm message type.</param>
        [JsonConstructor]
        public CiphertextInfo(string body, int type)
            : this()
        {
            Body = body;
            Type = type;
        }

        /// <summary>
        /// Gets the encrypted payload.
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; }

        /// <summary>
        /// Gets the Olm message type of the ciphertext.
        /// </summary>
        [JsonProperty("type")]
        public int Type { get; }
    }
}
