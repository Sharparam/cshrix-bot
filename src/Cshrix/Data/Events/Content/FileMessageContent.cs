// <copyright file="FileMessageContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System.Collections.Generic;

    using JetBrains.Annotations;

    public class FileMessageContent : UriMessageContent<FileInfo>
    {
        public FileMessageContent([NotNull] IDictionary<string, object> dictionary)
            : base(dictionary)
        {
        }
    }
}