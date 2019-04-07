// <copyright file="IMatrixApi.cs">
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

    using RestEase;

    public interface IMatrixApi
    {
        [Header("User-Agent", nameof(Cshrix))]
        string UserAgent { get; set; }

        [Path("apiVersion")]
        string ApiVersion { get; set; }

        [Header("Authorization")]
        string Authorization { get; set; }

        [Get("versions")]
        Task<VersionsResponse> GetVersionsAsync();

        [Post("{apiVersion}/user_directory/search")]
        Task<UserSearchResult> SearchUsersAsync([Body] UserSearchQuery query);
    }
}
