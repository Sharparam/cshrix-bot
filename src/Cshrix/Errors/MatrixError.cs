// <copyright file="MatrixError.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Errors
{
    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public class MatrixError
    {
        public MatrixError([CanBeNull] string code, [CanBeNull] string message)
        {
            Code = code;
            Message = message;
        }

        [JsonProperty("errcode")]
        [CanBeNull]
        public string Code { get; }

        [JsonProperty("error")]
        [CanBeNull]
        public string Message { get; }
    }
}
