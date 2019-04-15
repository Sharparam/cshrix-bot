// <copyright file="EventConverter.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Data;
    using Data.Events;
    using Data.Events.Content;

    using Extensions;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class EventConverter : JsonConverter<Event>
    {
        private static readonly IReadOnlyDictionary<string, Func<string, IDictionary<string, object>, EventContent>>
            ContentTypes = new Dictionary<string, Func<string, IDictionary<string, object>, EventContent>>
            {
                ["m.forwarded_room_key"] = (_, dict) => new ForwardedRoomKeyContent(dict),
                ["m.fully_read"] = (_, dict) => new FullyReadContent(dict),
                ["m.presence"] = (_, dict) => new PresenceContent(dict),
                ["m.room_key"] = (_, dict) => new RoomKeyContent(dict),
                ["m.room_key_request"] = (_, dict) => new RoomKeyRequestContent(dict),
                ["m.room.aliases"] = (_, dict) => new AliasesContent(dict),
                ["m.room.avatar"] = (_, dict) => new AvatarContent(dict),
                ["m.room.encrypted"] = (_, dict) => new EncryptedContent(dict),
                ["m.room.encryption"] = (_, dict) => new EncryptionContent(dict),
                ["m.room.message"] = (type, dict) =>
                {
                    var hasFunc = MessageContentTypes.TryGetValue(type, out var func);
                    return hasFunc ? func(dict) : new MessageContent(dict);
                },
                ["m.room.message.feedback"] = (_, dict) => new FeedbackContent(dict),
                ["m.room.name"] = (_, dict) => new RoomNameContent(dict),
                ["m.room.pinned_events"] = (_, dict) => new PinnedEventsContent(dict),
                ["m.room.topic"] = (_, dict) => new RoomTopicContent(dict)
            };

        private static readonly IReadOnlyDictionary<string, Func<IDictionary<string, object>, MessageContent>>
            MessageContentTypes = new Dictionary<string, Func<IDictionary<string, object>, MessageContent>>
            {
                ["m.text"] = dict => new TextMessageContent(dict),
                ["m.emote"] = dict => new EmoteMessageContent(dict),
                ["m.notice"] = dict => new NoticeMessageContent(dict),
                ["m.image"] = dict => new ImageMessageContent(dict),
                ["m.file"] = dict => new FileMessageContent(dict),
                ["m.video"] = dict => new VideoMessageContent(dict),
                ["m.audio"] = dict => new AudioMessageContent(dict),
                ["m.location"] = dict => new LocationMessageContent(dict)
            };

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, Event value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override Event ReadJson(
            JsonReader reader,
            Type objectType,
            Event existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var jObject = JObject.Load(reader);

            var type = jObject.Value<string>("type");
            var content = ParseEventContent(type, jObject);

            var hasSender = jObject.TryGetObject<Identifier>("sender", out var sender);
            var hasId = jObject.TryGetObject<Identifier>("event_id", out var id);
            var hasStateKey = jObject.TryGetValue<string>("state_key", out var stateKey);
            var roomId = jObject.ObjectOrDefault<Identifier?>("room_id");
            var unsigned = jObject.ObjectOrDefault<UnsignedData?>("unsigned");
            var previousContent = jObject.ObjectOrDefault<EventContent>("prev_content");

            var hasSentAt = false;
            DateTimeOffset sentAt;

            if (jObject.ContainsKey("origin_server_ts"))
            {
                var tsValue = jObject["origin_server_ts"];
                var ms = tsValue.Value<long>();
                sentAt = DateTimeOffset.FromUnixTimeMilliseconds(ms);
                hasSentAt = true;
            }
            else
            {
                sentAt = default;
            }

            if (objectType == typeof(StateEvent) || (hasId && hasSender && hasSentAt && hasStateKey))
            {
                return new StateEvent(content, type, id, roomId, sender, sentAt, unsigned, previousContent, stateKey);
            }

            if (objectType == typeof(RoomEvent) || (hasId && hasSender && hasSentAt))
            {
                return new RoomEvent(content, type, id, sender, roomId, sentAt, unsigned);
            }

            if (objectType == typeof(StrippedState) || (hasStateKey && hasSender))
            {
                return new StrippedState(content, stateKey, type, sender);
            }

            if (objectType == typeof(SenderEvent) || hasSender)
            {
                return new SenderEvent(content, type, sender);
            }

            if (objectType == typeof(RoomIdEvent) || roomId.HasValue)
            {
                if (!roomId.HasValue)
                {
                    throw new JsonSerializationException($"Cannot deserialize to {objectType} if room_id is missing");
                }

                return new RoomIdEvent(content, type, roomId.Value);
            }

            return new Event(content, type);
        }

        private static EventContent ParseEventContent(string type, JObject jObject)
        {
            if (!jObject.ContainsKey("content"))
            {
                return null;
            }

            var contentDict = jObject.Object<ReadOnlyDictionary<string, object>>("content");

            var hasMessageType = contentDict.TryGetValue("msgtype", out var messageTypeObject);
            var messageType = hasMessageType ? messageTypeObject.ToString() : null;
            var hasGenerator = ContentTypes.TryGetValue(type, out var generator);

            return hasGenerator ? generator(messageType, contentDict) : new EventContent(contentDict);
        }
    }
}
