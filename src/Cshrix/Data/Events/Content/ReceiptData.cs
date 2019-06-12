// <copyright file="ReceiptData.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains receipts for an event.
    /// </summary>
    public readonly struct ReceiptData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiptData" /> structure.
        /// </summary>
        /// <param name="read">A dictionary mapping user IDs to their <c>m.read</c> receipts.</param>
        [JsonConstructor]
        public ReceiptData(IReadOnlyDictionary<UserId, UserReceipt> read)
            : this() =>
            Read = read;

        /// <summary>
        /// Gets a dictionary mapping user IDs to their <c>m.read</c> user receipt for this event.
        /// </summary>
        [JsonProperty("m.read")]
        public IReadOnlyDictionary<UserId, UserReceipt> Read { get; }
    }
}
