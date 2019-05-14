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

    public sealed class MemberContent : EventContent
    {
        public MemberContent(
            Uri avatarUri,
            string displayName,
            Membership membership,
            bool isDirect,
            ThirdPartyInvite? thirdPartyInvite)
        {
            AvatarUri = avatarUri;
            DisplayName = displayName;
            Membership = membership;
            IsDirect = isDirect;
            ThirdPartyInvite = thirdPartyInvite;
        }

        [JsonProperty("avatar_url")]
        public Uri AvatarUri { get; }

        [JsonProperty("displayname")]
        public string DisplayName { get; }

        [JsonProperty("membership")]
        public Membership Membership { get; }

        [JsonProperty("is_direct")]
        public bool IsDirect { get; }

        [JsonProperty("third_party_invite")]
        public ThirdPartyInvite? ThirdPartyInvite { get; }
    }
}
