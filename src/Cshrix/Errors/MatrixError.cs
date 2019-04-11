// <copyright file="MatrixError.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Errors
{
    using Newtonsoft.Json;

    public class MatrixError
    {
        public MatrixError(string code, string message)
        {
            Code = code;
            Message = message;
        }

        [JsonProperty("errcode")]
        public string Code { get; }

        [JsonProperty("error")]
        public string Message { get; }
    }
}
