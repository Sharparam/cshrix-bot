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

    public static class MatrixClientServerApiExtensions
    {
        private const string DefaultContentType = "application/octet-stream";

        public static void SetBearerToken(this IMatrixClientServerApi api, string accessToken) =>
            api.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        public static Task<SyncResponse> SyncAsync(
            this IMatrixClientServerApi api,
            string since = null,
            string filter = null,
            bool fullState = false,
            string setPresence = "offline",
            TimeSpan timeout = default) =>
            api.SyncAsync(since, filter, fullState, setPresence, (long)timeout.TotalMilliseconds);

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
                return await CreateContentFromResponse(response);
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
                return await CreateContentFromResponse(response);
            }
        }

        public static Task<PreviewInfo> GetUriPreviewInfoAsync(this IMatrixClientServerApi api, Uri uri, DateTimeOffset? at = null)
        {
            var timestamp = at?.ToUnixTimeMilliseconds();
            return api.GetUriPreviewInfoAsync(uri, timestamp);
        }

        private static Task<HttpResponseMessage> DownloadAsync(
            IMatrixClientServerApi api,
            string serverName,
            string mediaId,
            [CanBeNull] string filename,
            bool? allowRemote)
        {
            if (filename == null)
            {
                return api.DownloadAsync(serverName, mediaId, allowRemote);
            }

            return api.DownloadAsync(serverName, mediaId, filename, allowRemote);
        }

        private static async Task<Content> CreateContentFromResponse(HttpResponseMessage response)
        {
            var hasContentDisposition =
                response.Headers.TryGetFirstContentDisposition(out var contentDispositionString);

            var hasContentType = response.Headers.TryGetFirstContentType(out var contentTypeString);

            var filename = hasContentDisposition ? ParseFilename(contentDispositionString) : null;
            var contentType = ParseContentType(hasContentType ? contentTypeString : DefaultContentType);
            var bytes = await response.Content.ReadAsByteArrayAsync();
            return new Content(filename, contentType, bytes);
        }

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
