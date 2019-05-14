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
    using System.Threading;
    using System.Threading.Tasks;

    using Cryptography;

    using Data;
    using Data.Authentication;
    using Data.Devices;
    using Data.Events;
    using Data.Events.Content;
    using Data.Notifications;
    using Data.Presence;
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

        /// <summary>
        /// Bulk deletion of devices.
        /// </summary>
        /// <param name="data">Data specifying what devices to delete, along with authentication.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// <para>
        /// This API endpoint uses the
        /// <a href="https://matrix.org/docs/spec/client_server/r0.4.0.html#user-interactive-authentication-api">
        ///     User-Interactive Authentication API
        /// </a>.
        /// </para>
        /// <para>Deletes the given devices, and invalidates any access token associated with them.</para>
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/delete_devices")]
        Task DeleteDevicesAsync([Body] DeleteDevicesRequest data);

        /// <summary>
        /// List registered devices for the current user.
        /// </summary>
        /// <returns>A wrapper object containing the list of devices registered to the user.</returns>
        /// <remarks>Gets information about all devices for the current user.</remarks>
        [Get("_matrix/client/{apiVersion}/devices")]
        Task<DevicesResponse> GetDevicesAsync();

        /// <summary>
        /// Delete a device.
        /// </summary>
        /// <param name="deviceId">The ID of the device to delete.</param>
        /// <param name="authentication">Authentication data.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// <para>
        /// This API endpoint uses the
        /// <a href="https://matrix.org/docs/spec/client_server/r0.4.0.html#user-interactive-authentication-api">
        ///     User-Interactive Authentication API
        /// </a>.
        /// </para>
        /// <para>Deletes the given device, and invalidates any access token associated with it.</para>
        /// </remarks>
        [Delete("_matrix/client/{apiVersion}/devices/{deviceId}")]
        Task DeleteDeviceAsync([Path] string deviceId, [Body] AuthenticationContainer authentication);

        /// <summary>
        /// Get a single device.
        /// </summary>
        /// <param name="deviceId">The ID of the device to get.</param>
        /// <returns>The <see cref="Device" />.</returns>
        /// <remarks>Gets information on a single device, by device ID.</remarks>
        [Get("_matrix/client/{apiVersion}/devices/{deviceId}")]
        Task<Device> GetDeviceAsync([Path] string deviceId);

        /// <summary>
        /// Update a device.
        /// </summary>
        /// <param name="deviceId">The ID of the device to update.</param>
        /// <param name="data">New metadata to set on the device.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>Updates the metadata on the given device.</remarks>
        [Put("_matrix/client/{apiVersion}/devices/{deviceId}")]
        Task SetDeviceMetadataAsync([Path] string deviceId, [Body] DeviceMetadata data);

        #endregion Device management

        #region Application service room directory management

        /// <summary>
        /// Updates a room's visibility in the application service's room directory.
        /// </summary>
        /// <param name="networkId">
        /// The protocol (network) ID to update the room list for. This would have been provided by the application
        /// service as being listed as a supported protocol.
        /// </param>
        /// <param name="roomId">The room ID to add to the directory.</param>
        /// <param name="data">A wrapper object containing the room visibility to set.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// <para>Updates the visibility of a given room on the application service's room directory.</para>
        /// <para>
        /// This API is similar to the room directory visibility API used by clients to update the homeserver's more
        /// general room directory.
        /// </para>
        /// </remarks>
        [Put("_matrix/client/{apiVersion}/directory/list/appservice/{networkId}/{roomId}")]
        Task UpdateAppServiceRoomVisibilityAsync(
            [Path] string networkId,
            [Path] string roomId,
            [Body] RoomVisibilityContainer data);

        #endregion Application service room directory management

        #region Room directory

        /// <summary>
        /// Remove a mapping of room alias to room ID.
        /// </summary>
        /// <param name="alias">The alias to remove.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// <para>
        /// Servers may choose to implement additional access control checks here, for instance that room aliases can
        /// only be deleted by their creator or a server administrator.
        /// </para>
        /// </remarks>
        [Delete("_matrix/client/{apiVersion}/directory/room/{roomAlias}")]
        Task DeleteRoomAliasAsync([Path("roomAlias")] RoomAlias alias);

        /// <summary>
        /// Get the room ID corresponding to this room alias.
        /// </summary>
        /// <param name="alias">The room alias.</param>
        /// <returns>Information about the alias.</returns>
        /// <remarks>
        /// <para>Requests that the server resolve a room alias to a room ID.</para>
        /// <para>
        /// The server will use the federation API to resolve the alias if the domain part of the alias does not
        /// correspond to the server's own domain.
        /// </para>
        /// </remarks>
        [Get("_matrix/client/{apiVersion}/directory/room/{roomAlias}")]
        Task<RoomAliasInformation> ResolveRoomAliasAsync([Path("roomAlias")] RoomAlias alias);

        /// <summary>
        /// Create a new mapping from room alias to room ID.
        /// </summary>
        /// <param name="alias">The room alias to set.</param>
        /// <param name="data">Information about the room alias.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        [Put("_matrix/client/{apiVersion}/directory/room/{roomAlias}")]
        Task MapRoomAliasAsync([Path("roomAlias")] RoomAlias alias, [Body] RoomIdContainer data);

        #endregion Room directory

        #region Room participation

        /// <summary>
        /// Get events and state around a specified event.
        /// </summary>
        /// <param name="roomId">The ID of the room to get events from.</param>
        /// <param name="eventId">The ID of the event to get context around.</param>
        /// <param name="limit">The maximum number of events to return. Defaults to <c>10</c>.</param>
        /// <returns>An object containing the event context.</returns>
        /// <remarks>
        /// This API returns a number of events that happened just before and after the specified event.
        /// This allows clients to get the context surrounding an event.
        /// </remarks>
        [Get("_matrix/client/{apiVersion}/rooms/{roomId}/context/{eventId}")]
        Task<EventContext> GetContextAsync([Path] string roomId, [Path] string eventId, [Query] int limit = 10);

        /// <summary>
        /// Get a single event by event ID.
        /// </summary>
        /// <param name="roomId">The ID of the room the event is in.</param>
        /// <param name="eventId">The ID of the event to get.</param>
        /// <returns>The full event.</returns>
        /// <remarks>
        /// Get a single event based on a <paramref name="roomId" /> and <paramref name="eventId" />.
        /// You must have permission to retrieve this event e.g. by being a member in the room for this event.
        /// </remarks>
        [Get("_matrix/client/{apiVersion}/rooms/{roomId}/event/{eventId}")]
        Task<Event> GetEventAsync([Path] string roomId, [Path] string eventId);

        /// <summary>
        /// Gets the list of currently joined users and their profile data.
        /// </summary>
        /// <param name="roomId">The ID of the room to get the members of.</param>
        /// <returns>
        /// A wrapper containing a dictionary mapping <see cref="UserId" /> to <see cref="Profile" /> objects.
        /// </returns>
        /// <remarks>
        /// This API returns a map of MXIDs to member info objects for members of the room. The current user must
        /// be in the room for it to work, unless it is an application service, in which case any of the AS's users
        /// must be in the room. This API is primarily for application services and should be faster to respond than
        /// <c>/members</c> as it can be implemented more efficiently on the server.
        /// </remarks>
        [Get("_matrix/client/{apiVersion}/rooms/{roomId}/joined_members")]
        Task<JoinedMembersResponse> GetJoinedMembersAsync([Path] string roomId);

        /// <summary>
        /// Get member events for a room.
        /// </summary>
        /// <param name="roomId">The ID of the room to get member events for.</param>
        /// <returns>
        /// Events of type <c>m.room.member</c> for the room. If you are joined to the room then this will be
        /// the current list of events. If you have left the room then it will be the list as it was when you
        /// left the room.
        /// </returns>
        [Get("_matrix/client/{apiVersion}/rooms/{roomId}/members")]
        Task<Chunk<StateEvent>> GetMemberEventsAsync([Path] string roomId);

        /// <summary>
        /// Get room events for a room.
        /// </summary>
        /// <param name="roomId">The ID of the room to get events for.</param>
        /// <param name="from">
        /// The token to start returning events from. This token can be obtained from a <c>prev_batch</c> token
        /// returned for each room by the sync API, or from a <c>start</c> or <c>end</c> token returned by a
        /// previous request to this endpoint.
        /// </param>
        /// <param name="to">
        /// The token to stop returning events at. This token can be obtained from a <c>prev_batch</c> token
        /// returned for each room by the sync endpoint, or from a <c>start</c> or <c>end</c> token returned
        /// by a previous request to this endpoint.
        /// </param>
        /// <param name="direction">The direction to return events from.</param>
        /// <param name="limit">The maximum number of events to return. Defaults to <c>10</c>.</param>
        /// <param name="filter">A <see cref="RoomEventFilter" /> to filter returned events with.</param>
        /// <returns>An object containing room events and a token to request more.</returns>
        /// <remarks>
        /// This API returns a list of message and state events for a room. It uses pagination query parameters
        /// to paginate history in the room.
        /// </remarks>
        [Get("_matrix/client/{apiVersion}/rooms/{roomId}/messages")]
        Task<PaginatedChunk<RoomEvent>> GetRoomEventsAsync(
            [Path] string roomId,
            [Query] string from = null,
            [Query] string to = null,
            [Query("dir", QuerySerializationMethod.Serialized)] Direction direction = Direction.Backwards,
            [Query] int limit = 10,
            [Query] RoomEventFilter filter = default);

        /// <summary>
        /// Send a receipt for the given event ID.
        /// </summary>
        /// <param name="roomId">The ID of the room in which to send the event.</param>
        /// <param name="receiptType">The type of receipt to send.</param>
        /// <param name="eventId">The event ID to acknowledge up to.</param>
        /// <param name="data">
        /// Extra receipt information to attach to the event content, if any.
        /// The server will automatically set the <c>ts</c> field.
        /// </param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>This API updates the marker for the given receipt type to the event ID specified.</remarks>
        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/receipt/{receiptType}/{eventId}")]
        Task SendReceiptAsync(
            [Path] string roomId,
            [Path] string receiptType,
            [Path] string eventId,
            [Body] object data = null);

        /// <summary>
        /// Redacts an event.
        /// </summary>
        /// <param name="roomId">The ID of the room from which to redact the event.</param>
        /// <param name="eventId">The ID of the event to redact.</param>
        /// <param name="transactionId">
        /// The transaction ID for this event. Clients should generate a unique ID; it will be used by the server
        /// to ensure idempotency of requests.
        /// </param>
        /// <param name="data">An object containing reasoning as to why the event was redacted.</param>
        /// <returns>A wrapper containing the ID for the redaction event.</returns>
        /// <remarks>
        /// <para>
        /// Strips all information out of an event which isn't critical to the integrity of the server-side
        /// representation of the room.
        /// </para>
        /// <para><strong>This cannot be undone.</strong></para>
        /// <para>
        /// Users may redact their own events, and any user with a power level greater than or equal to the
        /// <c>redact</c> power level of the room may redact events there.
        /// </para>
        /// </remarks>
        [Put("_matrix/client/{apiVersion}/rooms/{roomId}/redact/{eventId}/{txnId}")]
        Task<EventIdContainer> RedactEventAsync(
            [Path] string roomId,
            [Path] string eventId,
            [Path("txnId")] string transactionId,
            [Body] RedactionContent data);

        /// <summary>
        /// Send an event with content to the given room.
        /// </summary>
        /// <param name="roomId">The ID of the room to send the event to.</param>
        /// <param name="eventType">The type of event to send.</param>
        /// <param name="transactionId">
        /// The transaction ID for this event. Clients should generate an ID unique across requests with the
        /// same access token; it will be used by the server to ensure idempotency of requests.
        /// </param>
        /// <param name="data">Event content to send in the event.</param>
        /// <returns>A wrapper containing the event ID for the created message event.</returns>
        /// <remarks>
        /// <para>
        /// This endpoint is used to send room and message events to a room. These events allow access to
        /// historical events and pagination, making them suited for "once-off" activity in a room.
        /// </para>
        /// <para>
        /// The body of the request should be the content object of the event; the fields in this object will vary
        /// depending on the type of event.
        /// See <a href="https://matrix.org/docs/spec/client_server/r0.4.0.html#room-events">Room Events</a>
        /// for the <c>m.event</c> specification.
        /// </para>
        /// </remarks>
        [Put("_matrix/client/{apiVersion}/rooms/{roomId}/send/{eventType}/{txnId}")]
        Task<EventIdContainer> SendEventAsync(
            [Path] string roomId,
            [Path] string eventType,
            [Path("txnId")] string transactionId,
            [Body] EventContent data);

        /// <summary>
        /// Get all state events in the current state of a room.
        /// </summary>
        /// <param name="roomId">The ID of the room to look up the state for.</param>
        /// <returns>A list of <see cref="StateEvent" /> for the room.</returns>
        [Get("_matrix/client/{apiVersion}/rooms/{roomId}/state")]
        Task<IReadOnlyCollection<StateEvent>> GetStateEventsAsync([Path] string roomId);

        /// <summary>
        /// Get a specific type of state for a room, where the state key is empty.
        /// </summary>
        /// <param name="roomId">The ID of the room to look up the state in.</param>
        /// <param name="eventType">The type of state event to look up.</param>
        /// <returns>The <see cref="StateEvent" />.</returns>
        /// <remarks>
        /// <para>
        /// Looks up the contents of a state event in a room. If the user is joined to the room then the state is
        /// taken from the current state of the room. If the user has left the room then the state is taken from
        /// the state of the room when they left.
        /// </para>
        /// <para>This looks up the state event with the empty state key.</para>
        /// </remarks>
        [Get("_matrix/client/{apiVersion}/rooms/{roomId}/state/{eventType}")]
        Task<StateEvent> GetStateEventAsync([Path] string roomId, [Path] string eventType);

        /// <summary>
        /// Send a state event to the given room.
        /// </summary>
        /// <param name="roomId">The ID of the room to set the state in.</param>
        /// <param name="eventType">The type of event to send.</param>
        /// <param name="data">Content of the event.</param>
        /// <returns>A wrapper containing the ID of the created event.</returns>
        /// <remarks>
        /// <para>
        /// State events can be sent using this endpoint. This endpoint is equivalent to calling
        /// <c>/rooms/{roomId}/state/{eventType}/{stateKey}</c> with an empty <c>stateKey</c>. Previous state events
        /// with matching type and an empty state key will be overwritten.
        /// </para>
        /// <para>
        /// Requests to this endpoint <em>cannot use transaction IDs</em> like other <c>PUT</c> paths because they
        /// cannot be differentiated from the <c>state_key</c>. Furthermore, <c>POST</c> is unsupported on state
        /// paths.
        /// </para>
        /// <para>
        /// The body of the request should be the content object of the event; the fields in this object will vary
        /// depending on the type of event.
        /// See <a href="https://matrix.org/docs/spec/client_server/r0.4.0.html#room-events">Room Events</a>
        /// for the <c>m.event</c> specification.
        /// </para>
        /// </remarks>
        [Put("_matrix/client/{apiVersion}/rooms/{roomId}/state/{eventType}")]
        Task<EventIdContainer> SendStateAsync(
            [Path] string roomId,
            [Path] string eventType,
            [Body] EventContent data);

        /// <summary>
        /// Get the state event identified by a type and state key.
        /// </summary>
        /// <param name="roomId">The ID of the room to look up the state in.</param>
        /// <param name="eventType">The type of state event to look up.</param>
        /// <param name="stateKey">The key of the state to look up.</param>
        /// <returns>The <see cref="StateEvent" />.</returns>
        /// <remarks>
        /// Looks up the contents of a state event in a room. If the user is joined to the room then the state is taken
        /// from the current state of the room. If the user has left the room then the state is taken from the state
        /// of the room when they left.
        /// </remarks>
        [Get("_matrix/client/{apiVersion}/rooms/{roomId}/state/{eventType}/{stateKey}")]
        Task<StateEvent> GetStateEventAsync([Path] string roomId, [Path] string eventType, [Path] string stateKey);

        /// <summary>
        /// Send a state event to the given room.
        /// </summary>
        /// <param name="roomId">The ID of the room to set the state in.</param>
        /// <param name="eventType">The type of state event to send.</param>
        /// <param name="stateKey">The <c>state_key</c> for the state to send.</param>
        /// <param name="data">Content of the event.</param>
        /// <returns>A wrapper containing the ID of the created event.</returns>
        /// <remarks>
        /// <para>
        /// State events can be sent using this endpoint. These events will be overwritten if the
        /// <paramref name="eventType" /> and <paramref name="stateKey" /> both match.
        /// </para>
        /// <para>
        /// Requests to this endpoint <em>cannot use transaction IDs</em> like other <c>PUT</c> paths because they
        /// cannot be differentiated from the <c>state_key</c>. Furthermore, <c>POST</c> is unsupported on state paths.
        /// </para>
        /// <para>
        /// The body of the request should be the content object of the event; the fields in this object will vary
        /// depending on the type of event.
        /// See <a href="https://matrix.org/docs/spec/client_server/r0.4.0.html#room-events">Room Events</a>
        /// for the <c>m.event</c> specification.
        /// </para>
        /// </remarks>
        [Put("_matrix/client/{apiVersion}/rooms/{roomId}/state/{eventType}/{stateKey}")]
        Task<EventIdContainer> SendStateAsync(
            [Path] string roomId,
            [Path] string eventType,
            [Path] string stateKey,
            [Body] EventContent data);

        /// <summary>
        /// Informs the server that the user has started or stopped typing.
        /// </summary>
        /// <param name="roomId">The ID of the room in which the user's typing state has changed.</param>
        /// <param name="userId">The ID of the user whose typing state has changed.</param>
        /// <param name="data">The current typing state.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// This tells the server that the user is typing for the next <c>N</c> milliseconds where <c>N</c> is the
        /// value specified in the <see cref="TypingState.Timeout" /> property. Alternatively, if
        /// <see cref="TypingState.IsTyping" /> is <c>false</c>, it tells the server that the user has stopped typing.
        /// </remarks>
        [Put("_matrix/client/{apiVersion}/rooms/{roomId}/typing/{userId}")]
        Task SendTypingAsync([Path] string roomId, [Path] UserId userId, [Body] TypingState data);

        /// <summary>
        /// Synchronise the client's state and receive new messages.
        /// </summary>
        /// <param name="since">A point in time to continue sync from.</param>
        /// <param name="filter">
        /// The ID of a filter created using the filter API or a filter JSON object encoded as a string. The server
        /// will detect whether it is an ID or a JSON object by whether the first character is a <c>{</c> open brace.
        /// Passing the JSON inline is best suited to one-off requests. Creating a filter using the filter API is
        /// recommended for clients that reuse the same filter multiple times, for example in long poll requests.
        /// </param>
        /// <param name="fullState">
        /// <para>Controls whether to include the full state for all rooms the user is a member of.</para>
        /// <para>
        /// If this is set to <c>true</c>, then all state events will be returned, even if
        /// <paramref name="since" /> is non-empty.
        /// The timeline will still be limited by the <paramref name="since" /> parameter. In this case, the
        /// <paramref name="timeout" /> parameter will be ignored and the query will return immediately, possibly
        /// with an empty timeline.
        /// </para>
        /// <para>
        /// If <c>false</c>, and <paramref name="since" /> is non-empty, only state which has changed since the point
        /// indicated by <paramref name="since" /> will be returned.
        /// </para>
        /// </param>
        /// <param name="presence">
        /// Controls whether the client is automatically marked as online by polling this API. If this parameter is
        /// omitted then the client is automatically marked as online when it uses this API. Otherwise if the
        /// parameter is set to <see cref="Presence.Offline" /> then the client is not marked as being online when it
        /// uses this API. When set to <see cref="Presence.Unavailable" />, the client is marked as being idle.
        /// </param>
        /// <param name="timeout">
        /// <para>
        /// The maximum time to wait before returning this request. If no events (or other data) become available
        /// before this time elapses, the server will return a response with empty fields.
        /// </para>
        /// <para>
        /// By default, this is <see cref="TimeSpan.Zero" />, so the server will
        /// return immediately even if the response is empty.
        /// </para>
        /// </param>
        /// <param name="cancellationToken">Cancellation token to pass to the underlying HTTP requester.</param>
        /// <returns>The initial snapshot or delta for the client to use to update their state.</returns>
        /// <remarks>
        /// Synchronise the client's state with the latest state on the server. Clients use this API when they first
        /// log in to get an initial snapshot of the state on the server, and then continue to call this API to get
        /// incremental deltas to the state, and to receive new events.
        /// </remarks>
        [Get("_matrix/client/{apiVersion}/sync")]
        Task<SyncResponse> SyncAsync(
            [Query] string since = null,
            [Query] string filter = null,
            [Query("full_state", QuerySerializationMethod.Serialized)] bool fullState = false,
            [Query("set_presence", QuerySerializationMethod.Serialized)] Presence presence = Presence.Online,
            [Query(QuerySerializationMethod.Serialized)] TimeSpan timeout = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Upload a new filter.
        /// </summary>
        /// <param name="userId">
        /// The ID of the user uploading the filter. The access token must be authorized to make requests for this
        /// user ID.
        /// </param>
        /// <param name="data">The filter to upload.</param>
        /// <returns>A wrapper containing the ID of the created filter.</returns>
        /// <remarks>
        /// Uploads a new filter definition to the homeserver. Returns a filter ID that may be used in future requests
        /// to restrict which events are returned to the client.
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/user/{userId}/filter")]
        Task<FilterIdContainer> UploadFilterAsync([Path] UserId userId, [Body] Filter data);

        /// <summary>
        /// Download a filter.
        /// </summary>
        /// <param name="userId">The ID of the user to download a filter for.</param>
        /// <param name="filterId">The ID of the filter to download.</param>
        /// <returns>The filter definition.</returns>
        [Get("_matrix/client/{apiVersion}/user/{userId}/filter/{filterId}")]
        Task<Filter> GetFilterAsync([Path] UserId userId, [Path] string filterId);

        #endregion Room participation

        #region Room membership

        // TODO: The docs say that the third party data object should be additionally wrapped in
        // another level `signed`. For now assume that is a doc error because it would be a
        // preposterous amount of nesting. Awaiting response from #matrix-dev:matrix.org
        // on whether this is actually intended.
        /// <summary>
        /// Start the requesting user participating in a particular room.
        /// </summary>
        /// <param name="roomIdOrAlias">The ID or alias of the room to join.</param>
        /// <param name="serverNames">
        /// The servers to attempt to join the room through. One of the servers must be participating in the room.
        /// </param>
        /// <param name="data">
        /// A signature of an <c>m.third_party_invite</c> token to prove that this user owns a third party identity
        /// which has been invited to the room.
        /// </param>
        /// <returns>A wrapper object containing the ID of the room that was joined.</returns>
        /// <remarks>
        /// <para>Note that this API takes either a room ID or alias, unlike <see cref="JoinRoomAsync" />.</para>
        /// <para>
        /// This API starts a user participating in a particular room, if that user is allowed to participate in that
        /// room. After this call, the client is allowed to see all current state events in the room, and all
        /// subsequent events associated with the room until the user leaves the room.
        /// </para>
        /// <para>
        /// After a user has joined a room, the room will appear as an entry in the response of the call to
        /// <see cref="SyncAsync" />.
        /// </para>
        /// <para>
        /// If <paramref name="data" /> was supplied, the homeserver must verify that it matches a pending
        /// <c>m.room.third_party_invite</c> event in the room, and perform key validity checking if required
        /// by the event.
        /// </para>
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/join/{roomIdOrAlias}")]
        Task<RoomIdContainer> JoinRoomOrAliasAsync(
            [Path] string roomIdOrAlias,
            [Query("server_name")] IEnumerable<string> serverNames = null,
            [Body] SignedThirdPartyData? data = null);

        /// <summary>
        /// List the user's current rooms.
        /// </summary>
        /// <returns>A wrapper object containing a list of room IDs the user is joined to.</returns>
        [Get("_matrix/client/{apiVersion}/joined_rooms")]
        Task<JoinedRooms> GetJoinedRoomsAsync();

        /// <summary>
        /// Ban a user in a room.
        /// </summary>
        /// <param name="roomId">The ID of the room from which the user should be banned.</param>
        /// <param name="data">An object that contains the user ID that should be banned, along with a reason.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// <para>Ban a user in the room. If the user is currently in the room, also kick them.</para>
        /// <para>
        /// When a user is banned from a room, they may not join it or be invited to it until they are unbanned.
        /// </para>
        /// <para>The caller must have the required power level in order to perform this operation.</para>
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/ban")]
        Task BanAsync([Path] string roomId, [Body] Reason data);

        /// <summary>
        /// Stop the requesting user remembering about a particular room.
        /// </summary>
        /// <param name="roomId">The ID of the room to forget.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// <para>This API stops a user remembering about a particular room.</para>
        /// <para>
        /// In general, history is a first class citizen in Matrix. After this APi is called, however, a user will no
        /// longer be able to retrieve history for this room. If all users on a homeserver forget a room, the room is
        /// eligible for deletion from that homeserver.
        /// </para>
        /// <para>If the user is currently joined to the room, they must leave the room before calling this API.</para>
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/forget")]
        Task ForgetAsync([Path] string roomId);

        /// <summary>
        /// Invite a user to participate in a particular room.
        /// </summary>
        /// <param name="roomId">The ID of the room to which to invite the user.</param>
        /// <param name="data">An object containing information about the 3PID to use for sending the invite.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// <para>
        /// <em>Note that there are two forms of this API, which are documented separately. This version of the API
        /// does not require that the inviter know the Matrix identifier of the invitee, and instead relies on
        /// third party identifiers. The homeserver uses an identity server to perform the mapping from third party
        /// identifier to a Matrix identifier. The other is documented here:
        /// <see cref="InviteAsync(string, UserIdContainer)" />.</em>
        /// </para>
        /// <para>
        /// This API invites a user to participate in a particular room. They do not start participating in the room
        /// until they actually join the room.
        /// </para>
        /// <para>Only users currently in a particular room can invite other users to join that room.</para>
        /// <para>
        /// If the identity server did know the Matrix user identifier for the third party identifier, the homeserver
        /// will append a <c>m.room.member</c> event to the room.
        /// </para>
        /// <para>
        /// If the identity server does not know a Matrix user identifier for the passed third party identifier, the
        /// homeserver will issue an invitation which can be accepted upon providing proof of ownership of the
        /// third party identifier. This is achieved by the identity server generating a token, which it gives to
        /// the inviting homeserver. The homeserver will add an <c>m.room.third_party_invite</c> event into the graph
        /// for the room, containing that token.
        /// </para>
        /// <para>
        /// When the invitee binds the invited third party identifier to a Matrix user ID, the identity server will
        /// give the user a list of pending invitations, each containing:
        /// <list type="bullet">
        /// <item><description>The room ID to which they were invited.</description></item>
        /// <item><description>The token given to the homeserver.</description></item>
        /// <item><description>
        /// A signature of the token, signed with the identity server's private key.
        /// </description></item>
        /// <item><description>The Matrix user ID who invited them to the room.</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// If a token is requested from the identity server, the homeserver will append a
        /// <c>m.room.third_party_invite</c> event to the room.
        /// </para>
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/invite")]
        Task InviteAsync([Path] string roomId, [Body] ThirdPartyRoomInvite data);

        /// <summary>
        /// Invite a user to participate in a particular room.
        /// </summary>
        /// <param name="roomId">The ID of the room to which to invite the user.</param>
        /// <param name="data">An object containing the user ID of the invitee.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// <para>
        /// <em>Note that there are two forms of this API, which are documented separately. This version of the API
        /// requires that the inviter knows the Matrix identifier of the invitee. The other is documented here:
        /// <see cref="InviteAsync(string, ThirdPartyRoomInvite)" />.</em>
        /// </para>
        /// <para>
        /// This API invites a user to participate in a particular room. They do not start participating in the room
        /// until they actually join the room.
        /// </para>
        /// <para>Only users currently in a particular room can invite other users to join that room.</para>
        /// <para>
        /// If the user was invited to the room, the homeserver will append a <c>m.room.member</c> event to the room.
        /// </para>
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/invite")]
        Task InviteAsync([Path] string roomId, [Body] UserIdContainer data);

        /// <summary>
        /// Start the requesting user participating in a particular room.
        /// </summary>
        /// <param name="roomId">The ID of the room to join.</param>
        /// <param name="data">Signed third party data, if joining from a 3PID invite.</param>
        /// <returns>A wrapper object containing the ID of the joined room.</returns>
        /// <remarks>
        /// <para>
        /// Note that this API requires a room ID, not alias. <see cref="JoinRoomOrAliasAsync" /> exists if you have a
        /// room alias.
        /// </para>
        /// <para>
        /// This API starts a user participating in a particular room, if that user is allowed to participate in that
        /// room. After this call, the client is allowed to see all current state events in the room, and all
        /// subsequent events associated with the room until the user leaves the room.
        /// </para>
        /// <para>
        /// After a user has joined a room, the room will appear as an entry in the response of the call to
        /// <see cref="SyncAsync" />.
        /// </para>
        /// <para>
        /// If <paramref name="data" /> was supplied, the homeserver must verify that it matches a pending
        /// <c>m.room.third_party_invite</c> event in the room, and perform key validity checking if required
        /// by the event.
        /// </para>
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/join")]
        Task<RoomIdContainer> JoinRoomAsync([Path] string roomId, [Body] SignedThirdPartyData? data = null);

        /// <summary>
        /// Kick a user from a room.
        /// </summary>
        /// <param name="roomId">The ID of the room from which the user should be kicked.</param>
        /// <param name="data">An object that contains the user ID that should be kicked, along with a reason.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// <para>The caller must have the required power level in order to perform this operation.</para>
        /// <para>
        /// Kicking a user adjusts the target member's membership state to be <see cref="Membership.Left" /> with
        /// an optional <c>reason</c>. Like with other membership changes, a user can directly adjust the target
        /// member's state by using <see cref="SendStateAsync(string, string, string, EventContent)" />.
        /// </para>
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/kick")]
        Task KickAsync([Path] string roomId, [Body] Reason data);

        /// <summary>
        /// Stop the requesting user participating in a particular room.
        /// </summary>
        /// <param name="roomId">The ID of the room to leave.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// <para>This API stops a user participating in a particular room.</para>
        /// <para>
        /// If the user was already in the room, the will no longer be able to see new events in the room. If the room
        /// requires an invite to join, they will need to be re-invited before they can re-join.
        /// </para>
        /// <para>If the user was invited to the room, but had not joined, this call serves to reject the invite.</para>
        /// <para>
        /// The user will still be allowed to retrieve history from the room which they were previously allowed to see.
        /// </para>
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/leave")]
        Task LeaveAsync([Path] string roomId);

        /// <summary>
        /// Unban a user from the room.
        /// </summary>
        /// <param name="roomId">The ID of the room from which the user should be unbanned.</param>
        /// <param name="data">An object containing the ID of the user to unban.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// <para>
        /// Unban a user from the room. This allows them to be invited to the room, and join if they would otherwise
        /// be allowed to join according to its join rules.
        /// </para>
        /// <para>The caller must have the required power level in order to perform this operation.</para>
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/rooms/{roomId}/unban")]
        Task UnbanAsync([Path] string roomId, [Body] UserIdContainer data);

        #endregion Room membership

        #region End-to-end encryption

        /// <summary>
        /// Query users with recent device key updates.
        /// </summary>
        /// <param name="from">
        /// The desired start point of the list. Should be the <c>next_batch</c> field from a response to an earlier
        /// call to <see cref="SyncAsync" />. Users who have not uploaded new device identity keys since this point,
        /// nor deleted existing devices with identity keys since then, will be excluded from the results.
        /// </param>
        /// <param name="to">
        /// The desired end point of the list. Should be the <c>next_batch</c> field from a recent call to
        /// <see cref="SyncAsync" /> - typically the most recent such call. This may be used by the server as a hint
        /// to check its caches are up to date.
        /// </param>
        /// <returns>Lists of users who have had device changes.</returns>
        /// <remarks>
        /// <para>Gets lists of users who have updated their device identity keys since a previous sync token.</para>
        /// <para>
        /// The server should include in the results any users who:
        /// <list type="bullet">
        /// <item><description>
        /// Currently share a room with the calling user
        /// (i.e. both users have membership state <see cref="Membership.Joined" />); and
        /// </description></item>
        /// <item><description>
        /// Added new device identity keys or removed an existing device with identity keys,
        /// between <paramref name="from" /> and <paramref name="to" />.
        /// </description></item>
        /// </list>
        /// </para>
        /// </remarks>
        [Get("_matrix/client/{apiVersion}/keys/changes")]
        Task<DeviceChangeLists> GetDeviceKeyChangesAsync([Query] string from, [Query] string to);

        /// <summary>
        /// Claim one-time encryption keys (OTKs).
        /// </summary>
        /// <param name="data">Data specifying keys to request.</param>
        /// <returns>The claimed keys.</returns>
        /// <remarks>
        /// Claims one-time keys (OTKs) for use in pre-key messages.
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/keys/claim")]
        Task<ClaimKeysResponse> ClaimOneTimeKeysAsync([Body] ClaimKeysRequest data);

        /// <summary>
        /// Download device identity keys.
        /// </summary>
        /// <param name="data">Data specifying what keys to request.</param>
        /// <returns>The requested devices and associated identity keys.</returns>
        /// <remarks>
        /// Returns the current devices and identity keys for the given users.
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/keys/query")]
        Task<DeviceKeysQueryResponse> QueryDeviceKeysAsync([Body] DeviceKeysQuery data);

        /// <summary>
        /// Upload end-to-end encryption (E2EE) keys.
        /// </summary>
        /// <param name="data">The keys to be published.</param>
        /// <returns>
        /// For each key algorithm, the dictionary in the returned object keys it to the number of unclaimed
        /// one-time keys that type currently held on the server for the current device.
        /// </returns>
        /// <remarks>
        /// Publishes end-to-end encryption (E2EE) keys for the current device.
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/keys/upload")]
        Task<OneTimeKeyCounts> UploadDeviceKeysAsync([Body] UploadKeysRequest data);

        #endregion End-to-end encryption

        #region Session management

        /// <summary>
        /// Get the supported login types to authenticate users.
        /// </summary>
        /// <returns>A wrapper object containing the login types the homeserver supports.</returns>
        /// <remarks>
        /// Gets the homeserver's supported login types to authenticate users. Clients should pick one of these and
        /// supply it as the <c>type</c> when logging in.
        /// </remarks>
        [Get("_matrix/client/{apiVersion}/login")]
        Task<LoginFlows> GetLoginFlowsAsync();

        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="data">Authentication data.</param>
        /// <returns>The authenticated user's access token, user ID, and device ID.</returns>
        /// <remarks>
        /// <para>
        /// Authenticates the user, and issues an access token they can use to authorize themselves in subsequent
        /// requests.
        /// </para>
        /// <para>If the client does not supply a <c>device_id</c>, the server must auto-generate one.</para>
        /// <para>
        /// The returned access token must be associated with the <c>device_id</c> supplied by the client or
        /// generated by the server. The server may invalidate any access token previously associated with that
        /// device.
        /// </para>
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/login")]
        Task<AuthenticationResponse> LoginAsync([Body] LoginRequest data);

        /// <summary>
        /// Invalidates a user access token.
        /// </summary>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// Invalidates an existing access token, so that it can no longer be used for authorization.
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/logout")]
        Task LogoutAsync();

        /// <summary>
        /// Invalidates all access tokens for a user.
        /// </summary>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// <para>
        /// Invalidates all access tokens for a user, so that they can no longer be used for authorization.
        /// This includes the access token that made this request.
        /// </para>
        /// <para>
        /// This endpoint does not require UI authorization because UI authorization is designed to protect against
        /// attacks where someone gets hold of a single access token and then takes over the account.
        /// This endpoint invalidates all access tokes for the user, including the token used in the request,
        /// and therefore the attacker is unable to take over the account in this way.
        /// </para>
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/logout/all")]
        Task LogoutAllAsync();

        #endregion Session management

        #region Push notifications

        /// <summary>
        /// Gets a list of events that the user has been notified about.
        /// </summary>
        /// <param name="from">Pagination token given to retrieve the next set of events.</param>
        /// <param name="limit">Limit on the number of events to return in this request.</param>
        /// <param name="only">
        /// Allows basic filtering of events returned. Supply <c>highlight</c> to return only events where
        /// the notification had the highlight tweak set.
        /// </param>
        /// <returns>A batch of notification events.</returns>
        /// <remarks>
        /// This API is used to paginate through the list of events that the user has been, or would have been
        /// notified about.
        /// </remarks>
        [Get("_matrix/client/{apiVersion}/notifications")]
        Task<NotificationsResponse> GetNotificationsAsync(
            [Query] string from = null,
            [Query] int? limit = null,
            [Query] string only = null);

        /// <summary>
        /// Gets the current pushers for the authenticated user.
        /// </summary>
        /// <returns>The currently active notification pushers.</returns>
        /// <remarks>Gets all currently active pushers for the authenticated user.</remarks>
        [Get("_matrix/client/{apiVersion}/pushers")]
        Task<NotificationPushersContainer> GetNotificationPushersAsync();

        /// <summary>
        /// Modify a pusher for the current user on the homeserver.
        /// </summary>
        /// <param name="data">The pusher information.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        [Post("_matrix/client/{apiVersion}/pushers/set")]
        Task ModifyNotificationPusherAsync([Body] NotificationPusher data);

        /// <summary>
        /// Retrieve all push rules, optionally limited to a certain scope.
        /// </summary>
        /// <param name="scope">Optional scope to return a subset of rules under the specified scope.</param>
        /// <returns>All the push rulesets for the current user, matching the scope if one was given.</returns>
        [Get("_matrix/client/{apiVersion}/pushrules/{scope}")]
        Task<NotificationRulesets> GetNotificationPushRulesAsync([Path] string scope = null);

        /// <summary>
        /// Retrieve a push rule.
        /// </summary>
        /// <param name="scope">The scope to retrieve from.</param>
        /// <param name="kind">The kind of rule to retrieve.</param>
        /// <param name="ruleId">The ID of the rule to retrieve.</param>
        /// <returns>
        /// The requested push rule. This will also include data specific to the rule itself such as the rules
        /// actions and conditions, if set.
        /// </returns>
        /// <remarks>Retrieve a single specified push rule.</remarks>
        [Get("_matrix/client/{apiVersion}/pushrules/{scope}/{kind}/{ruleId}")]
        Task<NotificationPushRule> GetNotificationPushRuleAsync(
            [Path] string scope,
            [Path(PathSerializationMethod.Serialized)] NotificationPushRuleKind kind,
            [Path] string ruleId);

        /// <summary>
        /// Delete a push rule.
        /// </summary>
        /// <param name="scope">The scope to delete from.</param>
        /// <param name="kind">The kind of rule to delete.</param>
        /// <param name="ruleId">The ID of the rule to delete.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>This endpoint removes the push rule defined in the path.</remarks>
        [Delete("_matrix/client/{apiVersion}/pushrules/{scope}/{kind}/{ruleId}")]
        Task DeleteNotificationPushRuleAsync(
            [Path] string scope,
            [Path(PathSerializationMethod.Serialized)] NotificationPushRuleKind kind,
            [Path] string ruleId);

        /// <summary>
        /// Add or change a push rule.
        /// </summary>
        /// <param name="scope">The scope to add the rule in.</param>
        /// <param name="kind">The kind of rule to add.</param>
        /// <param name="ruleId">The ID of the rule to add.</param>
        /// <param name="data">Push rule data.</param>
        /// <param name="before">
        /// Provide the ID of a rule to make the new rule the next-most important rule with respect to the given
        /// user-defined rule. It is not possible to add a rule relative to a predefined server rule.
        /// </param>
        /// <param name="after">
        /// Provide the ID of a rule to make the new rule the next-least important rule relative to the given
        /// user-defined rule. It is not possible to add a rule relative to a predefined server rule.
        /// </param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// <para>
        /// This endpoint allows the creation, modification, and deletion of pushers for the current user ID.
        /// The behaviour of this endpoint varies depending on the provided values in <paramref name="data" />.
        /// </para>
        /// <para>When creating push rules, they <em>must</em> be enabled by default.</para>
        /// </remarks>
        [Put("_matrix/client/{apiVersion}/pushrules/{scope}/{kind}/{ruleId}")]
        Task SetNotificationPushRuleAsync(
            [Path] string scope,
            [Path(PathSerializationMethod.Serialized)] NotificationPushRuleKind kind,
            [Path] string ruleId,
            [Body] NotificationPushRule data,
            [Query] string before = null,
            [Query] string after = null);

        /// <summary>
        /// Set the actions for a push rule.
        /// </summary>
        /// <param name="scope">The scope of the rule.</param>
        /// <param name="kind">The kind of the rule.</param>
        /// <param name="ruleId">The ID of the rule on which to set the actions.</param>
        /// <param name="data">The actions to set.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// This endpoint allows clients to change the actions of a push rule. This can be used to change the
        /// actions of built-in rules.
        /// </remarks>
        [Put("_matrix/client/{apiVersion}/pushrules/{scope}/{kind}/{ruleId}/actions")]
        Task SetNotificationPushRuleActionsAsync(
            [Path] string scope,
            [Path(PathSerializationMethod.Serialized)] NotificationPushRuleKind kind,
            [Path] string ruleId,
            [Body] NotificationActionsContainer data);

        /// <summary>
        /// Enable or disable a push rule.
        /// </summary>
        /// <param name="scope">The scope of the rule.</param>
        /// <param name="kind">The kind of rule.</param>
        /// <param name="ruleId">The ID of the rule.</param>
        /// <param name="data">An object containing a boolean specifying whether to enable or disable the rule.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>This endpoint allows clients to enable or disable the specified push rule.</remarks>
        [Put("_matrix/client/{apiVersion}/pushrules/{scope}/{kind}/{ruleId}/enabled")]
        Task SetNotificationPushRuleEnabledAsync(
            [Path] string scope,
            [Path(PathSerializationMethod.Serialized)] NotificationPushRuleKind kind,
            [Path] string ruleId,
            [Body] EnabledContainer data);

        #endregion Push notifications

        #region Presence

        /// <summary>
        /// Get presence events for a user's presence list.
        /// </summary>
        /// <param name="userId">The ID of the user whose presence list should be retrieved.</param>
        /// <returns>A list of presence events.</returns>
        /// <remarks>Retrieves a presence event for each user on the presence list.</remarks>
        [Get("_matrix/client/{apiVersion}/presence/list/{userId}")]
        Task<IReadOnlyCollection<Event>> GetPresenceEventsAsync([Path] UserId userId);

        /// <summary>
        /// Add or remove user's from the current user's presence list.
        /// </summary>
        /// <param name="userId">The ID of the user whose presence list to modify.</param>
        /// <param name="data">Object describing the modifications to perform.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        [Post("_matrix/client/{apiVersion}/presence/list/{userId}")]
        Task UpdatePresenceListAsync([Path] UserId userId, [Body] PresenceListUpdate data);

        /// <summary>
        /// Get a user's presence state.
        /// </summary>
        /// <param name="userId">The ID of the user whose presence state to get.</param>
        /// <returns>The presence state for the user.</returns>
        [Get("_matrix/client/{apiVersion}/presence/{userId}/status")]
        Task<PresenceStatusResponse> GetPresenceAsync([Path] UserId userId);

        /// <summary>
        /// Update a user's presence state.
        /// </summary>
        /// <param name="userId">The ID of the user whose presence state to update.</param>
        /// <param name="data">New presence data to set.</param>
        /// <returns>A <see cref="Task" /> representing request progress.</returns>
        /// <remarks>
        /// This API sets the given user's presence state. When setting the status, the activity time is updated to
        /// reflect that activity; the client does not need to specify the <c>last_active_ago</c> field.
        /// You cannot set the presence state of another user.
        /// </remarks>
        [Put("_matrix/client/{apiVersion}/presence/{userId}/status")]
        Task SetPresenceAsync([Path] UserId userId, [Body] PresenceStatusRequest data);

        #endregion Presence

        #region Room discovery

        /// <summary>
        /// Lists the public rooms on the server.
        /// </summary>
        /// <param name="limit">Limit the number of results returned.</param>
        /// <param name="since">
        /// A pagination token from a previous request, allowing clients to get the next (or previous) batch of rooms.
        /// The direction of pagination is specified solely by which token is supplied, rather than via
        /// an explicit flag.
        /// </param>
        /// <param name="server">The server to fetch the public room lists from. Defaults to the local server.</param>
        /// <returns>A list of the rooms on the server.</returns>
        /// <remarks>
        /// This API returns paginated responses. The rooms are ordered by the number of joined members,
        /// with the largest room first.
        /// </remarks>
        [Get("_matrix/client/{apiVersion}/publicRooms")]
        Task<PublicRoomsChunk> GetPublicRoomsAsync(
            [Query] int? limit = null,
            [Query] string since = null,
            [Query] string server = null);

        /// <summary>
        /// Lists the public rooms on a server with optional filter.
        /// </summary>
        /// <param name="data">The filter to apply.</param>
        /// <param name="server">The server to fetch the public room lists from. Defaults to the local server.</param>
        /// <returns>A list of filtered rooms on the server.</returns>
        /// <remarks>
        /// This API returns paginated responses. The rooms are ordered by the number of joined members,
        /// with the largest rooms first.
        /// </remarks>
        [Post("_matrix/client/{apiVersion}/publicRooms")]
        Task<PublicRoomsChunk> GetPublicRoomsAsync([Body] PublicRoomsRequest data, [Query] string server = null);

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
