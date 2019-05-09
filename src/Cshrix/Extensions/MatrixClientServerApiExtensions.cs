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
    using Data.Events;

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
        public static void SetBearerToken(this IMatrixClientServerApi api, string accessToken) =>
            api.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        public static Task<OpenIdToken> RequestOpenIdTokenAsync(this IMatrixClientServerApi api, UserId userId) =>
            api.RequestOpenIdTokenAsync(userId, new object());

        public static Task<Content> DownloadContentAsync(
            this IMatrixClientServerApi api,
            Uri mediaUri,
            [CanBeNull] string filename = null,
            bool? allowRemote = null)
        {
            var serverName = mediaUri.Authority;

            // Strip the leading slash (/)
            var mediaId = mediaUri.AbsolutePath.Substring(1);

            return api.DownloadContentAsync(serverName, mediaId, filename, allowRemote);
        }

        public static async Task<Content> DownloadContentAsync(
            this IMatrixClientServerApi api,
            string serverName,
            string mediaId,
            [CanBeNull] string filename = null,
            bool? allowRemote = null)
        {
            using (var response = await DownloadAsync(api, serverName, mediaId, filename, allowRemote))
            {
                return await CreateContentFromResponse(response).ConfigureAwait(false);
            }
        }

        public static Task<Content> DownloadThumbnailContentAsync(
            this IMatrixClientServerApi api,
            Uri mediaUri,
            int width,
            int height,
            ResizeMethod resizeMethod = ResizeMethod.Scale,
            bool? allowRemote = null)
        {
            var serverName = mediaUri.Authority;

            // Strip the leading slash (/)
            var mediaId = mediaUri.AbsolutePath.Substring(1);

            return api.DownloadThumbnailContentAsync(serverName, mediaId, width, height, resizeMethod, allowRemote);
        }

        public static async Task<Content> DownloadThumbnailContentAsync(
            this IMatrixClientServerApi api,
            string serverName,
            string mediaId,
            int width,
            int height,
            ResizeMethod resizeMethod = ResizeMethod.Scale,
            bool? allowRemote = null)
        {
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
