// <copyright file="EventContent.cs">
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
    using System.Collections.ObjectModel;

    using JetBrains.Annotations;

    using Newtonsoft.Json.Linq;

    public class EventContent : ReadOnlyDictionary<string, object>
    {
        public EventContent([NotNull] IDictionary<string, object> dictionary)
            : base(dictionary)
        {
        }

        public T GetValueOrDefault<T>([NotNull] string key, T @default = default)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var hasValue = TryGetValue<T>(key, out var value);
            return hasValue ? value : @default;
        }

        public bool TryGetValue<T>([NotNull] string key, out T value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var hasValue = TryGetValue(key, out var obj);

            if (hasValue && obj is T t)
            {
                value = t;
                return true;
            }

            value = default;
            return false;
        }
    }
}
