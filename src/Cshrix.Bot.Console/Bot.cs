// <copyright file="Bot.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Bot.Console
{
    using System.Threading.Tasks;

    using Data;

    using Extensions;

    using Microsoft.Extensions.Logging;

    using RestEase;

    /// <summary>
    /// Main bot class.
    /// </summary>
    internal sealed class Bot
    {
        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private readonly ILogger _log;

        /// <summary>
        /// <see cref="MatrixClient" /> instance for this class used to communicate with the Matrix API.
        /// </summary>
        private readonly IMatrixClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bot" /> class.
        /// </summary>
        /// <param name="log">Logger instance to use.</param>
        /// <param name="client"><see cref="MatrixClient" /> instance to use.</param>
        public Bot(ILogger<Bot> log, IMatrixClient client)
        {
            _log = log;
            _client = client;
            _client.Invited += OnRoomInvite;
            _client.Joined += OnRoomJoin;
        }

        /// <summary>
        /// Runs some tests to check the Matrix API.
        /// </summary>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
        public async Task TestAsync()
        {
            _log.LogInformation("Testing");

            try
            {
                var userId = await _client.GetUserIdAsync();
                _log.LogInformation("I am {UserId}", userId);
            }
            catch (ApiException ex)
            {
                var hasError = ex.TryGetError(out var error);

                if (hasError)
                {
                    _log.LogError(ex, "Failed to get user ID: {@Error}", error);
                }
                else
                {
                    _log.LogError(ex, "Failed to get user ID, no error reported.");
                }
            }

            _log.LogInformation("Starting sync");
            await _client.StartSyncingAsync();
        }

        /// <summary>
        /// Handles an invite to a room.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private async void OnRoomInvite(object sender, InvitedEventArgs eventArgs)
        {
            _log.LogInformation("I've been invited to a room! Attempting to join {RoomId}...", eventArgs.Room.Id);

            try
            {
                await _client.JoinRoomByIdAsync(eventArgs.Room.Id);
            }
            catch (ApiException ex)
            {
                _log.LogError(ex, "Failed to join room {RoomId}", eventArgs.Room.Id);
            }
        }

        /// <summary>
        /// Handles the user joining a room.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private async void OnRoomJoin(object sender, JoinedEventArgs eventArgs)
        {
            _log.LogInformation("I've joined the room {RoomId}!", eventArgs.Room.Id);

            var room = eventArgs.Room;

            if (room.IsTombstoned)
            {
                await HandleTombstonedRoom(room);
            }
        }

        /// <summary>
        /// Handles a room which has been tombstoned.
        /// </summary>
        /// <param name="room">The room that has been tombstoned.</param>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
        private async Task HandleTombstonedRoom(IRoom room)
        {
            var content = room.TombstoneContent;

            if (content == null)
            {
                _log.LogError("Room {RoomId} was tombstoned but no tombstone content exists", room.Id);
                return;
            }

            _log.LogInformation(
                "Room {RoomId} has been tombstoned (\"{Message}\")! Replacement room is {ReplacementRoomId}",
                room.Id,
                content.Message,
                content.ReplacementRoomId);

            _log.LogInformation("Attempting to join replacement room {RoomId}", content.ReplacementRoomId);
            var joinedId = await _client.JoinRoomByIdAsync(content.ReplacementRoomId);
            _log.LogInformation("Successfully joined {RoomId}", joinedId);
        }
    }
}
