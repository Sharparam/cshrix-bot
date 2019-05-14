// <copyright file="ResizeMethod.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Runtime.Serialization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Available resize methods for images.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ResizeMethod
    {
        /// <summary>
        /// Scale the image up or down to meet the resolution target.
        /// </summary>
        [EnumMember(Value = "scale")]
        Scale,

        /// <summary>
        /// Crop the image to meet the resolution target.
        /// </summary>
        [EnumMember(Value = "crop")]
        Crop
    }
}
