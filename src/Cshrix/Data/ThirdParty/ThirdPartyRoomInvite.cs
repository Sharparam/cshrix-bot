// <copyright file="ThirdPartyRoomInvite.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.ThirdParty
{
    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about a third party invite.
    /// </summary>
    public readonly struct ThirdPartyRoomInvite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThirdPartyRoomInvite" /> structure.
        /// </summary>
        /// <param name="identityServer">
        /// The hostname and port of the identity server which should be used for third party identifier lookups.
        /// </param>
        /// <param name="medium">
        /// The kind of address being passed in the address field, for example <c>email</c>.
        /// </param>
        /// <param name="address">The invitee's third party identifier.</param>
        [JsonConstructor]
        public ThirdPartyRoomInvite(string identityServer, string medium, string address)
            : this()
        {
            IdentityServer = identityServer;
            Medium = medium;
            Address = address;
        }

        /// <summary>
        /// Gets the hostname and port of the identity server.
        /// </summary>
        /// <remarks>
        /// This server is used for the third party identifier lookups.
        /// </remarks>
        [JsonProperty("id_server")]
        private string IdentityServer { get; }

        /// <summary>
        /// Gets the kind of <see cref="Address" />, for example <c>email</c>.
        /// </summary>
        [JsonProperty("medium")]
        public string Medium { get; }

        /// <summary>
        /// Gets the invitee's third party identifier.
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; }
    }
}
