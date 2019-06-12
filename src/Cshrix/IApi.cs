// <copyright file="IApi.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix
{
    using System.Net.Http.Headers;

    using Data;

    using Extensions;

    using JetBrains.Annotations;

    using RestEase;

    /// <summary>
    /// Contains properties and methods common to all APIs.
    /// </summary>
    public interface IApi
    {
        /// <summary>
        /// Gets or sets the string passed as the User-Agent to the Matrix API.
        /// </summary>
        [UsedImplicitly]
        [Header("User-Agent", nameof(Cshrix))]
        string UserAgent { get; set; }

        /// <summary>
        /// Gets or sets the API version to use.
        /// </summary>
        [UsedImplicitly]
        [Path("apiVersion")]
        string ApiVersion { get; set; }

        /// <summary>
        /// Gets or sets the authorization header value.
        /// </summary>
        /// <remarks>
        /// To set a bearer token directly, the extension method
        /// <see cref="ApiExtensions.SetBearerToken" /> can be used.
        /// </remarks>
        /// <seealso cref="ApiExtensions.SetBearerToken" />
        [UsedImplicitly]
        [Header("Authorization")]
        AuthenticationHeaderValue Authorization { get; set; }

        /// <summary>
        /// Gets or sets the impersonated user.
        /// </summary>
        /// <remarks>
        /// This field is only useful if the token in use belongs to an application service.
        /// </remarks>
        [UsedImplicitly]
        [Query("user_id")]
        UserId UserId { get; set; }
    }
}
