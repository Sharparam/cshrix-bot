// <copyright file="GuestAccessContent.cs">
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
    /// Contains data for the <c>m.room.guest_access</c> event.
    /// </summary>
    public sealed class GuestAccessContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GuestAccessContent" /> class.
        /// </summary>
        /// <param name="access">Guest access level.</param>
        public GuestAccessContent(GuestAccess access) => Access = access;

        /// <summary>
        /// Gets an enum value specifying how guests can interact with the room.
        /// </summary>
        [JsonProperty("guest_access")]
        public GuestAccess Access { get; }
    }
}
