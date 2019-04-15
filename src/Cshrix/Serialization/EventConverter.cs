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
    using Data.Events.Content;

    using Extensions;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class EventConverter : JsonConverter<Event>
    {
        private static readonly IReadOnlyDictionary<string, Type>
            ContentTypes = new Dictionary<string, Type>
            {
                ["m.call.answer"] = typeof(CallAnswerContent),
                ["m.call.candidates"] = typeof(CallCandidatesContent),
                ["m.call.hangup"] = typeof(CallContent),
                ["m.call.invite"] = typeof(CallInviteContent),
                ["m.direct"] = typeof(DirectContent),
                ["m.forwarded_room_key"] = typeof(ForwardedRoomKeyContent),
                ["m.fully_read"] = typeof(FullyReadContent),
                ["m.ignored_user_list"] = typeof(IgnoredUsersContent),
                ["m.presence"] = typeof(PresenceContent),
                ["m.receipt"] = typeof(ReceiptContent),
                ["m.room_key"] = typeof(RoomKeyContent),
                ["m.room_key_request"] = typeof(RoomKeyRequestContent),
                ["m.room.aliases"] = typeof(AliasesContent),
                ["m.room.avatar"] = typeof(AvatarContent),
                ["m.room.canonical_alias"] = typeof(CanonicalAliasContent),
                ["m.room.create"] = typeof(CreationContent),
                ["m.room.encrypted"] = typeof(EncryptedContent),
                ["m.room.encryption"] = typeof(EncryptionContent),
                ["m.room.guest_access"] = typeof(GuestAccessContent),
                ["m.room.history_visibility"] = typeof(HistoryVisibilityContent),
                ["m.room.join_rules"] = typeof(JoinRuleContent),
                ["m.room.member"] = typeof(MemberContent),
                ["m.room.message"] = typeof(MessageContent),
                ["m.room.message.feedback"] = typeof(FeedbackContent),
                ["m.room.name"] = typeof(RoomNameContent),
                ["m.room.power_levels"] = typeof(PowerLevelsContent),
                ["m.room.redaction"] = typeof(RedactionContent),
                ["m.room.pinned_events"] = typeof(PinnedEventsContent),
                ["m.room.server_acl"] = typeof(ServerAclContent),
                ["m.room.third_party_invite"] = typeof(ThirdPartyInviteContent),
                ["m.room.topic"] = typeof(RoomTopicContent),
                ["m.sticker"] = typeof(StickerContent),
                ["m.tag"] = typeof(TagsContent),
                ["m.typing"] = typeof(TypingContent),
                ["m.text"] = typeof(TextMessageContent),
                ["m.emote"] = typeof(EmoteMessageContent),
                ["m.notice"] = typeof(NoticeMessageContent),
                ["m.image"] = typeof(ImageMessageContent),
                ["m.file"] = typeof(FileMessageContent),
                ["m.video"] = typeof(VideoMessageContent),
                ["m.audio"] = typeof(AudioMessageContent),
                ["m.location"] = typeof(LocationMessageContent)
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
            var redacts = jObject.ObjectOrDefault<Identifier?>("redacts");

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
                return new StateEvent(
                    content,
                    type,
                    redacts,
                    id,
                    roomId,
                    sender,
                    sentAt,
                    unsigned,
                    previousContent,
                    stateKey);
            }

            if (objectType == typeof(RoomEvent) || (hasId && hasSender && hasSentAt))
            {
                return new RoomEvent(content, type, redacts, id, sender, roomId, sentAt, unsigned);
            }

            if (objectType == typeof(StrippedState) || (hasStateKey && hasSender))
            {
                return new StrippedState(content, stateKey, type, redacts, sender);
            }

            if (objectType == typeof(SenderEvent) || hasSender)
            {
                return new SenderEvent(content, type, redacts, sender);
            }

            if (objectType == typeof(RoomIdEvent) || roomId.HasValue)
            {
                if (!roomId.HasValue)
                {
                    throw new JsonSerializationException($"Cannot deserialize to {objectType} if room_id is missing");
                }

                return new RoomIdEvent(content, type, redacts, roomId.Value);
            }

            return new Event(content, type, redacts);
        }

        private static EventContent ParseEventContent(string type, JObject jObject)
        {
            if (!jObject.TryGetValue("content", out var contentToken))
            {
                return null;
            }

            if (!(contentToken is JObject contentObject))
            {
                return null;
            }

            var hasMessageType = contentObject.TryGetValue("msgtype", out var messageTypeObject);
            var messageType = hasMessageType ? (string)messageTypeObject : null;
            var hasType = ContentTypes.TryGetValue(type, out var contentType);

            if (hasMessageType && ContentTypes.TryGetValue(messageType, out var messageContentType))
            {
                contentType = messageContentType;
                hasType = true;
            }

            return hasType ? (EventContent)contentObject.ToObject(contentType) : contentObject.ToObject<EventContent>();
        }
    }
}
