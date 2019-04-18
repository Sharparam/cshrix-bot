// <copyright file="NotificationAction.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using JetBrains.Annotations;

    using Newtonsoft.Json;

    using Serialization;

    [JsonConverter(typeof(NotificationActionConverter))]
    public readonly struct NotificationAction
    {
        public NotificationAction(string action, [CanBeNull] string name = null, [CanBeNull] object value = null)
            : this()
        {
            Action = action;
            Name = name;
            Value = value;
        }

        public string Action { get; }

        [CanBeNull]
        public string Name { get; }

        [CanBeNull]
        public object Value { get; }
    }
}
