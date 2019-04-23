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

    public abstract class UriMessageContent<T> : MessageContent
    {
        protected UriMessageContent(string body, string messageType, T info, Uri uri, EncryptedFile? encryptedFile)
            : base(body, messageType)
        {
            Info = info;
            Uri = uri;
            EncryptedFile = encryptedFile;
        }

        [JsonProperty("info")]
        public T Info { get; }

        [JsonProperty("url")]
        [CanBeNull]
        public Uri Uri { get; }

        [JsonProperty("file")]
        public EncryptedFile? EncryptedFile { get; }
    }
}
