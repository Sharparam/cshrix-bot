// <copyright file="Offer.cs">
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
    /// A session description object for a VoIP call.
    /// </summary>
    public readonly struct Offer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Offer" /> structure.
        /// </summary>
        /// <param name="type">The type of session description.</param>
        /// <param name="sdp">The SDP text of the session description.</param>
        [JsonConstructor]
        public Offer(string type, string sdp)
            : this()
        {
            Type = type;
            Sdp = sdp;
        }

        /// <summary>
        /// Gets the type of session description.
        /// </summary>
        /// <remarks>
        /// Must be <c>offer</c>.
        /// </remarks>
        [JsonProperty("type")]
        public string Type { get; }

        /// <summary>
        /// Gets the SDP text of the session description.
        /// </summary>
        [JsonProperty("sdp")]
        public string Sdp { get; }
    }
}
