// <copyright file="ServerName.cs">
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

    [JsonConverter(typeof(ServerNameConverter))]
    public readonly struct ServerName
    {
        [JsonConstructor]
        public ServerName(string raw)
            : this()
        {
            if (raw == null)
            {
                throw new ArgumentNullException(nameof(raw));
            }

            if (string.IsNullOrWhiteSpace(raw))
            {
                throw new ArgumentException("Input string cannot be empty", nameof(raw));
            }

            var split = raw.Split(':');

            Hostname = split[0];

            Port = split.Length > 1 ? (ushort?)ushort.Parse(split[1]) : null;
        }

        public ServerName(string hostname, ushort? port)
            : this()
        {
            if (hostname == null)
            {
                throw new ArgumentNullException(nameof(hostname));
            }

            if (string.IsNullOrWhiteSpace(hostname))
            {
                throw new ArgumentException("Hostname cannot be empty", nameof(hostname));
            }

            Hostname = hostname;
            Port = port;
        }

        public string Hostname { get; }

        public ushort? Port { get; }

        public ushort GetPortOrDefault(ushort defaultValue = default) => Port ?? defaultValue;

        public override string ToString() => Port.HasValue ? $"{Hostname}:{Port}" : Hostname;
    }
}
