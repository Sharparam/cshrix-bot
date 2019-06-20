// <copyright file="MatrixClient.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Configuration;

    using Data;
    using Data.Events;

    using Events;

    using Extensions;

    using JetBrains.Annotations;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using RestEase;

    using Serialization;

    /// <inheritdoc cref="IMatrixClient" />
    /// <summary>
    /// Implementation of a Matrix client.
    /// </summary>
    public class MatrixClient : IMatrixClient, IDisposable
    {
        /// <summary>
        /// The default base URL for the API that will be used if none is configured.
        /// </summary>
        private const string DefaultBaseUrl = "https://matrix.org";

        /// <summary>
        /// The default API version to use if none is configured.
        /// </summary>
        private const string DefaultApiVersion = "r0";

        /// <summary>
        /// The application logger factory.
        /// </summary>
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// The instance of <see cref="IMatrixClientServerApi" /> to use for API calls.
        /// </summary>
        private readonly IMatrixClientServerApi _api;

        /// <summary>
        /// A listener to use for the sync API.
        /// </summary>
        private readonly SyncListener _syncListener;

        /// <summary>
        /// An options monitor to retrieve the current client configuration.
        /// </summary>
        private readonly IOptionsMonitor<MatrixClientConfiguration> _configMonitor;

        /// <summary>
        /// Contains all known rooms, keyed by their ID.
        /// </summary>
        private readonly ConcurrentDictionary<string, Room> _rooms;

        /// <summary>
        /// The authenticated user's own user ID.
        /// </summary>
        [CanBeNull]
        private UserId _userId;

        // ReSharper disable once SuggestBaseTypeForParameter
        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixClient" /> class.
        /// </summary>
        /// <param name="loggerFactory">A factory to create logger instances.</param>
        /// <param name="httpClient">An instance of <see cref="HttpClient" /> to use for making API calls.</param>
        /// <param name="clientConfig">Client configuration monitor.</param>
        public MatrixClient(
            ILoggerFactory loggerFactory,
            HttpClient httpClient,
            IOptionsMonitor<MatrixClientConfiguration> clientConfig)
        {
            _loggerFactory = loggerFactory;
            Log = loggerFactory.CreateLogger<MatrixClient>();
            var baseUri = clientConfig.CurrentValue.BaseUri ?? new Uri(DefaultBaseUrl);
            httpClient.BaseAddress = baseUri;

            _api = new RestClient(httpClient)
            {
                RequestPathParamSerializer = new StringEnumRequestPathParamSerializer(),
                RequestQueryParamSerializer = new MatrixApiQueryParamSerializer()
            }.For<IMatrixClientServerApi>();

            _configMonitor = clientConfig;
            _api.ApiVersion = _configMonitor.CurrentValue.ApiVersion ?? DefaultApiVersion;
            _api.SetBearerToken(_configMonitor.CurrentValue.AccessToken);

            _syncListener = new SyncListener(loggerFactory.CreateLogger<SyncListener>(), _api);
            _syncListener.Sync += HandleSyncAsync;

            _rooms = new ConcurrentDictionary<string, Room>();
        }

        /// <inheritdoc />
        public event AsyncEventHandler<InvitedEventArgs> Invited;

        /// <inheritdoc />
        public event AsyncEventHandler<JoinedEventArgs> Joined;

        /// <inheritdoc />
        public event AsyncEventHandler<MessageEventArgs> Message;

        /// <inheritdoc />
        public UserId UserId => GetUserId();

        /// <summary>
        /// Gets the <see cref="ILogger" /> for this instance.
        /// </summary>
        protected ILogger Log { get; }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_syncListener != null)
            {
                _syncListener.Sync -= HandleSyncAsync;
                _syncListener.Dispose();
            }
        }

        /// <inheritdoc />
        public Task StartSyncingAsync() => _syncListener.StartAsync();

        /// <inheritdoc />
        public Task StopSyncingAsync() => _syncListener.StopAsync();

        /// <inheritdoc />
        public UserId GetUserId() => GetUserIdAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<UserId> GetUserIdAsync()
        {
            if (_userId != null)
            {
                return _userId;
            }

            var userId = (await _api.WhoAmIAsync().ConfigureAwait(false)).UserId;
            _userId = userId;
            return userId;
        }

        /// <inheritdoc />
        public async Task<PreviewInfo> GetUriPreviewInfoAsync(Uri uri, DateTimeOffset? at = null)
        {
            var info = await _api.GetUriPreviewInfoAsync(uri, at).ConfigureAwait(false);
            return info;
        }

        /// <inheritdoc />
        public async Task<string> JoinRoomByIdAsync(string roomId)
        {
            Log.LogDebug("Joining room {RoomId}", roomId);
            var result = await _api.JoinRoomAsync(roomId).ConfigureAwait(false);
            return result.RoomId;
        }

        /// <summary>
        /// Processes a sync event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="eventArgs">Event arguments.</param>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
        private async Task HandleSyncAsync(object sender, SyncEventArgs eventArgs)
        {
            Log.LogTrace("Handling sync event");
            var response = eventArgs.Response;
            await HandleRoomEventsAsync(response.Rooms);
        }

        /// <summary>
        /// Handles room events from a sync result.
        /// </summary>
        /// <param name="rooms">The <c>rooms</c> part of a sync result.</param>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
        private async Task HandleRoomEventsAsync(SyncedRooms rooms)
        {
            Log.LogTrace("Handling room events");
            await HandleInvitedRoomEventsAsync(rooms.Invited);
            await HandleJoinedRoomEventsAsync(rooms.Joined);
        }

        /// <summary>
        /// Handles events in rooms the user has been invited to.
        /// </summary>
        /// <param name="rooms">Invited rooms from a sync result.</param>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
        private async Task HandleInvitedRoomEventsAsync(IReadOnlyDictionary<string, InvitedRoom> rooms)
        {
            Log.LogTrace("Handling {Count} rooms in the 'invited' state", rooms.Count);
            foreach (var kvp in rooms)
            {
                await HandleInvitedRoomEventAsync(kvp.Key, kvp.Value);
            }
        }

        /// <summary>
        /// Handles events in rooms the user is joined to.
        /// </summary>
        /// <param name="rooms">Joined rooms from a sync result.</param>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
        private async Task HandleJoinedRoomEventsAsync(IReadOnlyDictionary<string, JoinedRoom> rooms)
        {
            Log.LogTrace("Handling {Count} rooms in the 'joined' state", rooms.Count);
            foreach (var kvp in rooms)
            {
                await HandleJoinedRoomEventAsync(kvp.Key, kvp.Value);
            }
        }

        /// <summary>
        /// Handles an invitation to a room.
        /// </summary>
        /// <param name="roomId">ID of the room the user was invited to.</param>
        /// <param name="invitedRoom">Data for the room.</param>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
        private async Task HandleInvitedRoomEventAsync(string roomId, InvitedRoom invitedRoom)
        {
            Log.LogTrace("Handling 'invited' room {RoomId}", roomId);
            var room = await GetOrAddRoomAsync(roomId, Membership.Invited);
            await room.UpdateAsync(invitedRoom);
        }

        /// <summary>
        /// Handles updates to a joined room.
        /// </summary>
        /// <param name="roomId">The ID of the room.</param>
        /// <param name="joinedRoom">Data for the room.</param>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
        private async Task HandleJoinedRoomEventAsync(string roomId, JoinedRoom joinedRoom)
        {
            Log.LogTrace("Handling 'joined' room {RoomId}", roomId);
            var room = await GetOrAddRoomAsync(roomId, Membership.Joined);
            await room.UpdateAsync(joinedRoom);
        }

        /// <summary>
        /// Gets an existing room by ID or adds it if it doesn't exist.
        /// </summary>
        /// <param name="roomId">The ID of the room.</param>
        /// <param name="membership">The initial membership of the room.</param>
        /// <returns>The <see cref="Room" /> instance for the given ID.</returns>
        private async Task<Room> GetOrAddRoomAsync(string roomId, Membership membership)
        {
            var hasRoom = _rooms.TryGetValue(roomId, out var room);

            if (hasRoom)
            {
                return room;
            }

            Log.LogTrace("Room {RoomId} did not exist, adding it", roomId);
            var logger = _loggerFactory.CreateLogger(Room.GenerateLoggerCategory(roomId));
            room = new Room(logger, this, _api, roomId, membership);
            _rooms.TryAdd(roomId, room);
            room.Message += HandleMessageAsync;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (membership)
            {
                case Membership.Invited:
                    await Invited.InvokeAsync(this, new InvitedEventArgs(room));
                    break;

                case Membership.Joined:
                    await Joined.InvokeAsync(this, new JoinedEventArgs(room));
                    break;
            }

            return room;
        }

        /// <summary>
        /// Handles message events from rooms.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="eventArgs">Event arguments.</param>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
        /// <remarks>
        /// This handler simply forwards the message event onto listeners, only changing the <c>sender</c>
        /// of the .NET event.
        /// </remarks>
        private async Task HandleMessageAsync(object sender, MessageEventArgs eventArgs)
        {
            await Message.InvokeAsync(this, eventArgs);
        }
    }
}
