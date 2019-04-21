// <copyright file="RoomEventTests.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Tests.Data.Events
{
    using System;

    using Cshrix.Data;
    using Cshrix.Data.Events;
    using Cshrix.Data.Events.Content;

    using Newtonsoft.Json;

    using NUnit.Framework;

    [TestFixture]
    public class RoomEventTests
    {
        [Test]
        public void ShouldParsePowerLevelsEvent()
        {
            var json = @"{
                ""content"": {
                    ""ban"": 50,
                    ""events"": {
                        ""m.room.name"": 100,
                        ""m.room.power_levels"": 100
                    },
                    ""events_default"": 0,
                    ""invite"": 50,
                    ""kick"": 50,
                    ""notifications"": {
                        ""room"": 20
                    },
                    ""redact"": 50,
                    ""state_default"": 50,
                    ""users"": {
                        ""@example:localhost"": 100
                    },
                    ""users_default"": 0
                },
                ""event_id"": ""$143273582443PhrSn:domain.com"",
                ""origin_server_ts"": 1432735824653,
                ""room_id"": ""!jEsUZKDJdhlrceRyVU:domain.com"",
                ""sender"": ""@example:domain.com"",
                ""state_key"": """",
                ""type"": ""m.room.power_levels"",
                ""unsigned"": {
                    ""age"": 1234
                }
            }";

            var ev = JsonConvert.DeserializeObject<StateEvent>(json);

            Assert.That(ev.Content, Is.TypeOf<PowerLevelsContent>());

            var content = (PowerLevelsContent)ev.Content;

            Assert.AreEqual(50, content.Ban);
            Assert.AreEqual(100, content.Events["m.room.name"]);
            Assert.AreEqual(100, content.Events["m.room.power_levels"]);
            Assert.AreEqual(0, content.EventsDefault);
            Assert.AreEqual(50, content.Invite);
            Assert.AreEqual(50, content.Kick);
            Assert.AreEqual(20, content.Notifications.Room);
            Assert.AreEqual(50, content.Redact);
            Assert.AreEqual(50, content.StateDefault);
            Assert.AreEqual(100, content.Users[(UserId)"@example:localhost"]);
            Assert.AreEqual(0, content.UsersDefault);
        }

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

            Assert.AreEqual(new Uri("mxc://domain.com/SEsfnsuifSDFSSEF#auto"), content.AvatarUri);
            Assert.AreEqual("Alice Margatroid", content.DisplayName);
            Assert.AreEqual(Membership.Joined, content.Membership);
        }
    }
}
