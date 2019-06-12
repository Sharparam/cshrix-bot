// <copyright file="ApiExtensions.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Extensions
{
    using System;
    using System.Net.Http.Headers;

    using JetBrains.Annotations;

    /// <summary>
    /// Contains extension methods for the <see cref="IApi" /> interface.
    /// </summary>
    public static class ApiExtensions
    {
        /// <summary>
        /// Sets a bearer token to use for authenticating with the API.
        /// </summary>
        /// <param name="api">An instance of <see cref="IMatrixClientServerApi" />.</param>
        /// <param name="accessToken">The access token to set.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="api" /> is <c>null</c>.</exception>
        public static void SetBearerToken([NotNull] this IApi api, string accessToken)
        {
            if (api == null)
            {
                throw new ArgumentNullException(nameof(api));
            }

            api.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}
