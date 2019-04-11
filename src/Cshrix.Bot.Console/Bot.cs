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

    using Microsoft.Extensions.Logging;

    internal sealed class Bot
    {
        private readonly ILogger _log;

        private readonly IMatrixClient _client;

        public Bot(ILogger<Bot> log, IMatrixClient client)
        {
            _log = log;
            _client = client;
        }

        public async Task TestAsync()
        {
            _log.LogInformation("Testing");

            var userId = await _client.GetUserIdAsync();

            _log.LogInformation("I am {UserId}", userId);
        }
    }
}
