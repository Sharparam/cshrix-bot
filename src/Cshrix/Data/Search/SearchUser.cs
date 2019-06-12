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

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Describes a user as returned from a search.
    /// </summary>
    public readonly struct SearchUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchUser" /> structure.
        /// </summary>
        /// <param name="userId">The Matrix user ID of the user.</param>
        /// <param name="displayName">The display name of the user.</param>
        /// <param name="avatarUri">The URI to the user's avatar.</param>
        [JsonConstructor]
        public SearchUser(UserId userId, [CanBeNull] string displayName, [CanBeNull] Uri avatarUri)
            : this()
        {
            UserId = userId;
            DisplayName = displayName;
            AvatarUri = avatarUri;
        }

        /// <summary>
        /// Gets the user's Matrix user ID.
        /// </summary>
        [JsonProperty("user_id")]
        public UserId UserId { get; }

        /// <summary>
        /// Gets the display name of the user, if one is set.
        /// </summary>
        [JsonProperty("display_name")]
        [CanBeNull]
        public string DisplayName { get; }

        /// <summary>
        /// Gets the URI to the user's avatar, if they have one set.
        /// </summary>
        [JsonProperty("avatar_url")]
        [CanBeNull]
        public Uri AvatarUri { get; }
    }
}
