// <copyright file="RoomEventTests.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

using NUnit.Framework;

namespace Cshrix.Tests.Data.Events
{
    using Cshrix.Data;
    using Cshrix.Data.Events;
    using Cshrix.Data.Events.Content;

    using Newtonsoft.Json;

    [TestFixture]
    public class RoomEventTests
    {
        [Test]
        public void ShouldParseSimpleMemberEvent()
        {
            var json = @"{
                ""content"": {
                    ""avatar_url"": ""mxc://domain.com/SEsfnsuifSDFSSEF#auto"",
                    ""displayname"": ""Alice Margatroid"",
                    ""membership"": ""join""
                },
                ""event_id"": ""$143273582443PhrSn:domain.com"",
                ""origin_server_ts"": 1432735824653,
                ""room_id"": ""!jEsUZKDJdhlrceRyVU:domain.com"",
                ""sender"": ""@example:domain.com"",
                ""state_key"": ""@alice:domain.com"",
                ""type"": ""m.room.member"",
                ""unsigned"": {
                    ""age"": 1234
                }
            }";

            var ev = JsonConvert.DeserializeObject<StateEvent>(json);

            Assert.That(ev.Content, Is.TypeOf<MemberContent>());

            var content = (MemberContent)ev.Content;

            Assert.AreEqual("mxc://domain.com/SEsfnsuifSDFSSEF#auto", content.AvatarUri);
            Assert.AreEqual("Alice Margatroid", content.DisplayName);
            Assert.AreEqual(Membership.Joined, content.Membership);
        }
    }
}
