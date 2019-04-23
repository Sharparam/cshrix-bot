// <copyright file="OneTimeKeyTests.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

using NUnit.Framework;

namespace Cshrix.Tests.Data
{
    using System.Collections.Generic;

    using Cshrix.Data;

    using Newtonsoft.Json;

    [TestFixture]
    public class OneTimeKeyTests
    {
        [Test]
        public void ShouldSerializeToStringIfUnsigned()
        {
            var otk = new OneTimeKey("foobar");
            var json = JsonConvert.SerializeObject(otk);

            Assert.AreEqual("\"foobar\"", json);
        }

        [Test]
        public void ShouldSerializeToObjectIfSigned()
        {
            var otk = new OneTimeKey(
                "foobar",
                new Dictionary<UserId, IDictionary<string, string>>
                {
                    [(UserId)"@sharparam:matrix.sharparam.com"] = new Dictionary<string, string>
                    {
                        ["fizz"] = "buzz"
                    }
                });

            var json = JsonConvert.SerializeObject(otk);

            const string ExpectedJson = "{\"key\":\"foobar\",\"signatures\":{\"@sharparam:matrix.sharparam.com\":{\"fizz\":\"buzz\"}}}";

            Assert.AreEqual(ExpectedJson, json);
        }
    }
}
