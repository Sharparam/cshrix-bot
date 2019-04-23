// <copyright file="EventContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using Extensions;

    using JetBrains.Annotations;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class EventContent
    {
        [UsedImplicitly]
        [JsonExtensionData]
        public JObject AdditionalData
        {
            get;

            [UsedImplicitly]
            protected set;
        }

        protected T GetValueOrDefault<T>([NotNull] string key, T @default = default) =>
            AdditionalData.ValueOrDefault(key, @default);

        protected bool TryGetValue<T>([NotNull] string key, out T value) => AdditionalData.TryGetValue(key, out value);

        protected T GetObjectOrDefault<T>([NotNull] string key, T @default = default) =>
            AdditionalData.ObjectOrDefault(key, @default);

        protected bool TryGetObject<T>([NotNull] string key, out T value) => AdditionalData.TryGetObject(key, out value);
    }
}
