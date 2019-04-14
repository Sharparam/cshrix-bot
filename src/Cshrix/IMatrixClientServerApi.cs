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
        Task<WhoisResponse> WhoisAsync([Path] Identifier userId);

        #endregion Server administration

        #region User data

        [Get("{apiVersion}/account/3pid")]
        Task<ThirdPartyIdentifiersResponse> GetThirdPartyIdentifiersAsync();

        [Post("{apiVersion}/account/3pid")]
        Task AddThirdPartyIdentifierAsync([Body] ThirdPartyIdentifierRegistrationRequest data);

        [Post("{apiVersion}/account/3pid/delete")]
        Task DeleteThirdPartyIdentifierAsync([Body] ThirdPartyIdentifierDeletionRequest data);

        [Post("{apiVersion}/account/deactivate")]
        Task DeactivateAccountAsync([Body] AuthenticationContainer data);

        [Post("{apiVersion}/account/password")]
        Task ChangePasswordAsync([Body] ChangePasswordRequest data);

        [Get("{apiVersion}/account/whoami")]
        Task<UserIdContainer> WhoAmIAsync();

        [Get("{apiVersion}/profile/{userId}")]
        Task<Profile> GetProfileAsync([Path] Identifier userId);

        [Get("{apiVersion}/profile/{userId}/avatar_url")]
        Task<AvatarUrlContainer> GetAvatarUrlAsync([Path] Identifier userId);

        [Put("{apiVersion}/profile/{userId}/avatar_url")]
        Task SetAvatarUrlAsync([Path] Identifier userId, [Body] AvatarUrlContainer data);

        [Get("{apiVersion}/profile/{userId}/displayname")]
        Task<DisplayNameContainer> GetDisplayNameAsync([Path] Identifier userId);

        [Put("{apiVersion}/profile/{userId}/displayname")]
        Task SetDisplayNameAsync([Path] Identifier userId, [Body] DisplayNameContainer data);

        [Post("{apiVersion}/register")]
        Task<RegistrationResponse> RegisterAsync([Body] RegistrationRequest data);

        [Get("{apiVersion}/register/available")]
        Task<AvailableContainer> IsRegistrationAvailableAsync([Query] string username);

        [Put("{apiVersion}/user/{userId}/account_data/{type}")]
        Task SetAccountDataAsync([Path] Identifier userId, [Path] string type, [Body] object data);

        [Put("{apiVersion}/user/{userId}/rooms/{roomId}/account_data/{type}")]
        Task SetRoomAccountDataAsync(
            [Path] Identifier userId,
            [Path] Identifier roomId,
            [Path] string type,
            [Body] object data);

        [Get("{apiVersion}/user/{userId}/rooms/{roomId}/tags")]
        Task<TagsResponse> GetRoomTagsAsync([Path] Identifier userId, [Path] Identifier roomId);

        [Delete("{apiVersion}/user/{userId}/rooms/{roomId}/tags/{tag}")]
        Task DeleteRoomTagAsync([Path] Identifier userId, [Path] Identifier roomId, [Path] string tag);

        [Put("{apiVersion}/user/{userId}/rooms/{roomId}/tags/{tag}")]
        Task AddRoomTagAsync(
            [Path] Identifier userId,
            [Path] Identifier roomId,
            [Path] string tag,
            [Body] TagData data);

        [Post("{apiVersion}/user_directory/search")]
        Task<UserSearchResult> SearchUsersAsync([Body] UserSearchQuery query);

        #endregion User data

        #region Room creation

        #endregion Room creation

        #region Device management

        [Get("{apiVersion}/devices")]
        Task<DevicesResponse> GetDevicesAsync();

        [Get("{apiVersion}/devices/{deviceId}")]
        Task<Device> GetDeviceAsync([Path] string deviceId);

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

        [Post("{apiVersion}/user/{userId}/openid/request_token")]
        Task<OpenIdToken> RequestOpenIdTokenAsync([Path] Identifier userId, [Body] object data);

        #endregion OpenID

        #region VOIP

        [Get("{apiVersion}/voip/turnServer")]
        Task<TurnServerCredentials> GetTurnServerCredentialsAsync();

        #endregion VOIP

        #region Media

        #endregion Media
    }
}
