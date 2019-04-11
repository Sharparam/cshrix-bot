// <copyright file="IIdentifier.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System;

    using Newtonsoft.Json;

    using Serialization;

    [JsonConverter(typeof(IdentifierConverter))]
    public interface IIdentifier : IEquatable<IIdentifier>, IEquatable<string>
    {
        IdentifierType Type { get; }

        char Sigil { get; }

        string Localpart { get; }

        ServerName Domain { get; }
    }
}