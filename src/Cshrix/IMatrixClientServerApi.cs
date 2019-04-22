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
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using Data;
    using Data.Events;
    using Data.Events.Content;
    using Data.Notifications;

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

        [Get("versions")]
        Task<VersionsResponse> GetVersionsAsync();

        [Get("{apiVersion}/admin/whois/{userId}")]
        Task<WhoisResponse> WhoisAsync([Path] UserId userId);

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
        Task<Profile> GetProfileAsync([Path] UserId userId);

        [Get("{apiVersion}/profile/{userId}/avatar_url")]
        Task<AvatarUrlContainer> GetAvatarUrlAsync([Path] UserId userId);

        [Put("{apiVersion}/profile/{userId}/avatar_url")]
        Task SetAvatarUrlAsync([Path] UserId userId, [Body] AvatarUrlContainer data);

        [Get("{apiVersion}/profile/{userId}/displayname")]
        Task<DisplayNameContainer> GetDisplayNameAsync([Path] UserId userId);

        [Put("{apiVersion}/profile/{userId}/displayname")]
        Task SetDisplayNameAsync([Path] UserId userId, [Body] DisplayNameContainer data);

        [Post("{apiVersion}/register")]
        Task<AuthenticationResponse> RegisterAsync([Body] RegistrationRequest data);

        [Get("{apiVersion}/register/available")]
        Task<AvailableContainer> IsRegistrationAvailableAsync([Query] string username);

        [Put("{apiVersion}/user/{userId}/account_data/{type}")]
        Task SetAccountDataAsync([Path] UserId userId, [Path] string type, [Body] EventContent data);

        [Put("{apiVersion}/user/{userId}/rooms/{roomId}/account_data/{type}")]
        Task SetRoomAccountDataAsync(
            [Path] UserId userId,
            [Path] string roomId,
            [Path] string type,
            [Body] EventContent data);

        [Get("{apiVersion}/user/{userId}/rooms/{roomId}/tags")]
        Task<TagsResponse> GetRoomTagsAsync([Path] UserId userId, [Path] string roomId);

        [Delete("{apiVersion}/user/{userId}/rooms/{roomId}/tags/{tag}")]
        Task DeleteRoomTagAsync([Path] UserId userId, [Path] string roomId, [Path] string tag);

        [Put("{apiVersion}/user/{userId}/rooms/{roomId}/tags/{tag}")]
        Task AddRoomTagAsync(
            [Path] UserId userId,
            [Path] string roomId,
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
            [Path] string roomId,
            [Body] RoomVisibilityContainer data);

        #endregion Application service room directory management

        #region Room directory

        [Delete("{apiVersion}/directory/room/{roomAlias}")]
        Task DeleteRoomAliasAsync([Path("roomAlias")] RoomAlias alias);

        [Get("{apiVersion}/directory/room/{roomAlias}")]
        Task<RoomAliasInformation> ResolveRoomAliasAsync([Path("roomAlias")] RoomAlias alias);

        [Put("{apiVersion}/directory/room/{roomAlias}")]
        Task MapRoomAliasAsync([Path("roomAlias")] RoomAlias alias, [Body] RoomIdContainer data);

        #endregion Room directory

        #region Room participation

        [Get("{apiVersion}/rooms/{roomId}/context/{eventId}")]
        Task<EventContext> GetContextAsync([Path] string roomId, [Path] string eventId, [Query] int limit = 10);

        [Get("{apiVersion}/rooms/{roomId}/event/{eventId}")]
        Task<Event> GetEventAsync([Path] string roomId, [Path] string eventId);

        [Get("{apiVersion}/rooms/{roomId}/joined_members")]
        Task<JoinedMembersResponse> GetJoinedMembersAsync([Path] string roomId);

        [Get("{apiVersion}/rooms/{roomId}/members")]
        Task<Chunk<StateEvent>> GetMemberEventsAsync([Path] string roomId);

        [Get("{apiVersion}/rooms/{roomId}/messages")]
        Task<PaginatedChunk<RoomEvent>> GetMessageEventsAsync(
            [Path] string roomId,
            [Query] string from = null,
            [Query] string to = null,
            [Query("dir")] Direction direction = Direction.Backwards,
            [Query] int limit = 10,
            [Query] RoomEventFilter filter = default);

        [Post("{apiVersion}/rooms/{roomId}/receipt/{receiptType}/{eventId}")]
        Task SendReceiptAsync(
            [Path] string roomId,
            [Path] string receiptType,
            [Path] string eventId,
            [Body] object data = null);

        [Put("{apiVersion}/rooms/{roomId}/redact/{eventId}/{txnId}")]
        Task<EventIdContainer> RedactEventAsync(
            [Path] string roomId,
            [Path] string eventId,
            [Path("txnId")] int transactionId,
            [Body] RedactionContent data);

        [Put("{apiVersion}/rooms/{roomId}/send/{eventType}/{txnId}")]
        Task<EventIdContainer> SendEventAsync(
            [Path] string roomId,
            [Path] string eventType,
            [Path("txnId")] int transactionId,
            [Body] Event data);

        [Get("{apiVersion}/rooms/{roomId}/state")]
        Task<IReadOnlyCollection<StateEvent>> GetStateEventsAsync([Path] string roomId);

        [Get("{apiVersion}/rooms/{roomId}/state/{eventType}")]
        Task<StateEvent> GetStateEventAsync([Path] string roomId, [Path] string eventType);

        [Put("{apiVersion}/rooms/{roomId}/state/{eventType}")]
        Task<EventIdContainer> SendStateAsync(
            [Path] string roomId,
            [Path] string eventType,
            [Body] EventContent data);

        [Get("{apiVersion}/rooms/{roomId}/state/{eventType}/{stateKey}")]
        Task<StateEvent> GetStateEventAsync([Path] string roomId, [Path] string eventType, [Path] string stateKey);

        [Put("{apiVersion}/rooms/{roomId}/state/{eventType}/{stateKey}")]
        Task<EventIdContainer> SendStateAsync(
            [Path] string roomId,
            [Path] string eventType,
            [Path] string stateKey,
            [Body] EventContent data);

        [Put("{apiVersion}/rooms/{roomId}/typing/{userId}")]
        Task SendTypingAsync([Path] string roomId, [Path] UserId userId, [Body] TypingState data);

        [Get("{apiVersion}/sync")]
        Task<SyncResponse> SyncAsync(
            [Query] string since = null,
            [Query] string filter = null,
            [Query("full_state")] bool fullState = false,
            [Query("set_presence")] string setPresence = "offline",
            [Query] long timeout = 0);

        [Post("{apiVersion}/user/{userId}/filter")]
        Task<FilterIdContainer> UploadFilterAsync([Path] UserId userId, [Body] Filter data);

        [Get("{apiVersion}/user/{userId}/filter/{filterId}")]
        Task<Filter> GetFilterAsync([Path] UserId userId, [Path] string filterId);

        #endregion Room participation

        #region Room membership

        // TODO: The docs say that the third party data object should be additionally wrapped in
        // another level `signed`. For now assume that is a doc error because it would be a
        // preposterous amount of nesting. Awaiting response from #matrix-dev:matrix.org
        // on whether this is actually intended.
        [Post("{apiVersion}/join/{roomIdOrAlias}")]
        Task<RoomIdContainer> JoinRoomOrAliasAsync(
            [Path] string roomIdOrAlias,
            [Query("server_name")] IEnumerable<string> serverNames = null,
            [Body] SignedThirdPartyData? data = null);

        [Get("{apiVersion}/joined_rooms")]
        Task<JoinedRooms> GetJoinedRoomsAsync();

        [Post("{apiVersion}/rooms/{roomId}/ban")]
        Task BanAsync([Path] string roomId, [Body] Reason data);

        [Post("{apiVersion}/rooms/{roomId}/forget")]
        Task ForgetAsync([Path] string roomId);

        [Post("{apiVersion}/rooms/{roomId}/invite")]
        Task InviteAsync([Path] string roomId, [Body] ThirdPartyRoomInvite data);

        [Post("{apiVersion}/rooms/{roomId}/invite")]
        Task InviteAsync([Path] string roomId, [Body] UserIdContainer data);

        [Post("{apiVersion}/rooms/{roomId}/join")]
        Task<RoomIdContainer> JoinRoomAsync([Path] string roomId, [Body] SignedThirdPartyData? data = null);

        [Post("{apiVersion}/rooms/{roomId}/kick")]
        Task KickAsync([Path] string roomId, [Body] Reason data);

        [Post("{apiVersion}/rooms/{roomId}/leave")]
        Task LeaveAsync([Path] string roomId);

        [Post("{apiVersion}/rooms/{roomId}/unban")]
        Task UnbanAsync([Path] string roomId, [Body] UserIdContainer data);

        #endregion Room membership

        #region End-to-end encryption

        [Get("{apiVersion}/keys/changes")]
        Task<DeviceChangeLists> GetDeviceKeyChangesAsync([Query] string from, [Query] string to);

        [Post("{apiVersion}/keys/claim")]
        Task<ClaimKeysResponse> ClaimOneTimeKeysAsync([Body] ClaimKeysRequest data);

        [Post("{apiVersion}/keys/query")]
        Task<DeviceKeysQueryResponse> QueryDeviceKeysAsync([Body] DeviceKeysQuery data);

        [Post("{apiVersion}/keys/upload")]
        Task<IReadOnlyDictionary<string, int>> UploadDeviceKeysAsync([Body] UploadKeysRequest data);

        #endregion End-to-end encryption

        #region Session management

        [Get("{apiVersion}/login")]
        Task<LoginFlows> GetLoginFlowsAsync();

        [Post("{apiVersion}/login")]
        Task<AuthenticationResponse> LoginAsync([Body] LoginRequest data);

        [Post("{apiVersion}/logout")]
        Task LogoutAsync();

        [Post("{apiVersion}/logout/all")]
        Task LogoutAllAsync();

        #endregion Session management

        #region Push notifications

        [Get("{apiVersion}/notifications")]
        Task<NotificationsResponse> GetNotificationsAsync(
            [Query] string from = null,
            [Query] int? limit = null,
            [Query] string only = null);

        [Get("{apiVersion}/pushers")]
        Task<NotificationPushersContainer> GetNotificationPushersAsync();

        [Post("{apiVersion}/pushers/set")]
        Task ModifyNotificationPusherAsync([Body] NotificationPusher data);

        [Get("{apiVersion}/pushrules/{scope}")]
        Task<NotificationRulesets> GetNotificationPushRulesAsync([Path] string scope = null);

        [Get("{apiVersion}/pushrules/{scope}/{kind}/{ruleId}")]
        Task<NotificationPushRule> GetNotificationPushRuleAsync(
            [Path] string scope,
            [Path] NotificationPushRuleKind kind,
            [Path] string ruleId);

        [Delete("{apiVersion}/pushrules/{scope}/{kind}/{ruleId}")]
        Task DeleteNotificationPushRuleAsync(
            [Path] string scope,
            [Path] NotificationPushRuleKind kind,
            [Path] string ruleId);

        [Put("{apiVersion}/pushrules/{scope}/{kind}/{ruleId}")]
        Task SetNotificationPushRuleAsync(
            [Path] string scope,
            [Path] NotificationPushRuleKind kind,
            [Path] string ruleId,
            [Body] NotificationPushRule data,
            [Query] string before = null,
            [Query] string after = null);

        [Put("{apiVersion}/pushrules/{scope}/{kind}/{ruleId}/actions")]
        Task SetNotificationPushRuleActionsAsync(
            [Path] string scope,
            [Path] NotificationPushRuleKind kind,
            [Path] string ruleId,
            [Body] NotificationActionsContainer data);

        [Put("{apiVersion}/pushrules/{scope}/{kind}/{ruleId}/enabled")]
        Task SetNotificationPushRuleEnabledAsync(
            [Path] string scope,
            [Path] NotificationPushRuleKind kind,
            [Path] string ruleId,
            [Body] EnabledContainer data);

        #endregion Push notifications

        #region Presence

        [Get("{apiVersion}/presence/list/{userId}")]
        Task<IReadOnlyCollection<Event>> GetPresenceEventsAsync([Path] UserId userId);

        [Post("{apiVersion}/presence/list/{userId}")]
        Task UpdatePresenceListAsync([Path] UserId userId, [Body] PresenceListUpdate data);

        [Get("{apiVersion}/presence/{userId}/status")]
        Task<PresenceStatusResponse> GetPresenceAsync([Path] UserId userId);

        [Put("{apiVersion}/presence/{userId}/status")]
        Task SetPresenceAsync([Path] UserId userId, [Body] PresenceStatusRequest data);

        #endregion Presence

        #region Room discovery

        #endregion Room discovery

        #region Read Markers

        [Post("{apiVersion}/rooms/{roomId}/read_markers")]
        Task SetReadMarkersAsync([Path] string roomId, [Body] ReadMarkers data);

        #endregion Read Markers

        #region Reporting content

        [Post("{apiVersion}/rooms/{roomId}/report/{eventId}")]
        Task ReportAsync([Path] string roomId, [Path] string eventId, [Body] Report data);

        #endregion Reporting content

        #region Search

        #endregion Search

        #region Send-to-Device messaging

        [Put("{apiVersion}/sendToDevice/{eventType}/{txnId}")]
        Task SendToDevicesAsync(
            [Path] string eventType,
            [Path("txnId")] string transactionId,
            [Body] SendToDeviceMessages data);

        #endregion Send-to-Device messaging

        #region OpenID

        [Post("{apiVersion}/user/{userId}/openid/request_token")]
        Task<OpenIdToken> RequestOpenIdTokenAsync([Path] UserId userId, [Body] object data);

        #endregion OpenID

        #region VOIP

        [Get("{apiVersion}/voip/turnServer")]
        Task<TurnServerCredentials> GetTurnServerCredentialsAsync();

        #endregion VOIP

        #region Media

        #endregion Media
    }
}
