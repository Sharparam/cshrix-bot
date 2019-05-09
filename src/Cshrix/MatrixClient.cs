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
    using System.Net.Http;
    using System.Threading.Tasks;

    using Configuration;

    using Data;
    using Data.Notifications;

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
        }

        /// <summary>
        /// Gets the <see cref="ILogger" /> for this instance.
        /// </summary>
        protected ILogger Log { get; }

        /// <inheritdoc />
        public void Dispose()
        {
            _syncListener?.Dispose();
        }

        /// <inheritdoc />
        public void StartSyncing() => _syncListener.Start();

        /// <inheritdoc />
        public Task StopSyncing() => _syncListener.Stop();

        /// <inheritdoc />
        public async Task<UserId> GetUserIdAsync() => (await _api.WhoAmIAsync().ConfigureAwait(false)).UserId;

        /// <inheritdoc />
        public Task<NotificationRulesets> GetNotificationPushRulesAsync() => _api.GetNotificationPushRulesAsync();

        /// <inheritdoc />
        public async Task<PreviewInfo> GetPreviewInfoAsync(Uri uri, DateTimeOffset? at = null)
        {
            var info = await _api.GetUriPreviewInfoAsync(uri, at).ConfigureAwait(false);
            return info;
        }
    }
}
