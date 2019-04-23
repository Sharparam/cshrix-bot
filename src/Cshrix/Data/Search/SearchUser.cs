// <copyright file="User.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Search
{
    using System;

    using Newtonsoft.Json;

    public readonly struct SearchUser
    {
        [JsonConstructor]
        public SearchUser(Uri avatarUri, string displayName, UserId userId)
            : this()
        {
            AvatarUri = avatarUri;
            DisplayName = displayName;
            UserId = userId;
        }

        [JsonProperty("avatar_url")]
        public Uri AvatarUri { get; }

        [JsonProperty("display_name")]
        public string DisplayName { get; }

        [JsonProperty("user_id")]
        public UserId UserId { get; }
    }
}
