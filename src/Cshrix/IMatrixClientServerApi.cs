// <copyright file="IMatrixClientServerApi.cs">
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

    public interface IMatrixClientServerApi
    {
        [UsedImplicitly]
        [Header("User-Agent", nameof(Cshrix))]
        string UserAgent { get; set; }

        [UsedImplicitly]
        [Path("apiVersion")]
        string ApiVersion { get; set; }

        [UsedImplicitly]
        [Header("Authorization")]
        string Authorization { get; set; }

        #region Server administration

        [Get("versions")]
        Task<VersionsResponse> GetVersionsAsync();

        [Get("{apiVersion}/admin/whois/{userId}")]
        Task<WhoisResponse> WhoisAsync([Path] UserId userId);

        #endregion Server administration

        #region User data

        [Get("{apiVersion}/account/whoami")]
        Task<UserIdContainer> WhoAmIAsync();

        [Post("{apiVersion}/user_directory/search")]
        Task<UserSearchResult> SearchUsersAsync([Body] UserSearchQuery query);

        #endregion User data

        #region Room creation
        #endregion Room creation

        #region Device management
        #endregion Device management

        #region Application service room directory management
        #endregion Application service room directory management

        #region Room directory
        #endregion Room directory

        #region Room participation
        #endregion Room participation

        #region Room membership
        #endregion Room membership

        #region End-to-end encryption
        #endregion End-to-end encryption

        #region Session management
        #endregion Session management

        #region Push notifications
        #endregion Push notifications

        #region Presence
        #endregion Presence

        #region Room discovery
        #endregion Room discovery

        #region Read Markers
        #endregion Read Markers

        #region Reporting content
        #endregion Reporting content

        #region Search
        #endregion Search

        #region Send-to-Device messaging
        #endregion Send-to-Device messaging

        #region OpenID
        #endregion OpenID

        #region VOIP
        #endregion VOIP

        #region Media
        #endregion Media
    }
}
