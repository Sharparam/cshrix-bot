// <copyright file="IMatrixClient.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix
{
    using System;
    using System.Threading.Tasks;

    using Data;
    using Data.Notifications;

    using JetBrains.Annotations;

    /// <summary>
    /// A client interface providing an easier way to interact with the Matrix API.
    /// </summary>
    [PublicAPI]
    public interface IMatrixClient
    {
        /// <summary>
        /// Gets the current user's ID.
        /// </summary>
        /// <returns>The current user's ID.</returns>
        Task<UserId> GetUserIdAsync();

        /// <summary>
        /// Gets all configured notification push rules for the current user.
        /// </summary>
        /// <returns>Configured notification push rules.</returns>
        Task<NotificationRulesets> GetNotificationPushRulesAsync();

        /// <summary>
        /// Gets preview information for a URL.
        /// </summary>
        /// <param name="uri">The URI to get preview information for.</param>
        /// <param name="at">The point in time at which to get information from.</param>
        /// <returns>Information about the URI.</returns>
        Task<PreviewInfo> GetPreviewInfoAsync(Uri uri, DateTimeOffset? at = null);
    }
}
