// <copyright file="CriteriaKeysSerializationTests.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

using NUnit.Framework;

namespace Cshrix.Tests.Serialization
{
    using Cshrix.Data.Search;

    using Newtonsoft.Json;

    [TestFixture]
    public class CriteriaKeysSerializationTests
    {
        [TestCase(SearchKeys.None, "[]")]
        [TestCase(SearchKeys.ContentName, "[\"content.name\"]")]
        [TestCase(SearchKeys.All, "[\"content.body\",\"content.name\",\"content.topic\"]")]
        public void ShouldSerialize(SearchKeys values, string expected)
        {
            var json = JsonConvert.SerializeObject(values);
            Assert.AreEqual(expected, json);
        }

        [TestCase("[]", SearchKeys.None)]
        [TestCase("[\"content.body\"]", SearchKeys.ContentBody)]
        [TestCase("[\"content.body\",\"content.name\",\"content.topic\"]", SearchKeys.All)]
        public void ShouldDeserialize(string json, SearchKeys expected)
        {
            var values = JsonConvert.DeserializeObject<SearchKeys>(json);
            Assert.AreEqual(expected, values);
        }
    }
}
