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

    /// <summary>
    /// Specifies filters when retrieving public rooms from a server.
    /// </summary>
    public readonly struct PublicRoomsRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicRoomsRequest" /> struct.
        /// </summary>
        /// <param name="limit">The maximum number of results to return.</param>
        /// <param name="since">A pagination token.</param>
        /// <param name="filter">A filter to apply to the results.</param>
        /// <param name="includeAllNetworks">Whether to include all networks in the search.</param>
        /// <param name="thirdPartyInstanceId">A specific third party network/protocol to request.</param>
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

        /// <summary>
        /// Gets the maximum number of results to return.
        /// </summary>
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; }

        /// <summary>
        /// Gets a pagination token.
        /// </summary>
        /// <remarks>
        /// This token (retrieved from a previous request to get public rooms) controls pagination.
        /// The direction of pagination is specified solely by which token is supplied,
        /// rather than via an explicit flag.
        /// </remarks>
        [JsonProperty(
            "since",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string Since { get; }

        /// <summary>
        /// Gets a filter to apply to the results.
        /// </summary>
        [JsonProperty(
            "filter",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public PublicRoomsFilter? Filter { get; }

        /// <summary>
        /// Gets a value indicating whether to include all known networks/protocols from application services
        /// on the homeserver.
        /// </summary>
        [JsonProperty("include_all_networks", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IncludeAllNetworks { get; }

        /// <summary>
        /// Gets a specific third party network/protocol to request from the homeserver.
        /// </summary>
        /// <remarks>
        /// Can only be used if <see cref="IncludeAllNetworks" /> is <c>false</c>.
        /// </remarks>
        [JsonProperty(
            "third_party_instance_id",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string ThirdPartyInstanceId { get; }
    }
}
