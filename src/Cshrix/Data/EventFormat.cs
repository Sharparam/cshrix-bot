// <copyright file="EventFormat.cs">
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
    /// Specifies a format for events returned from search/filter APIs.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EventFormat
    {
        /// <summary>
        /// A format suitable for clients.
        /// </summary>
        [EnumMember(Value = "client")]
        Client,

        /// <summary>
        /// A format that returns the events in their raw form, as received over federation.
        /// </summary>
        [EnumMember(Value = "federation")]
        Federation
    }
}
