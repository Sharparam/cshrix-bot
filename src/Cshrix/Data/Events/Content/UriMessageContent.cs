// <copyright file="UriMessageContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains content for events that have data with a corresponding URI and possible encrypted file content.
    /// </summary>
    /// <typeparam name="T">The type of of the data.</typeparam>
    public abstract class UriMessageContent<T> : MessageContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UriMessageContent{T}" /> class.
        /// </summary>
        /// <param name="body">A description of the content for accessibility.</param>
        /// <param name="messageType">The type of the message.</param>
        /// <param name="info">The message data.</param>
        /// <param name="uri">A URI for downloading the data, if not encrypted.</param>
        /// <param name="encryptedFile">Information about the encrypted file, if any.</param>
        protected UriMessageContent(
            string body,
            string messageType,
            T info,
            [CanBeNull] Uri uri,
            EncryptedFile? encryptedFile)
            : base(body, messageType)
        {
            Info = info;
            Uri = uri;
            EncryptedFile = encryptedFile;
        }

        /// <summary>
        /// Gets the contained data object.
        /// </summary>
        [JsonProperty("info")]
        public T Info { get; }

        /// <summary>
        /// Gets a URI to use for downloading the data.
        /// </summary>
        [JsonProperty("url")]
        [CanBeNull]
        public Uri Uri { get; }

        /// <summary>
        /// Gets information about an encrypted version of the data file, if present.
        /// </summary>
        [JsonProperty("file")]
        public EncryptedFile? EncryptedFile { get; }
    }
}
