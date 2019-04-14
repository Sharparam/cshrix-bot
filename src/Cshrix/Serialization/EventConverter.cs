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

    using Data;
    using Data.Events;

    using Extensions;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class EventConverter : JsonConverter<Event>
    {
        private static readonly IReadOnlyDictionary<string, Func<EventContent, string, Event>> EventTypeGenerators;

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

            var content = jObject.Value<EventContent>("content");
            var type = jObject.Value<string>("type");
            var hasSender = jObject.TryGetValue<Identifier>("sender", out var sender);
            var hasId = jObject.TryGetValue<Identifier>("event_id", out var id);
            var hasStateKey = jObject.TryGetValue<string>("state_key", out var stateKey);
            var hasRoomId = jObject.TryGetValue<Identifier?>("room_id", out var roomId);
            var hasUnsigned = jObject.TryGetValue<UnsignedData?>("unsigned", out var unsigned);
            var hasPreviousContent = jObject.TryGetValue<EventContent>("prev_content", out var previousContent);

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

            return new Event(content, type);
        }
    }
}
