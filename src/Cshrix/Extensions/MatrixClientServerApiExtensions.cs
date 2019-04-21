// <copyright file="MatrixApiExtensions.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Extensions
{
    using System;
    using System.Threading.Tasks;

    using Data;
    using Data.Events;

    public static class MatrixClientServerApiExtensions
    {
        public static void SetBearerToken(this IMatrixClientServerApi api, string accessToken) =>
            api.Authorization = $"Bearer {accessToken}";

        public static Task<SyncResponse> SyncAsync(
            this IMatrixClientServerApi api,
            string since = null,
            string filter = null,
            bool fullState = false,
            string setPresence = "offline",
            TimeSpan timeout = default) =>
            api.SyncAsync(since, filter, fullState, setPresence, (long)timeout.TotalMilliseconds);

        public static Task<OpenIdToken> RequestOpenIdTokenAsync(this IMatrixClientServerApi api, UserId userId) =>
            api.RequestOpenIdTokenAsync(userId, new object());
    }
}
