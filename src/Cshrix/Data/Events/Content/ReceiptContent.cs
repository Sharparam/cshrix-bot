// <copyright file="ReceiptContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    public sealed class ReceiptContent : EventContent
    {
        public ReceiptData this[string key]
        {
            get
            {
                var jToken = AdditionalData[key];
                return jToken.ToObject<ReceiptData>();
            }
        }

        public int Count => Keys.Count();

        public IEnumerable<string> Keys => GetValidKeys();

        public bool ContainsEvent(string key) => AdditionalData.ContainsKey(key);

        public bool TryGetData(string key, out ReceiptData value)
        {
            if (!AdditionalData.TryGetValue(key, out var jToken))
            {
                value = default;
                return false;
            }

            try
            {
                value = jToken.ToObject<ReceiptData>();
                return true;
            }
            catch (JsonSerializationException)
            {
                value = default;
                return false;
            }
        }

        private IEnumerable<string> GetValidKeys()
        {
            var validKeys = new List<string>();

            foreach (var kvp in AdditionalData)
            {
                validKeys.Add(kvp.Key);
            }

            return validKeys;
        }
    }
}
