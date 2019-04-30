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

    using Errors;

    using Extensions;

    using JetBrains.Annotations;

    using RestEase;

    /// <summary>
    /// Contains definitions for the Matrix Client-Server API.
    /// </summary>
    /// <remarks>
    /// For details, refer to <a href="https://matrix.org/docs/spec/client_server/r0.4.0.html">Client-Server API</a>
    /// and <a href="https://matrix.org/docs/api/client-server/">Swagger documentation</a> on
    /// the official Matrix site.
    /// </remarks>
    [PublicAPI]
    public interface IMatrixClientServerApi
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
        /// <see cref="MatrixClientServerApiExtensions.SetBearerToken" /> can be used.
        /// </remarks>
        /// <seealso cref="MatrixClientServerApiExtensions.SetBearerToken" />
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

        #region Server administration

        /// <summary>
        /// Gets the versions of the specification supported by the server.
        /// </summary>
        /// <returns>An instance of <see cref="VersionsResponse" /> containing a list of supported versions.</returns>
        /// <remarks>
        /// <para>Values will take the form <c>rX.Y.Z</c>.</para>
        /// <para>
        /// Only the latest <c>Z</c> value will be reported for each supported <c>X.Y</c> value.
        /// I.E.: If the server implements <c>r0.0.0</c>, <c>r0.0.1</c>, and <c>r1.2.0</c>, it will report
        /// <c>r0.0.1</c> and <c>r1.2.0</c>.
        /// </para>
        /// </remarks>
        [Get("_matrix/client/versions")]
        Task<VersionsResponse> GetVersionsAsync();

        /// <summary>
        /// Gets information about a particular user.
        /// </summary>
        /// <param name="userId">The ID of the user to look up.</param>
        /// <returns>An instance of <see cref="WhoisResponse" /> containing user information.</returns>
        /// <remarks>
        /// This API may be restricted to only be called by the user being looked up, or by a server admin.
        /// Server-local administrator privileges are not specified in this document.
        /// </remarks>
        [Get("_matrix/client/{apiVersion}/admin/whois/{userId}")]
        Task<WhoisResponse> WhoisAsync([Path] UserId userId);

        #endregion Server administration

        #region User data

        /// <summary>
        /// Gets a list of a user's third party identifiers.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="ThirdPartyIdentifiersResponse" /> containing third party identifiers.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Gets a list of the third party identifiers that the homeserver has associated with the user's account.
        /// </para>
        /// <para>
        /// This is not the same as the list of third party identifiers
        /// bound to the user's Matrix ID in identity servers.
        /// </para>
        /// <para>
        /// Identifiers in this list may be used by the homeserver as, for example, identifiers that it will accept
        /// to reset the user's account password.
        /// </para>
        /// </remarks>
        [Get("_matrix/client/{apiVersion}/account/3pid")]
        Task<ThirdPartyIdentifiersResponse> GetThirdPartyIdentifiersAsync();

        /// <summary>
        /// Adds contact information to the user's account.
        /// </summary>
        /// <param name="data">Contact information to add.</param>
        /// <returns>A <see cref="Task" /> that represents request progress.</returns>
        [Post("_matrix/client/{apiVersion}/account/3pid")]
        Task AddThirdPartyIdentifierAsync([Body] ThirdPartyIdentifierRegistrationRequest data);

        /// <summary>
        /// Deletes a third party identifier from the user's account.
        /// </summary>
        /// <param name="data">Request data to identify which third party identifier to delete.</param>
        /// <returns>A <see cref="Task" /> that represents request progress.</returns>
        /// <remarks>This might not cause an unbind of the identifier from the identity server.</remarks>
        [Post("_matrix/client/{apiVersion}/account/3pid/delete")]
        Task DeleteThirdPartyIdentifierAsync([Body] ThirdPartyIdentifierDeletionRequest data);

        /// <summary>
        /// Deactivates a user's account.
        /// </summary>
        /// <param name="data">Authentication data.</param>
        /// <returns>A <see cref="HttpResponseMessage" /> with details on the API response.</returns>
        /// <remarks>
        /// <para>Deactivates the user's account, removing all ability for the user to login again.</para>
        /// <para>
        /// This API endpoint uses the
        /// <a href="https://matrix.org/docs/spec/client_server/r0.4.0.html#user-interactive-authentication-api">
        ///     User-Interactive Authentication API
        /// </a>.
        /// </para>
        /// <para>An access token should be submitted to this endpoint if the client has an active session.</para>
        /// <para>
        /// The homeserver may change the flows available depending on whether a valid access token is provided.
        /// </para>
        /// <para>
        /// Due to the interactive nature of this endpoint, the returned <see cref="HttpResponseMessage" /> must
        /// be inspected for <c>401</c> status and a returned <see cref="UnauthorizedError" /> error.
        /// This can be more easily done by using the extension methods defined in
        /// <see cref="HttpResponseMessageExtensions" />.
        /// </para>
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/account/deactivate")]
        Task<HttpResponseMessage> DeactivateAccountAsync([Body] AuthenticationContainer data);

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <param name="data">Password and authentication data.</param>
        /// <returns>A <see cref="HttpResponseMessage" /> with details on the API response.</returns>
        /// <remarks>
        /// <para>Changes the password for an account on the homeserver.</para>
        /// <para>
        /// This API endpoint uses the
        /// <a href="https://matrix.org/docs/spec/client_server/r0.4.0.html#user-interactive-authentication-api">
        ///     User-Interactive Authentication API
        /// </a>.
        /// </para>
        /// <para>An access token should be submitted to this endpoint if the client has an active session.</para>
        /// <para>The homeserver may change the flows available depending on whether a valid access token is provided.</para>
        /// <para>
        /// Due to the interactive nature of this endpoint, the returned <see cref="HttpResponseMessage" /> must
        /// be inspected for <c>401</c> status and a returned <see cref="UnauthorizedError" /> error.
        /// This can be more easily done by using the extension methods defined in
        /// <see cref="HttpResponseMessageExtensions" />.
        /// </para>
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/account/password")]
        Task<HttpResponseMessage> ChangePasswordAsync([Body] ChangePasswordRequest data);

        /// <summary>
        /// Gets information about the owner of an access token.
        /// </summary>
        /// <returns>A wrapper object containing the <see cref="UserId" /> the token belongs to.</returns>
        [Get("_matrix/client/{apiVersion}/account/whoami")]
        Task<UserIdContainer> WhoAmIAsync();

        /// <summary>
        /// Gets a user's profile information.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve a profile for.</param>
        /// <returns>Profile information for the user.</returns>
        /// <remarks>
        /// Gets the combined profile information for a user. This API may be used to fetch the user's own profile
        /// information or other users; either locally or on remote homeservers. This API may return keys which are
        /// not limited to <c>displayname</c> or <c>avatar_url</c>.
        /// </remarks>
        [Get("_matrix/client/{apiVersion}/profile/{userId}")]
        Task<Profile> GetProfileAsync([Path] UserId userId);

        /// <summary>
        /// Get the user's avatar <see cref="Uri" />.
        /// </summary>
        /// <param name="userId">The user whose avatar <see cref="Uri" /> to get.</param>
        /// <returns>A wrapper object containing a <see cref="Uri" /> for the avatar.</returns>
        /// <remarks>
        /// <para>
        /// Get the user's avatar <see cref="Uri" />. This API may be used to fetch the user's own avatar
        /// <see cref="Uri" /> or to query the <see cref="Uri" /> of other users; either locally
        /// or on remote homeservers.
        /// </para>
        /// <para>
        /// The contents of the avatar can be downloaded using, for example,
        /// <see cref="MatrixClientServerApiExtensions.DownloadContentAsync(IMatrixClientServerApi, Uri, string, bool?)" />.
        /// </para>
        /// </remarks>
        [Get("_matrix/client/{apiVersion}/profile/{userId}/avatar_url")]
        Task<AvatarUriContainer> GetAvatarUrlAsync([Path] UserId userId);

        /// <summary>
        /// Set the user's avatar <see cref="Uri" />.
        /// </summary>
        /// <param name="userId">The user whose avatar <see cref="Uri" /> to set.</param>
        /// <param name="data">Wrapper object containing the <see cref="Uri" /> to set.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// This API sets the given user's avatar <see cref="Uri" />. You must have permission to set this
        /// user's avatar <see cref="Uri" />, e.g. you need to have their <c>access_token</c>.
        /// </remarks>
        [Put("_matrix/client/{apiVersion}/profile/{userId}/avatar_url")]
        Task SetAvatarUrlAsync([Path] UserId userId, [Body] AvatarUriContainer data);

        /// <summary>
        /// Get the user's display name.
        /// </summary>
        /// <param name="userId">The user whose display name to get.</param>
        /// <returns>A wrapper object containing the display name.</returns>
        /// <remarks>
        /// This API may be used to fetch the user's own display name or to query the name of other users;
        /// either locally or on remote homeservers.
        /// </remarks>
        [Get("_matrix/client/{apiVersion}/profile/{userId}/displayname")]
        Task<DisplayNameContainer> GetDisplayNameAsync([Path] UserId userId);

        /// <summary>
        /// Set the user's display name.
        /// </summary>
        /// <param name="userId">The user whose display name to set.</param>
        /// <param name="data">Wrapper object containing the display name to set.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// This API sets the given user's display name. You must have permission to set this user's display name,
        /// e.g. you need to have their <c>access_token</c>.
        /// </remarks>
        [Put("_matrix/client/{apiVersion}/profile/{userId}/displayname")]
        Task SetDisplayNameAsync([Path] UserId userId, [Body] DisplayNameContainer data);

        /// <summary>
        /// Register for an account on this homeserver.
        /// </summary>
        /// <param name="data">Registration data.</param>
        /// <returns>
        /// A <see cref="Response{T}" /> object containing an <see cref="AuthenticationResponse" /> for
        /// the registered user, on success.
        /// On failure, inspect the returned response for an <see cref="UnauthorizedError" /> for details on
        /// what flows to use for authentication.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This API endpoint uses the
        /// <a href="https://matrix.org/docs/spec/client_server/r0.4.0.html#user-interactive-authentication-api">
        ///     User-Interactive Authentication API
        /// </a>.
        /// </para>
        /// <para>Register for an account on this homeserver.</para>
        /// <para>
        /// There are two kinds of user account:
        /// <list type="bullet">
        /// <item><description>
        /// <c>user</c> accounts. These accounts may use the full API described in this specification.
        /// </description></item>
        /// <item><description>
        /// <c>guest</c> accounts. These accounts may have limited permissions and may not be supported by all servers.
        /// </description></item>
        /// </list>
        /// </para>
        /// <para>
        /// If registration is successful, this endpoint will issue an access token the client can use to
        /// authorize itself in subsequent requests.
        /// </para>
        /// <para>
        /// If the client does not supply a <see cref="RegistrationRequest.DeviceId" />,
        /// the server must auto-generate one.
        /// </para>
        /// <para>
        /// The server <em>should</em> register an account with a user ID based on
        /// the <see cref="RegistrationRequest.Username" /> provided, if any. Note that the grammar of Matrix
        /// user ID localparts is restricted, so the server <em>must</em> either map the provided
        /// <see cref="RegistrationRequest.Username" /> onto a <see cref="UserId" /> in a logical manner,
        /// or reject usernames which do not comply to the grammar, with <c>M_INVALID_USERNAME</c>.
        /// </para>
        /// <para>
        /// Matrix clients <em>must not</em> assume that <see cref="Identifier.Localpart" /> of the registered
        /// <see cref="UserId" /> matches the provided <see cref="RegistrationRequest.Username" />.
        /// </para>
        /// <para>
        /// The returned access token must be associated with the <see cref="RegistrationRequest.DeviceId" />
        /// supplied by the client or generated by the server. The server may invalidate any access token
        /// previously associated with that device. See
        /// <a href="https://matrix.org/docs/spec/client_server/r0.4.0.html#relationship-between-access-tokens-and-devices">
        /// Relationship between access tokens and devices
        /// </a>.
        /// </para>
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/register")]
        Task<Response<AuthenticationResponse>> RegisterAsync([Body] RegistrationRequest data);

        /// <summary>
        /// Checks to see if a username is available on the server.
        /// </summary>
        /// <param name="username">The username to check the availability of.</param>
        /// <returns>
        /// <para>
        /// The <see cref="Response{T}" /> from the API, containing an <see cref="AvailableContainer" /> on success.
        /// If the request failed, the response should be inspected for a <see cref="MatrixError" /> explaining
        /// in more detail why it failed or why the username was not available.
        /// </para>
        /// <para>
        /// The Matrix developers, in their infinite wisdom, has created yet another useless wrapper object for
        /// this API, so the <see cref="AvailableContainer" /> never has to actually be inspected on a successful
        /// API call, as it will always be <c>true</c>.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>Checks to see if a username is available, and valid, for the server.</para>
        /// <para>
        /// The server should check to ensure that, at the time of the request, the username requested is available
        /// for use. This includes verifying that an application service has not claimed the username and that
        /// the username fits the server's desired requirements (for example, a server could dictate that it does not
        /// permit usernames with underscores).
        /// </para>
        /// <para>
        /// Matrix clients may wish to use this API prior to attempting registration, however the clients must also
        /// be aware that using this API does not normally reserve the username. This can mean that the username
        /// becomes unavailable between checking its availability and attempting to register it.
        /// </para>
        /// </remarks>
        [Get("_matrix/client/{apiVersion}/register/available")]
        Task<Response<AvailableContainer>> IsRegistrationAvailableAsync([Query] string username);

        /// <summary>
        /// Set some account data for the user.
        /// </summary>
        /// <param name="userId">The ID of the user to set account data for.</param>
        /// <param name="type">
        /// The event type of the account data to set. Custom types should be namespaced to avoid clashes.
        /// </param>
        /// <param name="data">The content of the account data.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// Set some account data for the client. This config is only visible to the user that set the account data.
        /// The config will be synced to clients in the top-level <c>account_data</c> on sync.
        /// </remarks>
        [Put("_matrix/client/{apiVersion}/user/{userId}/account_data/{type}")]
        Task SetAccountDataAsync([Path] UserId userId, [Path] string type, [Body] EventContent data);

        /// <summary>
        /// Set some account data for the user.
        /// </summary>
        /// <param name="userId">The ID of the user to set account data for.</param>
        /// <param name="roomId">The ID of the room to set account data on.</param>
        /// <param name="type">
        /// The event type of the account data to set. Custom types should be namespaced to avoid clashes.
        /// </param>
        /// <param name="data">The content of the account data.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// Set some account data for the client on a given room. This config is only visible to the user that set
        /// the account data. The config will be synced to clients in the per-room <c>account_data</c>.
        /// </remarks>
        [Put("_matrix/client/{apiVersion}/user/{userId}/rooms/{roomId}/account_data/{type}")]
        Task SetRoomAccountDataAsync(
            [Path] UserId userId,
            [Path] string roomId,
            [Path] string type,
            [Body] EventContent data);

        /// <summary>
        /// Get the tags for a room.
        /// </summary>
        /// <param name="userId">The ID of the user to get tags for.</param>
        /// <param name="roomId">The ID of the room to get tags for.</param>
        /// <returns>The list of tags for the user for the room.</returns>
        /// <remarks>
        /// List the tags set by a user on a room.
        /// </remarks>
        [Get("_matrix/client/{apiVersion}/user/{userId}/rooms/{roomId}/tags")]
        Task<TagsResponse> GetRoomTagsAsync([Path] UserId userId, [Path] string roomId);

        /// <summary>
        /// Delete a tag from a room.
        /// </summary>
        /// <param name="userId">The ID of the user to remove a tag for.</param>
        /// <param name="roomId">The ID of the room to remove a tag from.</param>
        /// <param name="tag">The tag to remove.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        [Delete("_matrix/client/{apiVersion}/user/{userId}/rooms/{roomId}/tags/{tag}")]
        Task DeleteRoomTagAsync([Path] UserId userId, [Path] string roomId, [Path] string tag);

        /// <summary>
        /// Add a tag to a room.
        /// </summary>
        /// <param name="userId">The ID of the user to add a tag for.</param>
        /// <param name="roomId">The ID of the room to add a tag to.</param>
        /// <param name="tag">The tag to add.</param>
        /// <param name="data">Extra data for the tag.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        [Put("_matrix/client/{apiVersion}/user/{userId}/rooms/{roomId}/tags/{tag}")]
        Task AddRoomTagAsync(
            [Path] UserId userId,
            [Path] string roomId,
            [Path] string tag,
            [Body] TagData data);

        /// <summary>
        /// Searches the user directory.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="acceptLanguage">Content to set in the <c>Accept-Language</c> header for the request.</param>
        /// <returns>The users matching the search query.</returns>
        /// <remarks>
        /// <para>
        /// Performs a search for users on the homeserver. The homeserver may determine which subset of users are
        /// searched, however the homeserver <em>must</em> at a minimum consider the users the requesting user shares
        /// a room with and those who reside in public rooms (known to the homeserver). The search <em>must</em>
        /// consider local users to the homeserver, and <em>should</em> query remote users as part of the search.
        /// </para>
        /// <para>
        /// The search is performed case-insensitively on user IDs and display names preferably using a collation
        /// determined based upon the <paramref name="acceptLanguage"/> value provided, if any.
        /// </para>
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/user_directory/search")]
        Task<UserSearchResult> SearchUsersAsync(
            [Body] UserSearchQuery query,
            [Header("Accept-Language")] string acceptLanguage = null);

        #endregion User data

        #region Room creation

        /// <summary>
        /// Create a new room.
        /// </summary>
        /// <param name="data">The desired room configuration.</param>
        /// <returns>A wrapper object containing the ID of the created room.</returns>
        /// <remarks>
        /// <para>Creates a new room with various configuration options.</para>
        /// <para>
        /// The server <em>must</em> apply the normal state resolution rules when creating the new room, including
        /// checking power levels for each event. It <em>must</em> apply the events implied by the request in the
        /// following order:
        ///
        ///  1. A default <c>m.room.power_levels</c> event, giving the room creator (and not other members) permission
        ///     to send state events. Overridden by the <see cref="CreateRoomRequest.PowerLevelsOverride" /> parameter.
        ///  2. Events set by the <see cref="CreateRoomRequest.Preset" />. Currently these are the
        ///     <c>m.room.join_rules</c>, <c>m.room.history_visibility</c>, and <c>m.room.guest_access</c>
        ///     state events.
        ///  3. Events listed in <see cref="CreateRoomRequest.InitialState" />, in the order that they are listed.
        ///  4. Events implied by <see cref="CreateRoomRequest.Name" /> and <see cref="CreateRoomRequest.Topic" />
        ///     (<c>m.room.name</c> and <c>m.room.topic</c> state events).
        ///  5. Invite events implied by <see cref="CreateRoomRequest.Invites" /> and
        ///     <see cref="CreateRoomRequest.ThirdPartyInvites" /> (<c>m.room.member</c> with
        ///     <see cref="Membership.Invited" /> and <c>m.room.third_party_invite</c>).
        /// </para>
        /// <para>
        /// The available presets do the following with respect to room state:
        ///
        /// | Preset | <see cref="JoinRule" /> | <see cref="HistoryVisibility" /> | <see cref="GuestAccess" /> | Other |
        /// | ------ | ----------------------- | -------------------------------- | -------------------------- | ----- |
        /// | <see cref="RoomPreset.PrivateChat" /> | <see cref="JoinRule.Invite" /> | <see cref="HistoryVisibility.Shared" /> | <see cref="GuestAccess.CanJoin" /> ||
        /// | <see cref="RoomPreset.TrustedPrivateChat" /> | <see cref="JoinRule.Invite" /> | <see cref="HistoryVisibility.Shared" /> | <see cref="GuestAccess.CanJoin" /> | All invitees are given the same power level as the room creator. |
        /// | <see cref="RoomPreset.PublicChat" /> | <see cref="JoinRule.Public" /> | <see cref="HistoryVisibility.Shared" /> | <see cref="GuestAccess.CanJoin" /> ||
        /// </para>
        /// <para>
        /// The server will create a <c>m.room.create</c> event in the room with the requesting user as the creator,
        /// alongside other keys provided in <see cref="CreateRoomRequest.Content" />.
        /// </para>
        /// </remarks>
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
            [Query(QuerySerializationMethod.Serialized)] TimeSpan timeout = default);

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
            [Query("ts", QuerySerializationMethod.Serialized)] DateTimeOffset? at = null);

        [Get("_matrix/media/{apiVersion}/config")]
        Task<ContentConfiguration> GetContentConfigurationAsync();

        #endregion Media
    }
}
