// <copyright file="SignedData.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains content that has been signed, which can be used to verify an event.
    /// </summary>
    /// <remarks>
    /// Clients do not usually have a need for this data.
    /// </remarks>
    public class SignedData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignedData" /> structure.
        /// </summary>
        /// <param name="userId">The ID of the user this data relates to.</param>
        /// <param name="signatures">Signatures verifying the data.</param>
        /// <param name="token">A token from a containing object.</param>
        public SignedData(
            UserId userId,
            IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> signatures,
            string token)
        {
            UserId = userId;
            Signatures = signatures;
            Token = token;
        }

        /// <summary>
        /// Gets the user ID this data relates to.
        /// </summary>
        [JsonProperty("mxid")]
        public UserId UserId { get; }

        /// <summary>
        /// Gets a dictionary of signatures verifying this data.
        /// </summary>
        [JsonProperty("signatures")]
        public IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> Signatures { get; }

        /// <summary>
        /// Gets a token from the containing object.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; }
    }
}
