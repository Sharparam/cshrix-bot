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

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using RestEase;

    public class MatrixClient : IMatrixClient
    {
        private const string DefaultBaseUrl = "https://matrix.org";

        private const string ClientServerSlug = "_matrix/client";

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
            var clientServerBase = new Uri(baseUri, ClientServerSlug);
            httpClient.BaseAddress = clientServerBase;
            _api = RestClient.For<IMatrixClientServerApi>(httpClient);
            _configMonitor = clientConfig;
            _api.ApiVersion = clientConfig.CurrentValue.ApiVersion ?? DefaultApiVersion;
        }

        protected ILogger Log { get; }

        public async Task<UserId> GetUserIdAsync() => (await _api.WhoAmIAsync()).UserId;
    }
}
