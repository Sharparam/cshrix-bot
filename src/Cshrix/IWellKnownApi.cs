// <copyright file="IWellKnownApi.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix
{
    using System.Threading.Tasks;

    using Data;

    using RestEase;

    public interface IWellKnownApi
    {
        [Header("User-Agent", nameof(Cshrix))]
        string UserAgent { get; set; }

        [Get(".well-known/matrix/client")]
        Task<ClientInfo> GetClientInfoAsync();
    }
}
