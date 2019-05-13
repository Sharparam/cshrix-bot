// <copyright file="NotificationAction.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Notifications
{
    using System.Diagnostics;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    using Serialization;

    /// <summary>
    /// Contains information about an action to be performed in response to a notification.
    /// </summary>
    [DebuggerDisplay("{Action} ({Name} = {Value})")]
    [JsonConverter(typeof(NotificationActionConverter))]
    public readonly struct NotificationAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationAction" /> structure.
        /// </summary>
        /// <param name="action">An action to perform.</param>
        /// <param name="name">An action target.</param>
        /// <param name="value">A value to set on the action target.</param>
        public NotificationAction(string action, [CanBeNull] string name = null, [CanBeNull] object value = null)
            : this()
        {
            Action = action;
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Gets the action to perform.
        /// </summary>
        public string Action { get; }

        /// <summary>
        /// Gets a name parameter, indicating a target for the <see cref="Action" />.
        /// </summary>
        [CanBeNull]
        public string Name { get; }

        /// <summary>
        /// Gets a value that should be assigned to the <see cref="Name" /> target.
        /// </summary>
        [CanBeNull]
        public object Value { get; }
    }
}
