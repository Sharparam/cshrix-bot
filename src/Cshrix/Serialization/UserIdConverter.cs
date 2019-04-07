// <copyright file="UserIdConverter.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Serialization
{
    using Data;

    public sealed class UserIdConverter : AbstractIdentifierConverter<UserId>
    {
        public UserIdConverter()
            : base(
                id => new UserId(id),
                (localpart, domain) => new UserId(localpart, domain))
        {
        }
    }
}
