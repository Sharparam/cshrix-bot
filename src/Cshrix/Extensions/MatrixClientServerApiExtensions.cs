// <copyright file="MatrixApiExtensions.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Extensions
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Mime;
    using System.Threading.Tasks;

    using Data;

    using JetBrains.Annotations;

    /// <summary>
    /// Contains extension methods for the <see cref="IMatrixClientServerApi" /> interface.
    /// </summary>
    [PublicAPI]
    public static class MatrixClientServerApiExtensions
    {
        /// <summary>
        /// The default content type to assign to downloaded content, if none was returned from the API.
        /// </summary>
        private const string DefaultContentType = "application/octet-stream";

        /// <summary>
        /// Sets a bearer token to use for authenticating with the API.
        /// </summary>
        /// <param name="api">An instance of <see cref="IMatrixClientServerApi" />.</param>
        /// <param name="accessToken">The access token to set.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="api" /> is <c>null</c>.</exception>
        public static void SetBearerToken([NotNull] this IMatrixClientServerApi api, string accessToken)
        {
            if (api == null)
            {
                throw new ArgumentNullException(nameof(api));
            }

            api.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        /// <summary>
        /// Get an OpenID token object to verify the requester's identity.
        /// </summary>
        /// <param name="api">An instance of <see cref="IMatrixClientServerApi" /></param>
        /// <param name="userId">
        /// The ID of the user to request an OpenID token for. Should be the user who is authenticated for the request.
        /// </param>
        /// <returns>The OpenID token for the user.</returns>
        /// <remarks>
        /// <para>
        /// Gets an OpenID token object that the requester may supply to another service to verify their identity in
        /// Matrix. The generated token is only valid for exchanging for user information from the federation API
        /// for OpenID.
        /// </para>
        /// <para>
        /// The access token generated is only valid for the OpenID API. It cannot be used to request another OpenID
        /// access token or call <c>/sync</c>, for example.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="api" /> is <c>null</c>.</exception>
        public static Task<OpenIdToken> RequestOpenIdTokenAsync([NotNull] this IMatrixClientServerApi api, UserId userId)
        {
            if (api == null)
            {
                throw new ArgumentNullException(nameof(api));
            }

            return api.RequestOpenIdTokenAsync(userId, new object());
        }

        /// <summary>
        /// Download content from the content repository.
        /// </summary>
        /// <param name="api">An instance of <see cref="IMatrixClientServerApi" />.</param>
        /// <param name="mediaUri">The MXC URI to download.</param>
        /// <param name="filename">The filename to give in the <c>Content-Disposition</c> header.</param>
        /// <param name="allowRemote">
        /// A value indicating whether the server should attempt to fetch the media if it is deemed remote.
        /// This is to prevent routing loops where the server contacts itself. Defaults to <c>true</c> if not provided.
        /// </param>
        /// <returns>The downloaded content.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="api" /> is <c>null</c>.</exception>
        public static Task<Content> DownloadContentAsync(
            [NotNull] this IMatrixClientServerApi api,
            Uri mediaUri,
            [CanBeNull] string filename = null,
            bool? allowRemote = null)
        {
            if (api == null)
            {
                throw new ArgumentNullException(nameof(api));
            }

            var serverName = mediaUri.Authority;

            // Strip the leading slash (/)
            var mediaId = mediaUri.AbsolutePath.Substring(1);

            return api.DownloadContentAsync(serverName, mediaId, filename, allowRemote);
        }

        /// <summary>
        /// Download content from the content repository.
        /// </summary>
        /// <param name="api">An instance of <see cref="IMatrixClientServerApi" />.</param>
        /// <param name="serverName">The server name from the <c>mxc://</c> URI (the authority component).</param>
        /// <param name="mediaId">The media ID from the <c>mxc://</c> URI (the path component).</param>
        /// <param name="filename">The filename to give in the <c>Content-Disposition</c> header.</param>
        /// <param name="allowRemote">
        /// A value indicating whether the server should attempt to fetch the media if it is deemed remote.
        /// This is to prevent routing loops where the server contacts itself. Defaults to <c>true</c> if not provided.
        /// </param>
        /// <returns>The downloaded content.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="api" /> is <c>null</c>.</exception>
        public static async Task<Content> DownloadContentAsync(
            [NotNull] this IMatrixClientServerApi api,
            string serverName,
            string mediaId,
            [CanBeNull] string filename = null,
            bool? allowRemote = null)
        {
            if (api == null)
            {
                throw new ArgumentNullException(nameof(api));
            }

            using (var response = await DownloadAsync(api, serverName, mediaId, filename, allowRemote))
            {
                return await CreateContentFromResponse(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Download a thumbnail of the content from the content repository.
        /// </summary>
        /// <param name="api">An instance of <see cref="IMatrixClientServerApi" />.</param>
        /// <param name="mediaUri">The MXC URI to download a thumbnail for.</param>
        /// <param name="width">
        /// The desired width of the thumbnail, in pixels. The actual thumbnail may not match the size specified.
        /// </param>
        /// <param name="height">
        /// The desired height of the thumbnail, in pixels. The actual thumbnail may not match the size specified.
        /// </param>
        /// <param name="resizeMethod">The desired resizing method.</param>
        /// <param name="allowRemote">
        /// A value indicating whether the server should attempt to fetch the media if it is deemed remote.
        /// This is to prevent routing loops where the server contacts itself. Defaults to <c>true</c> if not provided.
        /// </param>
        /// <returns>The downloaded thumbnail.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="api" /> is <c>null</c>.</exception>
        public static Task<Content> DownloadThumbnailContentAsync(
            [NotNull] this IMatrixClientServerApi api,
            Uri mediaUri,
            int width,
            int height,
            ResizeMethod resizeMethod = ResizeMethod.Scale,
            bool? allowRemote = null)
        {
            if (api == null)
            {
                throw new ArgumentNullException(nameof(api));
            }

            var serverName = mediaUri.Authority;

            // Strip the leading slash (/)
            var mediaId = mediaUri.AbsolutePath.Substring(1);

            return api.DownloadThumbnailContentAsync(serverName, mediaId, width, height, resizeMethod, allowRemote);
        }

        /// <summary>
        /// Download a thumbnail of the content from the content repository.
        /// </summary>
        /// <param name="api">An instance of <see cref="IMatrixClientServerApi" />.</param>
        /// <param name="serverName">The server name from the <c>mxc://</c> URI (the authority component).</param>
        /// <param name="mediaId">The media ID from the <c>mxc://</c> URI (the path component).</param>
        /// <param name="width">
        /// The desired width of the thumbnail, in pixels. The actual thumbnail may not match the size specified.
        /// </param>
        /// <param name="height">
        /// The desired height of the thumbnail, in pixels. The actual thumbnail may not match the size specified.
        /// </param>
        /// <param name="resizeMethod">The desired resizing method.</param>
        /// <param name="allowRemote">
        /// A value indicating whether the server should attempt to fetch the media if it is deemed remote.
        /// This is to prevent routing loops where the server contacts itself. Defaults to <c>true</c> if not provided.
        /// </param>
        /// <returns>The downloaded thumbnail.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="api" /> is <c>null</c>.</exception>
        public static async Task<Content> DownloadThumbnailContentAsync(
            [NotNull] this IMatrixClientServerApi api,
            string serverName,
            string mediaId,
            int width,
            int height,
            ResizeMethod resizeMethod = ResizeMethod.Scale,
            bool? allowRemote = null)
        {
            if (api == null)
            {
                throw new ArgumentNullException(nameof(api));
            }

            using (var response = await api.DownloadThumbnailAsync(
                serverName,
                mediaId,
                width,
                height,
                resizeMethod,
                allowRemote))
            {
                return await CreateContentFromResponse(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Helper method to download a file from the Matrix API, choosing the relevant API method based on
        /// <paramref name="filename" /> being <c>null</c> or not.
        /// </summary>
        /// <param name="api">An instance of <see cref="IMatrixClientServerApi" />.</param>
        /// <param name="serverName">The domain part of a content URL.</param>
        /// <param name="mediaId">The ID of the media as extracted from a content URL.</param>
        /// <param name="filename">Name to assign to the file, can be left <c>null</c> if none is needed.</param>
        /// <param name="allowRemote">Allow the homeserver to fetch remote media if necessary.</param>
        /// <returns>
        /// An instance of <see cref="HttpResponseMessage" /> ready to be parsed.
        /// The returned object <em>must be disposed</em>.
        /// </returns>
        private static Task<HttpResponseMessage> DownloadAsync(
            IMatrixClientServerApi api,
            string serverName,
            string mediaId,
            [CanBeNull] string filename,
            bool? allowRemote)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (filename == null)
            {
                return api.DownloadAsync(serverName, mediaId, allowRemote);
            }

            return api.DownloadAsync(serverName, mediaId, filename, allowRemote);
        }

        /// <summary>
        /// Helper method to generate a <see cref="Content" /> instance from an <see cref="HttpResponseMessage" />
        /// object. Automatically parses filename, content type, and data.
        /// </summary>
        /// <param name="response">The response to parse.</param>
        /// <returns>Parsed content data.</returns>
        private static async Task<Content> CreateContentFromResponse(HttpResponseMessage response)
        {
            var hasContentDisposition =
                response.Headers.TryGetFirstContentDisposition(out var contentDispositionString);

            var hasContentType = response.Headers.TryGetFirstContentType(out var contentTypeString);

            var filename = hasContentDisposition ? ParseFilename(contentDispositionString) : null;
            var contentType = ParseContentType(hasContentType ? contentTypeString : DefaultContentType);
            var bytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            return new Content(filename, contentType, bytes);
        }

        /// <summary>
        /// Helper method to extract a filename from a <c>Content-Disposition</c> header value.
        /// </summary>
        /// <param name="contentDispositionString">Header value to parse.</param>
        /// <returns>Extracted filename if found; otherwise, <c>null</c>.</returns>
        [CanBeNull]
        private static string ParseFilename([CanBeNull] string contentDispositionString)
        {
            if (string.IsNullOrWhiteSpace(contentDispositionString))
            {
                return null;
            }

            try
            {
                var contentDisposition = new ContentDisposition(contentDispositionString);
                return contentDisposition.FileName;
            }
            catch (ArgumentException)
            {
                return null;
            }
            catch (FormatException)
            {
                return null;
            }
        }

        /// <summary>
        /// Helper method to generate a <see cref="ContentType" /> from a content type string.
        /// </summary>
        /// <param name="contentType">The content type string to parse.</param>
        /// <returns>An instance of <see cref="ContentType" />.</returns>
        private static ContentType ParseContentType([CanBeNull] string contentType)
        {
            if (string.IsNullOrWhiteSpace(contentType))
            {
                return new ContentType(DefaultContentType);
            }

            try
            {
                return new ContentType(contentType);
            }
            catch (ArgumentException)
            {
                return new ContentType(DefaultContentType);
            }
            catch (FormatException)
            {
                return new ContentType(DefaultContentType);
            }
        }
    }
}
