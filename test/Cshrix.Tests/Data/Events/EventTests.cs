// <copyright file="EventTests.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Tests.Data.Events
{
    using Cshrix.Data.Events;

    using Newtonsoft.Json;

    using NUnit.Framework;

    [TestFixture]
    public class EventTests
    {
        [Test]
        public void TestDeserialization()
        {
            var json = @"{
                ""content"": {
                    ""aliases"": [
                        ""#somewhere:domain.com"",
                        ""#another:domain.com""
                    ]
                },
                ""event_id"": ""$143273582443PhrSn:domain.com"",
                ""origin_server_ts"": 1432735824653,
                ""room_id"": ""!jEsUZKDJdhlrceRyVU:domain.com"",
                ""sender"": ""@example:domain.com"",
                ""state_key"": ""domain.com"",
                ""type"": ""m.room.aliases"",
                ""unsigned"": {
                    ""age"": 1234
                }
            }";

            var ev = JsonConvert.DeserializeObject<Event>(json);

            Assert.NotNull(ev);
        }
    }
}
