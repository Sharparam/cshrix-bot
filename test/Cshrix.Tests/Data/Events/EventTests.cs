// <copyright file="EventTests.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Tests.Data.Events
{
    using Cshrix.Data;
    using Cshrix.Data.Events;
    using Cshrix.Data.Events.Content;

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

        [Test]
        public void TestEncryptedDeserialization()
        {
            var json = @"{
              ""content"": {
                        ""body"": ""something-important.jpg"",
                        ""file"": {
                            ""url"": ""mxc://domain.com/FHyPlCeYUSFFxlgbQYZmoEoe"",
                            ""mimetype"": ""image/jpeg"",
                            ""v"": ""v2"",
                            ""key"": {
                                ""alg"": ""A256CTR"",
                                ""ext"": true,
                                ""k"": ""aWF6-32KGYaC3A_FEUCk1Bt0JA37zP0wrStgmdCaW-0"",
                                ""key_ops"": [""encrypt"",""decrypt""],
                                ""kty"": ""oct""
                            },
                            ""iv"": ""w+sE15fzSc0AAAAAAAAAAA"",
                            ""hashes"": {
                                ""sha256"": ""fdSLu/YkRx3Wyh3KQabP3rd6+SFiKg5lsJZQHtkSAYA""
                            }
                        },
                        ""info"": {
                            ""mimetype"": ""image/jpeg"",
                            ""h"": 1536,
                            ""size"": 422018,
                            ""thumbnail_file"": {
                                ""hashes"": {
                                    ""sha256"": ""/NogKqW5bz/m8xHgFiH5haFGjCNVmUIPLzfvOhHdrxY""
                                },
                                ""iv"": ""U+k7PfwLr6UAAAAAAAAAAA"",
                                ""key"": {
                                    ""alg"": ""A256CTR"",
                                    ""ext"": true,
                                    ""k"": ""RMyd6zhlbifsACM1DXkCbioZ2u0SywGljTH8JmGcylg"",
                                    ""key_ops"": [""encrypt"", ""decrypt""],
                                    ""kty"": ""oct""
                                },
                                ""mimetype"": ""image/jpeg"",
                                ""url"": ""mxc://domain.com/pmVJxyxGlmxHposwVSlOaEOv"",
                                ""v"": ""v2""
                            },
                            ""thumbnail_info"": {
                                ""h"": 768,
                                ""mimetype"": ""image/jpeg"",
                                ""size"": 211009,
                                ""w"": 432
                            },
                            ""w"": 864
                        },
                        ""msgtype"": ""m.image""
                    },
                    ""event_id"": ""$143273582443PhrSn:domain.com"",
                    ""origin_server_ts"": 1432735824653,
                    ""room_id"": ""!jEsUZKDJdhlrceRyVU:domain.com"",
                    ""sender"": ""@example:domain.com"",
                    ""type"": ""m.room.message"",
                    ""unsigned"": {
                        ""age"": 1234
                    }
                }";

            var ev = JsonConvert.DeserializeObject<Event>(json);

            Assert.NotNull(ev);
        }

        [Test]
        public void ShouldParseFeedbackEvent()
        {
            var json = @"{
                ""content"": {
                    ""target_event_id"": ""$WEIGFHFW:localhost"",
                    ""type"": ""delivered""
                },
                ""event_id"": ""$143273582443PhrSn:domain.com"",
                ""origin_server_ts"": 1432735824653,
                ""room_id"": ""!jEsUZKDJdhlrceRyVU:domain.com"",
                ""sender"": ""@example:domain.com"",
                ""type"": ""m.room.message.feedback"",
                ""unsigned"": {
                    ""age"": 1234
                }
            }";

            var ev = JsonConvert.DeserializeObject<Event>(json);

            var expectedEventId = "$WEIGFHFW:localhost";
            var expectedType = FeedbackType.Delivered;

            var content = ev.Content as FeedbackContent;

            Assert.NotNull(content);

            Assert.AreEqual(expectedEventId, content.TargetEventId);
            Assert.AreEqual(expectedType, content.Type);
        }

        [Test]
        public void ShouldParseReceiptEvent()
        {
            const string Json = @"{
                ""content"": {
                    ""$1435641916114394fHBLK:matrix.org"": {
                        ""m.read"": {
                            ""@rikj:jki.re"": {
                                ""ts"": 1436451550453
                            }
                        }
                    }
                },
                ""room_id"": ""!jEsUZKDJdhlrceRyVU:domain.com"",
                ""type"": ""m.receipt""
            }";

            var ev = JsonConvert.DeserializeObject<Event>(Json);

            Assert.That(ev.Content, Is.TypeOf<ReceiptContent>());

            var eventId = "$1435641916114394fHBLK:matrix.org";
            var userId = (UserId)"@rikj:jki.re";
            const long Timestamp = 1436451550453;

            var content = (ReceiptContent)ev.Content;

            Assert.True(content.ContainsEvent(eventId));

            var data = content[eventId];

            Assert.True(data.Read.ContainsKey(userId));

            Assert.AreEqual(Timestamp, data.Read[userId].Timestamp.ToUnixTimeMilliseconds());
        }
    }
}
