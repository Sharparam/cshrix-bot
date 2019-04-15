// <copyright file="ReceiptContent.cs">
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
    using System.Linq;

    using Newtonsoft.Json;

    public class ReceiptContent : EventContent
    {
        public ReceiptData this[Identifier key]
        {
            get
            {
                var jToken = AdditionalData[key];
                return jToken.ToObject<ReceiptData>();
            }
        }

        public int Count => Keys.Count();

        public IEnumerable<Identifier> Keys => GetValidKeys();

        public bool ContainsEvent(Identifier key) => AdditionalData.ContainsKey(key);

        public bool TryGetData(Identifier key, out ReceiptData value)
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

        private IEnumerable<Identifier> GetValidKeys()
        {
            var validKeys = new List<Identifier>();
            foreach (var kvp in AdditionalData)
            {
                try
                {
                    var identifier = new Identifier(kvp.Key);
                    validKeys.Add(identifier);
                }
                catch (ArgumentException)
                {
                }
            }

            return validKeys;
        }
    }
}
