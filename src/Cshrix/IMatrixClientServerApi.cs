// <copyright file="IMatrixClientServerApi.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Data;
    using Data.Events;
    using Data.Events.Content;

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

        /// <summary>
        /// Gets or sets the impersonated user.
        /// </summary>
        /// <remarks>
        /// This field is only usable if the token in use belongs to an application service.
        /// </remarks>
        [UsedImplicitly]
        [Query("user_id")]
        Identifier? UserId { get; set; }

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

        [Post("{apiVersion}/createRoom")]
        Task<RoomIdContainer> CreateRoomAsync([Body] CreateRoomRequest data);

        #endregion Room creation

        #region Device management

        [Post("{apiVersion}/delete_devices")]
        Task DeleteDevicesAsync([Body] DeleteDevicesRequest data);

        [Get("{apiVersion}/devices")]
        Task<DevicesResponse> GetDevicesAsync();

        [Delete("{apiVersion}/devices/{deviceId}")]
        Task DeleteDeviceAsync([Path] string deviceId, [Body] AuthenticationContainer authentication);

        [Get("{apiVersion}/devices/{deviceId}")]
        Task<Device> GetDeviceAsync([Path] string deviceId);

        [Put("{apiVersion}/devices/{deviceId}")]
        Task SetDeviceMetadataAsync([Path] string deviceId, [Body] DeviceMetadata data);

        #endregion Device management

        #region Application service room directory management

        [Put("{apiVersion}/directory/list/appservice/{networkId}/{roomId}")]
        Task UpdateAppServiceRoomVisibilityAsync(
            [Path] string networkId,
            [Path] Identifier roomId,
            [Body] RoomVisibilityContainer data);

        #endregion Application service room directory management

        #region Room directory

        [Delete("{apiVersion}/directory/room/{roomAlias}")]
        Task DeleteRoomAliasAsync([Path("roomAlias")] Identifier alias);

        [Get("{apiVersion}/directory/room/{roomAlias}")]
        Task<RoomAliasInformation> ResolveRoomAliasAsync([Path("roomAlias")] Identifier alias);

        [Put("{apiVersion}/directory/room/{roomAlias}")]
        Task MapRoomAliasAsync([Path("roomAlias")] Identifier alias, [Body] RoomIdContainer data);

        #endregion Room directory

        #region Room participation

        [Get("{apiVersion}/rooms/{roomId}/context/{eventId}")]
        Task<EventContext> GetContextAsync([Path] Identifier roomId, [Path] Identifier eventId, [Query] int limit = 10);

        [Get("{apiVersion}/rooms/{roomId}/event/{eventId}")]
        Task<Event> GetEventAsync([Path] Identifier roomId, [Path] Identifier eventId);

        [Get("{apiVersion}/rooms/{roomId}/joined_members")]
        Task<JoinedMembersResponse> GetJoinedMembersAsync([Path] Identifier roomId);

        [Get("{apiVersion}/rooms/{roomId}/members")]
        Task<Chunk<StateEvent>> GetMemberEventsAsync([Path] Identifier roomId);

        Task<PaginatedChunk<RoomEvent>> GetMessageEventsAsync(
            [Path] Identifier roomId,
            [Query] string from = null,
            [Query] string to = null,
            [Query("dir")] Direction direction = Direction.Backwards,
            [Query] int limit = 10,
            [Query] RoomEventFilter filter = default);

        [Post("{apiVersion}/rooms/{roomId}/receipt/{receiptType}/{eventId}")]
        Task SendReceiptAsync(
            [Path] Identifier roomId,
            [Path] string receiptType,
            [Path] Identifier eventId,
            [Body] object data = null);

        [Put("{apiVersion}/rooms/{roomId}/redact/{eventId}/{txnId}")]
        Task<EventIdContainer> RedactEventAsync(
            [Path] Identifier roomId,
            [Path] Identifier eventId,
            [Path("txnId")] int transactionId,
            [Body] RedactionContent data);

        [Put("{apiVersion}/rooms/{roomId}/send/{eventType}/{txnId}")]
        Task<EventIdContainer> SendEventAsync(
            [Path] Identifier roomId,
            [Path] string eventType,
            [Path("txnId")] int transactionId,
            [Body] Event data);

        [Get("{apiVersion}/rooms/{roomId}/state")]
        Task<IReadOnlyCollection<StateEvent>> GetStateEventsAsync([Path] Identifier roomId);

        [Get("{apiVersion}/rooms/{roomId}/state/{eventType}")]
        Task<StateEvent> GetStateEventAsync([Path] Identifier roomId, [Path] string eventType);

        [Put("{apiVersion}/rooms/{roomId}/state/{eventType}")]
        Task<EventIdContainer> SendStateAsync(
            [Path] Identifier roomId,
            [Path] string eventType,
            [Body] EventContent data);

        [Get("{apiVersion}/rooms/{roomId}/state/{eventType}/{stateKey}")]
        Task<StateEvent> GetStateEventAsync([Path] Identifier roomId, [Path] string eventType, [Path] string stateKey);

        [Put("{apiVersion}/rooms/{roomId}/state/{eventType}/{stateKey}")]
        Task<EventIdContainer> SendStateAsync(
            [Path] Identifier roomId,
            [Path] string eventType,
            [Path] string stateKey,
            [Body] EventContent data);

        [Put("{apiVersion}/rooms/{roomId}/typing/{userId}")]
        Task SendTypingAsync([Path] Identifier roomId, [Path] Identifier userId, [Body] TypingState data);

        [Get("{apiVersion}/sync")]
        Task<SyncResponse> SyncAsync(
            [Query] string since = null,
            [Query] string filter = null,
            [Query("full_state")] bool fullState = false,
            [Query("set_presence")] string setPresence = "offline",
            [Query] long timeout = 0);

        [Post("{apiVersion}/user/{userId}/filter")]
        Task<FilterIdContainer> UploadFilterAsync([Path] Identifier userId, [Body] Filter data);

        [Get("{apiVersion}/user/{userId}/filter/{filterId}")]
        Task<Filter> GetFilterAsync([Path] Identifier userId, [Path] string filterId);

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
