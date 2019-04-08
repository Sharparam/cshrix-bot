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

    using Configuration;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using RestEase;

    public class MatrixClient : IMatrixClient
    {
        private const string DefaultBaseUrl = "https://matrix.org/_matrix/client";

        private readonly IMatrixApi _api;

        private readonly IOptionsMonitor<MatrixClientConfiguration> _configMonitor;

        public MatrixClient(
            ILogger<MatrixClient> log,
            HttpClient httpClient,
            IOptionsMonitor<MatrixClientConfiguration> clientConfig)
        {
            Log = log;
            httpClient.BaseAddress = clientConfig.CurrentValue.BaseUri ?? new Uri(DefaultBaseUrl);
            _api = RestClient.For<IMatrixApi>(httpClient);
            _configMonitor = clientConfig;
        }

        protected ILogger Log { get; }
    }
}
