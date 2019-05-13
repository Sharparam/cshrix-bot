// <copyright file="AudioMessageContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System;

    public class AudioMessageContent : UriMessageContent<AudioInfo>
    {
        public AudioMessageContent(
            string body,
            string messageType,
            AudioInfo info,
            Uri uri,
            EncryptedFile? encryptedFile)
            : base(body, messageType, info, uri, encryptedFile)
        {
        }
    }
}