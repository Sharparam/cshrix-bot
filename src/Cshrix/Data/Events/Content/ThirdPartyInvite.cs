// <copyright file="ThirdPartyInvite.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using Newtonsoft.Json;

    public readonly struct ThirdPartyInvite
    {
        [JsonConstructor]
        public ThirdPartyInvite(string displayName, SignedData signed)
            : this()
        {
            DisplayName = displayName;
            Signed = signed;
        }

        [JsonProperty("displayname")]
        public string DisplayName { get; }

        [JsonProperty("signed")]
        public SignedData Signed { get; }
    }
}
