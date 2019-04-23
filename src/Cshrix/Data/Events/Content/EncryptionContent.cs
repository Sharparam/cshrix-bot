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

    using Newtonsoft.Json;

    using Serialization;

    public class EncryptionContent : EventContent
    {
        public EncryptionContent(string algorithm, TimeSpan rotationPeriod, int rotationPeriodMessages)
        {
            Algorithm = algorithm;
            RotationPeriod = rotationPeriod;
            RotationPeriodMessages = rotationPeriodMessages;
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
