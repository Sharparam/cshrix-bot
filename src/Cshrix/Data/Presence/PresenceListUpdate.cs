// <copyright file="PresenceListUpdate.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Presence
{
    using System.Collections.Generic;
    using System.Linq;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Describes modifications to make to a user's presence list.
    /// </summary>
    public readonly struct PresenceListUpdate
    {
        /// <inheritdoc />
        public PresenceListUpdate(
            [CanBeNull] IEnumerable<UserId> add = null,
            [CanBeNull] IEnumerable<UserId> remove = null)
            : this(add?.ToList().AsReadOnly(), remove?.ToList().AsReadOnly())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PresenceListUpdate" /> structure.
        /// </summary>
        /// <param name="add">User ID's to add to the presence list.</param>
        /// <param name="remove">User ID's to remove from the presence list.</param>
        [JsonConstructor]
        public PresenceListUpdate(
            [CanBeNull] IReadOnlyCollection<UserId> add = null,
            [CanBeNull] IReadOnlyCollection<UserId> remove = null)
            : this()
        {
            Add = add;
            Remove = remove;
        }

        /// <summary>
        /// Gets a collection of user ID's to add to the presence list.
        /// </summary>
        [JsonProperty(
            "invite",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<UserId> Add { get; }

        /// <summary>
        /// Gets a collection of user ID's to remove from the presence list.
        /// </summary>
        [JsonProperty(
            "drop",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<UserId> Remove { get; }
    }
}
