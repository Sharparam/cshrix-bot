// <copyright file="IMatrixClientServerApi.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using Data;
    using Data.Authentication;
    using Data.Events;
    using Data.Events.Content;
    using Data.Notifications;
    using Data.Search;
    using Data.ThirdParty;

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
        AuthenticationHeaderValue Authorization { get; set; }

        /// <summary>
        /// Gets or sets the impersonated user.
        /// </summary>
        /// <remarks>
        /// This field is only usable if the token in use belongs to an application service.
        /// </remarks>
        [UsedImplicitly]
        [Query("user_id")]
        UserId UserId { get; set; }

        #region Server administration

        [Get("_matrix/client/versions")]
        Task<VersionsResponse> GetVersionsAsync();

        [Get("_matrix/client/{apiVersion}/admin/whois/{userId}")]
        Task<WhoisResponse> WhoisAsync([Path] UserId userId);

        #endregion Server administration

        #region User data

        [Get("_matrix/client/{apiVersion}/account/3pid")]
        Task<ThirdPartyIdentifiersResponse> GetThirdPartyIdentifiersAsync();

        [Post("_matrix/client/{apiVersion}/account/3pid")]
        Task AddThirdPartyIdentifierAsync([Body] ThirdPartyIdentifierRegistrationRequest data);

        [Post("_matrix/client/{apiVersion}/account/3pid/delete")]
        Task DeleteThirdPartyIdentifierAsync([Body] ThirdPartyIdentifierDeletionRequest data);

        [Post("_matrix/client/{apiVersion}/account/deactivate")]
        Task<HttpResponseMessage> DeactivateAccountAsync([Body] AuthenticationContainer data);

        [Post("_matrix/client/{apiVersion}/account/password")]
        Task ChangePasswordAsync([Body] ChangePasswordRequest data);

        [Get("_matrix/client/{apiVersion}/account/whoami")]
        Task<UserIdContainer> WhoAmIAsync();

        [Get("_matrix/client/{apiVersion}/profile/{userId}")]
        Task<Profile> GetProfileAsync([Path] UserId userId);

        [Get("_matrix/client/{apiVersion}/profile/{userId}/avatar_url")]
        Task<AvatarUrlContainer> GetAvatarUrlAsync([Path] UserId userId);

        [Put("_matrix/client/{apiVersion}/profile/{userId}/avatar_url")]
        Task SetAvatarUrlAsync([Path] UserId userId, [Body] AvatarUrlContainer data);

        [Get("_matrix/client/{apiVersion}/profile/{userId}/displayname")]
        Task<DisplayNameContainer> GetDisplayNameAsync([Path] UserId userId);

        [Put("_matrix/client/{apiVersion}/profile/{userId}/displayname")]
        Task SetDisplayNameAsync([Path] UserId userId, [Body] DisplayNameContainer data);

        [Post("_matrix/client/{apiVersion}/register")]
        Task<AuthenticationResponse> RegisterAsync([Body] RegistrationRequest data);

        [Get("_matrix/client/{apiVersion}/register/available")]
        Task<AvailableContainer> IsRegistrationAvailableAsync([Query] string username);

        [Put("_matrix/client/{apiVersion}/user/{userId}/account_data/{type}")]
        Task SetAccountDataAsync([Path] UserId userId, [Path] string type, [Body] EventContent data);

        [Put("_matrix/client/{apiVersion}/user/{userId}/rooms/{roomId}/account_data/{type}")]
        Task SetRoomAccountDataAsync(
            [Path] UserId userId,
            [Path] string roomId,
            [Path] string type,
            [Body] EventContent data);

        [Get("_matrix/client/{apiVersion}/user/{userId}/rooms/{roomId}/tags")]
        Task<TagsResponse> GetRoomTagsAsync([Path] UserId userId, [Path] string roomId);

        [Delete("_matrix/client/{apiVersion}/user/{userId}/rooms/{roomId}/tags/{tag}")]
        Task DeleteRoomTagAsync([Path] UserId userId, [Path] string roomId, [Path] string tag);

        [Put("_matrix/client/{apiVersion}/user/{userId}/rooms/{roomId}/tags/{tag}")]
        Task AddRoomTagAsync(
            [Path] UserId userId,
            [Path] string roomId,
            [Path] string tag,
            [Body] TagData data);

        [Post("_matrix/client/{apiVersion}/user_directory/search")]
        Task<UserSearchResult> SearchUsersAsync([Body] UserSearchQuery query);

        #endregion User data

        #region Room creation

        [Post("_matrix/client/{apiVersion}/createRoom")]
        Task<RoomIdContainer> CreateRoomAsync([Body] CreateRoomRequest data);

        #endregion Room creation

        #region Device management

        [Post("_matrix/client/{apiVersion}/delete_devices")]
        Task DeleteDevicesAsync([Body] DeleteDevicesRequest data);

        [Get("_matrix/client/{apiVersion}/devices")]
        Task<DevicesResponse> GetDevicesAsync();

        [Delete("_matrix/client/{apiVersion}/devices/{deviceId}")]
        Task DeleteDeviceAsync([Path] string deviceId, [Body] AuthenticationContainer authentication);

        [Get("_matrix/client/{apiVersion}/devices/{deviceId}")]
        Task<Device> GetDeviceAsync([Path] string deviceId);

        [Put("_matrix/client/{apiVersion}/devices/{deviceId}")]
        Task SetDeviceMetadataAsync([Path] string deviceId, [Body] DeviceMetadata data);

        #endregion Device management

        #region Application service room directory management

        [Put("_matrix/client/{apiVersion}/directory/list/appservice/{networkId}/{roomId}")]
        Task UpdateAppServiceRoomVisibilityAsync(
            [Path] string networkId,
            [Path] string roomId,
            [Body] RoomVisibilityContainer data);

        #endregion Application service room directory management

        #region Room directory

        [Delete("_matrix/client/{apiVersion}/directory/room/{roomAlias}")]
        Task DeleteRoomAliasAsync([Path("roomAlias")] RoomAlias alias);

        [Get("_matrix/client/{apiVersion}/directory/room/{roomAlias}")]
        Task<RoomAliasInformation> ResolveRoomAliasAsync([Path("roomAlias")] RoomAlias alias);

        [Put("_matrix/client/{apiVersion}/directory/room/{roomAlias}")]
        Task MapRoomAliasAsync([Path("roomAlias")] RoomAlias alias, [Body] RoomIdContainer data);

        #endregion Room directory

        #region Room participation

        [Get("_matrix/client/{apiVersion}/rooms/{roomId}/context/{eventId}")]
        Task<EventContext> GetContextAsync([Path] string roomId, [Path] string eventId, [Query] int limit = 10);

        [Get("_matrix/client/{apiVersion}/rooms/{roomId}/event/{eventId}")]
        Task<Event> GetEventAsync([Path] string roomId, [Path] string eventId);

        [Get("_matrix/client/{apiVersion}/rooms/{roomId}/joined_members")]
        Task<JoinedMembersResponse> GetJoinedMembersAsync([Path] string roomId);

        [Get("_matrix/client/{apiVersion}/rooms/{roomId}/members")]
        Task<Chunk<StateEvent>> GetMemberEventsAsync([Path] string roomId);

        [Get("_matrix/client/{apiVersion}/rooms/{roomId}/messages")]
        Task<PaginatedChunk<RoomEvent>> GetMessageEventsAsync(
            [Path] string roomId,
            [Query] string from = null,
            [Query] string to = null,
            [Query("dir")] Direction direction = Direction.Backwards,
            [Query] int limit = 10,
            [Query] RoomEventFilter filter = default);

        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/receipt/{receiptType}/{eventId}")]
        Task SendReceiptAsync(
            [Path] string roomId,
            [Path] string receiptType,
            [Path] string eventId,
            [Body] object data = null);

        [Put("_matrix/client/{apiVersion}/rooms/{roomId}/redact/{eventId}/{txnId}")]
        Task<EventIdContainer> RedactEventAsync(
            [Path] string roomId,
            [Path] string eventId,
            [Path("txnId")] int transactionId,
            [Body] RedactionContent data);

        [Put("_matrix/client/{apiVersion}/rooms/{roomId}/send/{eventType}/{txnId}")]
        Task<EventIdContainer> SendEventAsync(
            [Path] string roomId,
            [Path] string eventType,
            [Path("txnId")] int transactionId,
            [Body] Event data);

        [Get("_matrix/client/{apiVersion}/rooms/{roomId}/state")]
        Task<IReadOnlyCollection<StateEvent>> GetStateEventsAsync([Path] string roomId);

        [Get("_matrix/client/{apiVersion}/rooms/{roomId}/state/{eventType}")]
        Task<StateEvent> GetStateEventAsync([Path] string roomId, [Path] string eventType);

        [Put("_matrix/client/{apiVersion}/rooms/{roomId}/state/{eventType}")]
        Task<EventIdContainer> SendStateAsync(
            [Path] string roomId,
            [Path] string eventType,
            [Body] EventContent data);

        [Get("_matrix/client/{apiVersion}/rooms/{roomId}/state/{eventType}/{stateKey}")]
        Task<StateEvent> GetStateEventAsync([Path] string roomId, [Path] string eventType, [Path] string stateKey);

        [Put("_matrix/client/{apiVersion}/rooms/{roomId}/state/{eventType}/{stateKey}")]
        Task<EventIdContainer> SendStateAsync(
            [Path] string roomId,
            [Path] string eventType,
            [Path] string stateKey,
            [Body] EventContent data);

        [Put("_matrix/client/{apiVersion}/rooms/{roomId}/typing/{userId}")]
        Task SendTypingAsync([Path] string roomId, [Path] UserId userId, [Body] TypingState data);

        [Get("_matrix/client/{apiVersion}/sync")]
        Task<SyncResponse> SyncAsync(
            [Query] string since = null,
            [Query] string filter = null,
            [Query("full_state")] bool fullState = false,
            [Query("set_presence")] string setPresence = "offline",
            [Query] long timeout = 0);

        [Post("_matrix/client/{apiVersion}/user/{userId}/filter")]
        Task<FilterIdContainer> UploadFilterAsync([Path] UserId userId, [Body] Filter data);

        [Get("_matrix/client/{apiVersion}/user/{userId}/filter/{filterId}")]
        Task<Filter> GetFilterAsync([Path] UserId userId, [Path] string filterId);

        #endregion Room participation

        #region Room membership

        // TODO: The docs say that the third party data object should be additionally wrapped in
        // another level `signed`. For now assume that is a doc error because it would be a
        // preposterous amount of nesting. Awaiting response from #matrix-dev:matrix.org
        // on whether this is actually intended.
        [Post("_matrix/client/{apiVersion}/join/{roomIdOrAlias}")]
        Task<RoomIdContainer> JoinRoomOrAliasAsync(
            [Path] string roomIdOrAlias,
            [Query("server_name")] IEnumerable<string> serverNames = null,
            [Body] SignedThirdPartyData? data = null);

        [Get("_matrix/client/{apiVersion}/joined_rooms")]
        Task<JoinedRooms> GetJoinedRoomsAsync();

        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/ban")]
        Task BanAsync([Path] string roomId, [Body] Reason data);

        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/forget")]
        Task ForgetAsync([Path] string roomId);

        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/invite")]
        Task InviteAsync([Path] string roomId, [Body] ThirdPartyRoomInvite data);

        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/invite")]
        Task InviteAsync([Path] string roomId, [Body] UserIdContainer data);

        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/join")]
        Task<RoomIdContainer> JoinRoomAsync([Path] string roomId, [Body] SignedThirdPartyData? data = null);

        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/kick")]
        Task KickAsync([Path] string roomId, [Body] Reason data);

        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/leave")]
        Task LeaveAsync([Path] string roomId);

        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/unban")]
        Task UnbanAsync([Path] string roomId, [Body] UserIdContainer data);

        #endregion Room membership

        #region End-to-end encryption

        [Get("_matrix/client/{apiVersion}/keys/changes")]
        Task<DeviceChangeLists> GetDeviceKeyChangesAsync([Query] string from, [Query] string to);

        [Post("_matrix/client/{apiVersion}/keys/claim")]
        Task<ClaimKeysResponse> ClaimOneTimeKeysAsync([Body] ClaimKeysRequest data);

        [Post("_matrix/client/{apiVersion}/keys/query")]
        Task<DeviceKeysQueryResponse> QueryDeviceKeysAsync([Body] DeviceKeysQuery data);

        [Post("_matrix/client/{apiVersion}/keys/upload")]
        Task<IReadOnlyDictionary<string, int>> UploadDeviceKeysAsync([Body] UploadKeysRequest data);

        #endregion End-to-end encryption

        #region Session management

        [Get("_matrix/client/{apiVersion}/login")]
        Task<LoginFlows> GetLoginFlowsAsync();

        [Post("_matrix/client/{apiVersion}/login")]
        Task<AuthenticationResponse> LoginAsync([Body] LoginRequest data);

        [Post("_matrix/client/{apiVersion}/logout")]
        Task LogoutAsync();

        [Post("_matrix/client/{apiVersion}/logout/all")]
        Task LogoutAllAsync();

        #endregion Session management

        #region Push notifications

        [Get("_matrix/client/{apiVersion}/notifications")]
        Task<NotificationsResponse> GetNotificationsAsync(
            [Query] string from = null,
            [Query] int? limit = null,
            [Query] string only = null);

        [Get("_matrix/client/{apiVersion}/pushers")]
        Task<NotificationPushersContainer> GetNotificationPushersAsync();

        [Post("_matrix/client/{apiVersion}/pushers/set")]
        Task ModifyNotificationPusherAsync([Body] NotificationPusher data);

        [Get("_matrix/client/{apiVersion}/pushrules/{scope}")]
        Task<NotificationRulesets> GetNotificationPushRulesAsync([Path] string scope = null);

        [Get("_matrix/client/{apiVersion}/pushrules/{scope}/{kind}/{ruleId}")]
        Task<NotificationPushRule> GetNotificationPushRuleAsync(
            [Path] string scope,
            [Path] NotificationPushRuleKind kind,
            [Path] string ruleId);

        [Delete("_matrix/client/{apiVersion}/pushrules/{scope}/{kind}/{ruleId}")]
        Task DeleteNotificationPushRuleAsync(
            [Path] string scope,
            [Path] NotificationPushRuleKind kind,
            [Path] string ruleId);

        [Put("_matrix/client/{apiVersion}/pushrules/{scope}/{kind}/{ruleId}")]
        Task SetNotificationPushRuleAsync(
            [Path] string scope,
            [Path] NotificationPushRuleKind kind,
            [Path] string ruleId,
            [Body] NotificationPushRule data,
            [Query] string before = null,
            [Query] string after = null);

        [Put("_matrix/client/{apiVersion}/pushrules/{scope}/{kind}/{ruleId}/actions")]
        Task SetNotificationPushRuleActionsAsync(
            [Path] string scope,
            [Path] NotificationPushRuleKind kind,
            [Path] string ruleId,
            [Body] NotificationActionsContainer data);

        [Put("_matrix/client/{apiVersion}/pushrules/{scope}/{kind}/{ruleId}/enabled")]
        Task SetNotificationPushRuleEnabledAsync(
            [Path] string scope,
            [Path] NotificationPushRuleKind kind,
            [Path] string ruleId,
            [Body] EnabledContainer data);

        #endregion Push notifications

        #region Presence

        [Get("_matrix/client/{apiVersion}/presence/list/{userId}")]
        Task<IReadOnlyCollection<Event>> GetPresenceEventsAsync([Path] UserId userId);

        [Post("_matrix/client/{apiVersion}/presence/list/{userId}")]
        Task UpdatePresenceListAsync([Path] UserId userId, [Body] PresenceListUpdate data);

        [Get("_matrix/client/{apiVersion}/presence/{userId}/status")]
        Task<PresenceStatusResponse> GetPresenceAsync([Path] UserId userId);

        [Put("_matrix/client/{apiVersion}/presence/{userId}/status")]
        Task SetPresenceAsync([Path] UserId userId, [Body] PresenceStatusRequest data);

        #endregion Presence

        #region Room discovery

        [Get("_matrix/client/{apiVersion}/publicRooms")]
        Task<PublicRoomsChunk> GetPublicRoomsAsync(
            [Query] int? limit = null,
            [Query] string since = null,
            [Query()] string server = null);

        [Post("_matrix/client/{apiVersion}/publicRooms")]
        Task<PublicRoomsChunk> GetPublicRoomsAsync([Body] PublicRoomsRequest data);

        #endregion Room discovery

        #region Read Markers

        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/read_markers")]
        Task SetReadMarkersAsync([Path] string roomId, [Body] ReadMarkers data);

        #endregion Read Markers

        #region Reporting content

        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/report/{eventId}")]
        Task ReportAsync([Path] string roomId, [Path] string eventId, [Body] Report data);

        #endregion Reporting content

        #region Search

        [Post("_matrix/client/{apiVersion}/search")]
        Task<SearchResult> SearchAsync([Body] SearchQuery data, [Query("next_batch")] string from = null);

        #endregion Search

        #region Send-to-Device messaging

        [Put("_matrix/client/{apiVersion}/sendToDevice/{eventType}/{txnId}")]
        Task SendToDevicesAsync(
            [Path] string eventType,
            [Path("txnId")] string transactionId,
            [Body] SendToDeviceMessages data);

        #endregion Send-to-Device messaging

        #region OpenID

        [Post("_matrix/client/{apiVersion}/user/{userId}/openid/request_token")]
        Task<OpenIdToken> RequestOpenIdTokenAsync([Path] UserId userId, [Body] object data);

        #endregion OpenID

        #region VOIP

        [Get("_matrix/client/{apiVersion}/voip/turnServer")]
        Task<TurnServerCredentials> GetTurnServerCredentialsAsync();

        #endregion VOIP

        #region Media

        [Post("_matrix/media/{apiVersion}/upload")]
        Task<ContentUriContainer> UploadAsync(
            [Query] string filename,
            [Header("Content-Type")] MediaTypeHeaderValue contentType,
            [Body] Stream data);

        [Post("_matrix/media/{apiVersion}/upload")]
        Task<ContentUriContainer> UploadAsync(
            [Query] string filename,
            [Header("Content-Type")] string contentType,
            [Body] Stream data);

        [Post("_matrix/media/{apiVersion}/upload")]
        Task<ContentUriContainer> UploadAsync(
            [Query] string filename,
            [Header("Content-Type")] MediaTypeHeaderValue contentType,
            [Body] byte[] data);

        [Post("_matrix/media/{apiVersion}/upload")]
        Task<ContentUriContainer> UploadAsync(
            [Query] string filename,
            [Header("Content-Type")] string contentType,
            [Body] byte[] data);

        [Get("_matrix/media/{apiVersion}/download/{serverName}/{mediaId}")]
        Task<HttpResponseMessage> DownloadAsync(
            [Path] string serverName,
            [Path] string mediaId,
            [Query("allow_remote")] bool? allowRemote = null);

        [Get("_matrix/media/{apiVersion}/download/{serverName}/{mediaId}/{filename}")]
        Task<HttpResponseMessage> DownloadAsync(
            [Path] string serverName,
            [Path] string mediaId,
            [Path] string filename,
            [Query("allow_remote")] bool? allowRemote = null);

        [Get("_matrix/media/{apiVersion}/thumbnail/{serverName}/{mediaId}")]
        Task<HttpResponseMessage> DownloadThumbnailAsync(
            [Path] string serverName,
            [Path] string mediaId,
            [Query] int width,
            [Query] int height,
            [Query("method", QuerySerializationMethod.Serialized)] ResizeMethod resizeMethod = ResizeMethod.Scale,
            [Query("allow_remote")] bool? allowRemote = null);

        [Get("_matrix/media/{apiVersion}/preview_url")]
        Task<PreviewInfo> GetUriPreviewInfoAsync(
            [Query("url")] Uri uri,
            [Query("ts")] long? timestamp = null);

        [Get("_matrix/media/{apiVersion}/config")]
        Task<ContentConfiguration> GetContentConfigurationAsync();

        #endregion Media
    }
}
