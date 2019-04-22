// <copyright file="Bot.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Bot.Console
{
    using System;
    using System.Threading.Tasks;

    using Extensions;

    using Microsoft.Extensions.Logging;

    using RestEase;

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

            var pushRules = await _client.GetNotificationPushRulesAsync();

            _log.LogInformation("My push rules are {@Rules}", pushRules);
        }
    }
}
