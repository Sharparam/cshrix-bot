// <copyright file="SignedSenderData.cs">
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
    /// Contains signed data. Like <see cref="SignedData" /> but additionally has a sender ID.
    /// </summary>
    public class SignedSenderData : SignedData
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="SignedData" /> structure.
        /// </summary>
        /// <param name="userId">The ID of the user this data relates to.</param>
        /// <param name="signatures">Signatures verifying the data.</param>
        /// <param name="token">A token from a containing object.</param>
        /// <param name="sender">The ID of the user that sent the data.</param>
        public SignedSenderData(
            UserId userId,
            IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> signatures,
            string token,
            UserId sender)
            : base(userId, signatures, token) =>
            Sender = sender;

        /// <summary>
        /// Gets the ID of the user that sent this data.
        /// </summary>
        [JsonProperty("sender")]
        public UserId Sender { get; }
    }
}
