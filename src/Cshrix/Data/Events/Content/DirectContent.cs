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

    /// <summary>
    /// Specifies rooms that are direct messaging rooms.
    /// </summary>
    public sealed class DirectContent : EventContent
    {
        /// <summary>
        /// Gets the direct messaging room IDs for a specific user ID.
        /// </summary>
        /// <param name="key">The user ID to look up.</param>
        public IReadOnlyCollection<string> this[UserId key]
        {
            get
            {
                var jToken = AdditionalData[key];
                return jToken.ToObject<IReadOnlyCollection<string>>();
            }
        }

        /// <summary>
        /// Gets the number of keys (user IDs).
        /// </summary>
        public int Count => Keys.Count();

        /// <summary>
        /// Gets all user ID keys.
        /// </summary>
        public IEnumerable<UserId> Keys => GetValidKeys();

        /// <summary>
        /// Checks whether the specified user ID exists in the data.
        /// </summary>
        /// <param name="key">The user ID to check for.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="key" /> exists among the user IDs; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsUser(UserId key) => AdditionalData.ContainsKey(key);

        /// <summary>
        /// Attempts to get a collection of direct messaging room IDs for the specified user ID.
        /// </summary>
        /// <param name="key">The user ID to get direct messaging room IDs for.</param>
        /// <param name="values">
        /// When this method returns, contains the room IDs that are considered direct messaging rooms, if the user ID
        /// was found; otherwise, the default value for <see cref="IReadOnlyCollection{String}" />.
        /// </param>
        /// <returns><c>true</c> if the user ID existed in the data; otherwise, <c>false</c>.</returns>
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

        /// <summary>
        /// Get all keys from the data that are valid user IDs.
        /// </summary>
        /// <returns>A list of user IDs.</returns>
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
