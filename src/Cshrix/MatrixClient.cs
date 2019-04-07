// <copyright file="MatrixClient.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix
{
    using Configuration;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class MatrixClient : IMatrixClient
    {
        private readonly IOptionsMonitor<MatrixClientConfiguration> _configMonitor;

        public MatrixClient(ILogger<MatrixClient> log, IOptionsMonitor<MatrixClientConfiguration> clientConfig)
        {
            _configMonitor = clientConfig;
            Log = log;
        }

        protected ILogger Log { get; }
    }
}
