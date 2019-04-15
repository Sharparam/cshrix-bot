// <copyright file="AliasesContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using JetBrains.Annotations;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class AliasesContent : EventContent
    {
        public AliasesContent([NotNull] IDictionary<string, object> dictionary)
            : base(dictionary)
        {
            var hasAliases = TryGetValue<JArray>("aliases", out var aliases);
            Aliases = hasAliases
                ? aliases.ToObject<ReadOnlyCollection<Identifier>>()
                : Enumerable.Empty<Identifier>().ToList().AsReadOnly();
        }

        [JsonProperty("aliases")]
        public IReadOnlyCollection<Identifier> Aliases { get; }
    }
}
