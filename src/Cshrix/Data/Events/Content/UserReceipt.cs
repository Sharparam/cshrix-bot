// <copyright file="UserReceipt.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System;

    using Newtonsoft.Json;

    using Serialization;

    /// <summary>
    /// Describes a user-specific receipt.
    /// </summary>
    public readonly struct UserReceipt
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserReceipt" /> structure.
        /// </summary>
        /// <param name="timestamp">The date and time at which the receipt was sent.</param>
        [JsonConstructor]
        public UserReceipt(DateTimeOffset timestamp)
            : this() =>
            Timestamp = timestamp;

        /// <summary>
        /// Gets the date and time at which the user sent the receipt.
        /// </summary>
        [JsonProperty("ts")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        public DateTimeOffset Timestamp { get; }
    }
}
