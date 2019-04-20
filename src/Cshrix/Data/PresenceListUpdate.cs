// <copyright file="PresenceListUpdate.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public readonly struct PresenceListUpdate
    {
        public PresenceListUpdate(
            [CanBeNull] IEnumerable<Identifier> add = null,
            [CanBeNull] IEnumerable<Identifier> remove = null)
            : this(add?.ToList().AsReadOnly(), remove?.ToList().AsReadOnly())
        {
        }

        [JsonConstructor]
        public PresenceListUpdate(
            [CanBeNull] IReadOnlyCollection<Identifier> add = null,
            [CanBeNull] IReadOnlyCollection<Identifier> remove = null)
            : this()
        {
            Add = add;
            Remove = remove;
        }

        [JsonProperty(
            "invite",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<Identifier> Add { get; }

        [JsonProperty(
            "drop",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<Identifier> Remove { get; }
    }
}
