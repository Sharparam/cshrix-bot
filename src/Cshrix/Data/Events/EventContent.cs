// <copyright file="EventContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using JetBrains.Annotations;

    public class EventContent : ReadOnlyDictionary<string, object>
    {
        public EventContent([NotNull] IDictionary<string, object> dictionary)
            : base(dictionary)
        {
        }

        public bool TryGetValue<T>(string key, out T value)
        {
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
