// <copyright file="DirectContent.cs">
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

    public class DirectContent : EventContent
    {
        public IReadOnlyCollection<string> this[UserId key]
        {
            get
            {
                var jToken = AdditionalData[key];
                return jToken.ToObject<IReadOnlyCollection<string>>();
            }
        }

        public int Count => Keys.Count();

        public IEnumerable<UserId> Keys => GetValidKeys();

        public bool ContainsUser(UserId key) => AdditionalData.ContainsKey(key);

        public bool TryGetData(UserId key, out IReadOnlyCollection<string> values)
        {
            if (!AdditionalData.TryGetValue(key, out var jToken))
            {
                values = default;
                return false;
            }

            try
            {
                values = jToken.ToObject<IReadOnlyCollection<string>>();
                return true;
            }
            catch (JsonSerializationException)
            {
                values = default;
                return false;
            }
        }

        private IEnumerable<UserId> GetValidKeys()
        {
            var validKeys = new List<UserId>();
            foreach (var kvp in AdditionalData)
            {
                var valid = UserId.TryParse(kvp.Key, out var userId);
                if (valid)
                {
                    validKeys.Add(userId);
                }
            }

            return validKeys;
        }
    }
}
