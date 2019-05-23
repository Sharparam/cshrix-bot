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

    using Extensions;

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
        public event EventHandler<InvitedEventArgs> Invited;

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
        public async Task<UserId> GetUserIdAsync() => (await _api.WhoAmIAsync().ConfigureAwait(false)).UserId;

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
        private void HandleSyncAsync(object sender, SyncEventArgs eventArgs)
        {
            Log.LogTrace("Handling sync event");
            var response = eventArgs.Response;
            HandleRoomEvents(response.Rooms);
        }

        private void HandleRoomEvents(SyncedRooms rooms)
        {
            Log.LogTrace("Handling room events");
            HandleInvitedRoomEvents(rooms.Invited);
        }

        private void HandleInvitedRoomEvents(IReadOnlyDictionary<string, InvitedRoom> rooms)
        {
            Log.LogTrace("Handling {Count} rooms in the 'invited' state", rooms.Count);
            foreach (var kvp in rooms)
            {
                HandleInvitedRoomEvent(kvp.Key, kvp.Value);
            }
        }

        private void HandleInvitedRoomEvent(string roomId, InvitedRoom invitedRoom)
        {
            Log.LogTrace("Handling 'invited' room {RoomId}", roomId);
            var hasRoom = _rooms.TryGetValue(roomId, out var room);

            if (!hasRoom)
            {
                Log.LogTrace("Invited room {RoomId} did not exist, adding it", roomId);
                room = Room.FromInvitedRoom(roomId, invitedRoom);
                _rooms.TryAdd(roomId, room);
            }

            Invited?.Invoke(this, new InvitedEventArgs(room));
        }
    }
}
