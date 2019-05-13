// <copyright file="NotificationsResponse.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Notifications
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains notification data.
    /// </summary>
    public readonly struct NotificationsResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationsResponse" /> structure.
        /// </summary>
        /// <param name="notifications">A collection of events that triggered notifications.</param>
        /// <param name="nextToken">A token that can be used to paginate through notification events, if any.</param>
        [JsonConstructor]
        public NotificationsResponse(IReadOnlyCollection<Notification> notifications, string nextToken = null)
            : this()
        {
            Notifications = notifications;
            NextToken = nextToken;
        }

        /// <summary>
        /// Gets the collection of events that triggered notifications.
        /// </summary>
        [JsonProperty("notifications")]
        public IReadOnlyCollection<Notification> Notifications { get; }

        /// <summary>
        /// Gets the token to supply in the <c>from</c> parameter of the next <c>/notifications</c> request in order
        /// to request more events. If this is <c>null</c>, there are no more results.
        /// </summary>
        [JsonProperty("next_token")]
        public string NextToken { get; }
    }
}
