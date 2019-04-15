// <copyright file="JObjectExtensions.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Extensions
{
    using Newtonsoft.Json.Linq;

    public static class JObjectExtensions
    {
        public static T Object<T>(this JObject jObject, string key) => jObject[key].ToObject<T>();

        public static bool TryGetValue<T>(this JObject jObject, string key, out T value)
        {
            if (!jObject.ContainsKey(key))
            {
                value = default;
                return false;
            }

            value = jObject.Value<T>(key);
            return true;
        }

        public static T ObjectOrDefault<T>(this JObject jObject, string key, T @default = default) =>
            jObject.TryGetObject<T>(key, out var value) ? value : @default;

        public static bool TryGetObject<T>(this JObject jObject, string key, out T value)
        {
            if (!jObject.ContainsKey(key))
            {
                value = default;
                return false;
            }

            value = jObject.Object<T>(key);
            return true;
        }
    }
}