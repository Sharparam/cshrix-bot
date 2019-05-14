// <copyright file="RedactionContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public sealed class RedactionContent : EventContent
    {
        public RedactionContent(string reason) => Reason = reason;

        [JsonProperty("reason")]
        [CanBeNull]
        public string Reason { get; }
    }
}
