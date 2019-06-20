// <copyright file="SyncListener.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Data;
    using Data.Events;

    using Errors;

    using Extensions;

    using Microsoft.Extensions.Logging;

    using RestEase;

    /// <summary>
    /// Manages a thread to continuously poll the Matrix API for new events.
    /// </summary>
    internal sealed class SyncListener : IDisposable
    {
        /// <summary>
        /// The default amount of time to wait between sync calls.
        /// </summary>
        private static readonly TimeSpan DefaultSyncDelay = TimeSpan.Zero;

        /// <summary>
        /// The default amount of time to wait for a sync response from the server.
        /// </summary>
        private static readonly TimeSpan DefaultSyncTimeout = TimeSpan.FromSeconds(30);

        /// <summary>
        /// The logger instance for this class.
        /// </summary>
        private readonly ILogger _log;

        /// <summary>
        /// An instance of <see cref="IMatrixClientServerApi" /> to use for API calls.
        /// </summary>
        private readonly IMatrixClientServerApi _api;

        /// <summary>
        /// Last received sync token.
        /// </summary>
        private string _token;

        /// <summary>
        /// The task running sync operations.
        /// </summary>
        private Task _syncTask;

        /// <summary>
        /// Cancellation token source to use when wanting to exit the sync thread.
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// Amount of time to wait between sync calls. This will be increased and decreased depending on
        /// if calls to the sync API fail.
        /// </summary>
        private TimeSpan _syncDelay;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncListener" /> class.
        /// </summary>
        /// <param name="log">Logger instance for this class.</param>
        /// <param name="api">API interface to use.</param>
        public SyncListener(ILogger<SyncListener> log, IMatrixClientServerApi api)
        {
            _log = log;
            _api = api;
        }

        /// <summary>
        /// Raised when a new sync response is obtained from the Matrix API.
        /// </summary>
        public event EventHandler<SyncEventArgs> Sync;

        /// <inheritdoc />
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            StopAsync().Wait();
        }

        /// <summary>
        /// Starts the sync listener.
        /// </summary>
        /// <param name="token">Optional token to start syncing from.</param>
        public Task StartAsync(string token = null)
        {
            if (_syncTask?.IsCompleted == false)
            {
                _log.LogDebug("Sync listener already started");
                return Task.CompletedTask;
            }

            _log.LogDebug("Starting sync listener");

            _token = token;
            _cancellationTokenSource = new CancellationTokenSource();
            _syncDelay = DefaultSyncDelay;

            _log.LogDebug("Starting sync task");
            var cancellationToken = _cancellationTokenSource.Token;
            _syncTask = Task.Run(
                async () => await SyncLoop(cancellationToken).ConfigureAwait(false),
                cancellationToken);

            _log.LogDebug("Sync listener started");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Stops the sync listener.
        /// </summary>
        public async Task StopAsync()
        {
            _log.LogDebug("Stopping sync listener");
            _cancellationTokenSource.Cancel();

            _log.LogTrace("Waiting for sync task to finish");
            await _syncTask.ConfigureAwait(false);

            _log.LogDebug("Sync listener stopped");
        }

        /// <summary>
        /// Continuously syncs with the Matrix API until cancelled.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to use if cancellation is desirable.</param>
        private async Task SyncLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await SyncAsync(cancellationToken).ConfigureAwait(false);
                }
                catch (TaskCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    _log.LogInformation("Sync loop was canceled after cancellation was requested");
                }
            }
        }

        /// <summary>
        /// Performs a sync operation against the Matrix API and dispatches any events.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to pass to any async calls.</param>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
        private async Task SyncAsync(CancellationToken cancellationToken)
        {
            try
            {
                _log.LogTrace("Calling sync API with token {Token}", _token);
                var response = await _api.SyncAsync(
                        _token,
                        timeout: DefaultSyncTimeout,
                        cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                if (cancellationToken.IsCancellationRequested)
                {
                    _log.LogDebug("Cancellation of syncing has been requested, aborting sync processing");
                    return;
                }

                _log.LogTrace("Processing data from sync call");

                HandleSyncResponse(response);

                _token = response.NextBatchToken;
                _syncDelay = DefaultSyncDelay;
            }
            catch (ApiException ex) when (ex.TryGetError(out RateLimitError error))
            {
                _log.LogWarning(
                    ex,
                    "Received a rate limit error from sync API, waiting {Delay} before retrying",
                    error.RetryAfter);

                _syncDelay = error.RetryAfter;
            }
            catch (ApiException ex)
            {
                _log.LogError(ex, "Call to sync API failed");
                throw;
            }

            _log.LogTrace("Sync run complete, next batch is {Token}", _token);

            if (_syncDelay > TimeSpan.Zero)
            {
                _log.LogTrace("Delaying next sync call by {Delay}", _syncDelay);
                await Task.Delay(_syncDelay, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Handles a sync response from the Matrix API.
        /// </summary>
        /// <param name="response">The response from the API.</param>
        /// <remarks>Examines the response and dispatches one or multiple events as appropriate.</remarks>
        private void HandleSyncResponse(SyncResponse response)
        {
            Sync?.Invoke(this, new SyncEventArgs(response));
        }
    }
}
