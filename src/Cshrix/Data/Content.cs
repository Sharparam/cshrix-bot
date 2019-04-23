// <copyright file="Content.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;
    using System.Net.Mime;

    using JetBrains.Annotations;

    public readonly struct Content
    {
        public Content([CanBeNull] string filename, ContentType contentType, IReadOnlyCollection<byte> bytes)
            : this()
        {
            Filename = filename;
            ContentType = contentType;
            Bytes = bytes;
        }

        [CanBeNull]
        public string Filename { get; }

        public ContentType ContentType { get; }

        public IReadOnlyCollection<byte> Bytes { get; }
    }
}
