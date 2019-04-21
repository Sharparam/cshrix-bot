// <copyright file="UserIdUserIdentifier.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using Newtonsoft.Json;

    public readonly struct UserIdUserIdentifier : IUserIdentifier
    {
        [JsonConstructor]
        public UserIdUserIdentifier(UserId userId)
            : this() =>
            UserId = userId;

        public UserIdentifierType Type => UserIdentifierType.User;

        [JsonProperty("user")]
        public UserId UserId { get; }
    }
}
