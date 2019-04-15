// <copyright file="RoomNameContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System.Collections.Generic;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public class RoomNameContent : EventContent
    {
        public RoomNameContent([NotNull] IDictionary<string, object> dictionary)
            : base(dictionary) =>
            Name = GetValueOrDefault<string>("name");

        [JsonProperty("name")]
        public string Name { get; }
    }
}