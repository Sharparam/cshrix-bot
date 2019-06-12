// <copyright file="ThirdPartyInvite.cs">
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
    /// Describes a third party invite.
    /// </summary>
    public readonly struct ThirdPartyInvite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThirdPartyInvite" /> structure.
        /// </summary>
        /// <param name="displayName">Display name of the invited user.</param>
        /// <param name="signed">Signed data.</param>
        [JsonConstructor]
        public ThirdPartyInvite(string displayName, SignedData signed)
            : this()
        {
            DisplayName = displayName;
            Signed = signed;
        }

        /// <summary>
        /// Gets the display name of the invited user.
        /// </summary>
        [JsonProperty("displayname")]
        public string DisplayName { get; }

        /// <summary>
        /// Gets content which has been signed, which servers can use to verify the event.
        /// </summary>
        /// <remarks>Clients should ignore this.</remarks>
        [JsonProperty("signed")]
        public SignedData Signed { get; }
    }
}
