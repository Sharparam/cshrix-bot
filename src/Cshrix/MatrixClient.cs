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

    public class MatrixClient : IMatrixClient
    {
        private const string DefaultBaseUrl = "https://matrix.org";

        private const string DefaultApiVersion = "r0";

        private readonly IMatrixClientServerApi _api;

        private readonly IOptionsMonitor<MatrixClientConfiguration> _configMonitor;

        public MatrixClient(
            ILogger<MatrixClient> log,
            HttpClient httpClient,
            IOptionsMonitor<MatrixClientConfiguration> clientConfig)
        {
            Log = log;
            var baseUri = clientConfig.CurrentValue.BaseUri ?? new Uri(DefaultBaseUrl);
            httpClient.BaseAddress = baseUri;

            _api = new RestClient(httpClient)
            {
                RequestQueryParamSerializer = new QuoteStrippingJsonRequestQueryParamSerializer()
            }.For<IMatrixClientServerApi>();

            _configMonitor = clientConfig;
            _api.ApiVersion = _configMonitor.CurrentValue.ApiVersion ?? DefaultApiVersion;
            _api.SetBearerToken(_configMonitor.CurrentValue.AccessToken);
        }

        protected ILogger Log { get; }

        public async Task<UserId> GetUserIdAsync() => (await _api.WhoAmIAsync()).UserId;

        public Task<NotificationRulesets> GetNotificationPushRulesAsync() => _api.GetNotificationPushRulesAsync();

        public async Task<PreviewInfo> GetPreviewInfoAsync(Uri uri, DateTimeOffset? at = null)
        {
            var info = await _api.GetUriPreviewInfoAsync(uri, at);
            return info;
        }
    }
}
