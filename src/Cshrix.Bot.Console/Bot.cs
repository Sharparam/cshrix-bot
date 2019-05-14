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

    using Extensions;

    using Microsoft.Extensions.Logging;

    using RestEase;

    /// <summary>
    /// Main bot class.
    /// </summary>
    internal sealed class Bot
    {
        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private readonly ILogger _log;

        /// <summary>
        /// <see cref="MatrixClient" /> instance for this class used to communicate with the Matrix API.
        /// </summary>
        private readonly IMatrixClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bot" /> class.
        /// </summary>
        /// <param name="log">Logger instance to use.</param>
        /// <param name="client"><see cref="MatrixClient" /> instance to use.</param>
        public Bot(ILogger<Bot> log, IMatrixClient client)
        {
            _log = log;
            _client = client;
        }

        /// <summary>
        /// Runs some tests to check the Matrix API.
        /// </summary>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
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

            _log.LogInformation("Starting sync");
            await _client.StartSyncingAsync();
        }
    }
}
