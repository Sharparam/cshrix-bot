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

    using JetBrains.Annotations;

    /// <summary>
    /// A client interface providing an easier way to interact with the Matrix API.
    /// </summary>
    [PublicAPI]
    public interface IMatrixClient
    {
        /// <summary>
        /// Raised when the user is invited to a room.
        /// </summary>
        event EventHandler<InvitedEventArgs> Invited;

        /// <summary>
        /// Raised when the user joins a room.
        /// </summary>
        event EventHandler<JoinedEventArgs> Joined;

        /// <summary>
        /// Raised when a message is received.
        /// </summary>
        event EventHandler<MessageEventArgs> Message;

        /// <summary>
        /// Gets the current user's ID.
        /// </summary>
        UserId UserId { get; }

        /// <summary>
        /// Starts syncing with the Matrix API.
        /// </summary>
        Task StartSyncingAsync();

        /// <summary>
        /// Stops syncing with the Matrix API.
        /// </summary>
        Task StopSyncingAsync();

        /// <summary>
        /// Gets the current user's ID.
        /// </summary>
        /// <returns>The current user's ID.</returns>
        UserId GetUserId();

        /// <summary>
        /// Gets the current user's ID.
        /// </summary>
        /// <returns>The current user's ID.</returns>
        Task<UserId> GetUserIdAsync();

        /// <summary>
        /// Gets preview information for a URI.
        /// </summary>
        /// <param name="uri">The URI to get preview information for.</param>
        /// <param name="at">The point in time at which to get information from.</param>
        /// <returns>Information about the URI.</returns>
        Task<PreviewInfo> GetUriPreviewInfoAsync(Uri uri, DateTimeOffset? at = null);

        /// <summary>
        /// Joins a room by its ID.
        /// </summary>
        /// <param name="roomId">The ID of the room to join.</param>
        /// <returns>The ID of the joined room.</returns>
        Task<string> JoinRoomByIdAsync(string roomId);
    }
}
