// <copyright file="MemberContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains the contents of a <c>m.room.member</c> event.
    /// </summary>
    public sealed class MemberContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberContent" /> class.
        /// </summary>
        /// <param name="displayName">Display name of the user.</param>
        /// <param name="avatarUri">URI to the user's avatar.</param>
        /// <param name="membership">Current membership state of the user.</param>
        /// <param name="isDirect">Whether the user is joined with the intention of direct messaging.</param>
        /// <param name="thirdPartyInvite">Any third party invite associated with the user/membership.</param>
        public MemberContent(
            string displayName,
            Uri avatarUri,
            Membership membership,
            bool isDirect,
            ThirdPartyInvite? thirdPartyInvite)
        {
            DisplayName = displayName;
            AvatarUri = avatarUri;
            Membership = membership;
            IsDirect = isDirect;
            ThirdPartyInvite = thirdPartyInvite;
        }

        /// <summary>
        /// Gets the display name of the user.
        /// </summary>
        [JsonProperty("displayname")]
        public string DisplayName { get; }

        /// <summary>
        /// Gets the URI for the user's avatar.
        /// </summary>
        [JsonProperty("avatar_url")]
        public Uri AvatarUri { get; }

        /// <summary>
        /// Gets the type of membership the user is currently active as.
        /// </summary>
        [JsonProperty("membership")]
        public Membership Membership { get; }

        /// <summary>
        /// Gets a value indicating whether the user is joined to the room with the intention of direct messaging.
        /// </summary>
        [JsonProperty("is_direct")]
        public bool IsDirect { get; }

        /// <summary>
        /// Gets the related third party invite, if any.
        /// </summary>
        [JsonProperty("third_party_invite")]
        public ThirdPartyInvite? ThirdPartyInvite { get; }
    }
}
