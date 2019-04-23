// <copyright file="PublicRoomsRequest.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public readonly struct PublicRoomsRequest
    {
        [JsonConstructor]
        public PublicRoomsRequest(
            int? limit = null,
            [CanBeNull] string since = null,
            PublicRoomsFilter? filter = null,
            bool? includeAllNetworks = null,
            [CanBeNull] string thirdPartyInstanceId = null)
            : this()
        {
            Limit = limit;
            Since = since;
            Filter = filter;
            IncludeAllNetworks = includeAllNetworks;
            ThirdPartyInstanceId = thirdPartyInstanceId;
        }

        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; }

        [JsonProperty(
            "since",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string Since { get; }

        [JsonProperty(
            "filter",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public PublicRoomsFilter? Filter { get; }

        [JsonProperty("include_all_networks", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IncludeAllNetworks { get; }

        [JsonProperty(
            "third_party_instance_id",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string ThirdPartyInstanceId { get; }
    }
}
