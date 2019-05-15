// <copyright file="ISynapseAdminApi.cs">
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
    /// Contains definitions for the Synapse admin API.
    /// </summary>
    [PublicAPI]
    public interface ISynapseAdminApi : IApi
    {
        /// <summary>
        /// Gets information about a particular user.
        /// </summary>
        /// <param name="userId">The ID of the user to look up.</param>
        /// <returns>An instance of <see cref="WhoisResponse" /> containing user information.</returns>
        /// <remarks>
        /// This API may be restricted to only be called by the user being looked up, or by a server admin.
        /// Server-local administrator privileges are not specified in this document.
        /// </remarks>
        [Get("_synapse/admin/{apiVersion}/whois/{userId}")]
        Task<WhoisResponse> WhoisAsync([Path] UserId userId);
    }
}
