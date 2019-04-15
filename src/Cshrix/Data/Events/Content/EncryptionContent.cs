// <copyright file="EncryptionContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System;
    using System.Collections.Generic;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    using Serialization;

    public class EncryptionContent : EventContent
    {
        public EncryptionContent([NotNull] IDictionary<string, object> dictionary)
            : base(dictionary)
        {
            Algorithm = GetValueOrDefault<string>("algorithm");
            var rotationPeriodMs = GetValueOrDefault<long>("rotation_period_ms");
            RotationPeriod = TimeSpan.FromMilliseconds(rotationPeriodMs);
            RotationPeriodMessages = GetValueOrDefault<int>("rotation_period_msgs");
        }

        [JsonProperty("algorithm")]
        public string Algorithm { get; }

        [JsonProperty("rotation_period_ms")]
        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        public TimeSpan RotationPeriod { get; }

        [JsonProperty("rotation_period_msgs")]
        public int RotationPeriodMessages { get; }
    }
}
