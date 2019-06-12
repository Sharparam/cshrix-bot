// <copyright file="IUserIdentifier.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using Newtonsoft.Json;

    /// <summary>
    /// Identification information for a user.
    /// </summary>
    public interface IUserIdentifier
    {
        /// <summary>
        /// Gets the type of this identification.
        /// </summary>
        [JsonProperty("type")]
        UserIdentifierType Type { get; }
    }
}
