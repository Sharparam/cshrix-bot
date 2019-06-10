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

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Describes the contents of a receipt event.
    /// </summary>
    [PublicAPI]
    public sealed class ReceiptContent : EventContent
    {
        /// <summary>
        /// Gets receipt data for the specified key.
        /// </summary>
        /// <param name="key">The key to get receipt data for.</param>
        public ReceiptData this[string key]
        {
            get
            {
                var jToken = AdditionalData[key];
                return jToken.ToObject<ReceiptData>();
            }
        }

        /// <summary>
        /// Gets the number of event entries.
        /// </summary>
        public int Count => Keys.Count();

        /// <summary>
        /// Gets all valid receipt keys.
        /// </summary>
        public IEnumerable<string> Keys => GetValidKeys();

        /// <summary>
        /// Returns a boolean value indicating whether the specified key (event ID) is present.
        /// </summary>
        /// <param name="key">The key (event ID) to check.</param>
        /// <returns><c>true</c> if the specified key exists; otherwise, <c>false</c>.</returns>
        public bool ContainsEvent(string key) => AdditionalData.ContainsKey(key);

        /// <summary>
        /// Attempts to get <see cref="ReceiptData" /> for the specified key.
        /// </summary>
        /// <param name="key">The key to retrieve.</param>
        /// <param name="value">
        /// When this function returns, will contain the <see cref="ReceiptData" /> at the requested key;
        /// or the default value for <see cref="ReceiptData" /> if the key was not found.
        /// </param>
        /// <returns><c>true</c> if the key was found; otherwise, <c>false</c>.</returns>
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

        /// <summary>
        /// Gets valid keys.
        /// </summary>
        /// <returns>An enumerable containing all valid keys.</returns>
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
