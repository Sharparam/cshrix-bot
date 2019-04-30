// <copyright file="IWellKnownApi.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix
{
    using System.Threading.Tasks;

    using Data;

    using JetBrains.Annotations;

    using RestEase;

    /// <summary>
    /// Defines methods available on the <c>.well-known</c> API.
    /// </summary>
    [PublicAPI]
    public interface IWellKnownApi
    {
        /// <summary>
        /// Gets or sets the User-Agent string that will be sent to the API.
        /// </summary>
        [Header("User-Agent", nameof(Cshrix))]
        string UserAgent { get; set; }

        /// <summary>
        /// Gets client information.
        /// </summary>
        /// <returns>An instance of <see cref="ClientInfo" />.</returns>
        [Get(".well-known/matrix/client")]
        Task<ClientInfo> GetClientInfoAsync();
    }
}
