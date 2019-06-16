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

    /// <inheritdoc />
    /// <summary>
    /// A converter to convert <see cref="Event" /> objects to/from their JSON representation.
    /// </summary>
    /// <remarks>
    /// This is a "smart" converter, it will convert JSON to the event that fits best according to available
    /// properties in the JSON data. The same applies for the <see cref="EventContent" /> supplied with the event.
    /// </remarks>
    public class EventConverter : JsonConverter<Event>
    {
        /// <summary>
        /// Contains a mapping between <see cref="Event" /> and <see cref="EventContent" /> types and the
        /// Cshrix <see cref="Type" /> used to contain the event content data.
        /// </summary>
        /// <remarks>
        /// This is used to select the appropriate <see cref="EventContent" /> subclass to deserialized event content
        /// into, depending on the event and, if applicable, event content type.
        /// </remarks>
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
                ["m.room.pinned_events"] = typeof(PinnedEventsContent),
                ["m.room.power_levels"] = typeof(PowerLevelsContent),
                ["m.room.redaction"] = typeof(RedactionContent),
                ["m.room.related_groups"] = typeof(GroupsContent),
                ["m.room.server_acl"] = typeof(ServerAclContent),
                ["m.room.third_party_invite"] = typeof(ThirdPartyInviteContent),
                ["m.room.tombstone"] = typeof(TombstoneContent),
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

        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether this <see cref="JsonConverter" /> can write JSON.
        /// </summary>
        /// <value><c>true</c> if this <see cref="JsonConverter" /> can write JSON; otherwise, <c>false</c>.</value>
        public override bool CanWrite => false;

        /// <inheritdoc />
        /// <summary>
        /// Throws <see cref="NotSupportedException" />, as this converter doesn't implement custom serialization
        /// and instead delegates this to the default serialization behaviour by Newtonsoft.Json;
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, Event value, JsonSerializer serializer) =>
            throw new NotSupportedException("Writing is handled by the default JSON.NET behaviour");

        /// <inheritdoc />
        /// <summary>Reads the JSON representation of the object.</summary>
        /// <param name="reader">The <see cref="JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">
        /// The existing value of object being read. If there is no existing value then <c>null</c> will be used.
        /// </param>
        /// <param name="hasExistingValue">The existing value has a value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>An appropriate <see cref="Event" /> instance.</returns>
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
            var redacts = jObject.ValueOrDefault<string>("redacts");

            var sender = jObject.ObjectOrDefault<UserId>("sender");
            var id = jObject.ValueOrDefault<string>("event_id");
            var stateKey = jObject.ValueOrDefault<string>("state_key");
            var roomId = jObject.ValueOrDefault<string>("room_id");
            var unsigned = ParseUnsignedData(type, jObject);
            var previousContent = ParseEventContent(type, jObject, "prev_content");

            DateTimeOffset sentAt;

            if (jObject.ContainsKey("origin_server_ts"))
            {
                var tsValue = jObject["origin_server_ts"];
                var ms = tsValue.Value<long>();
                sentAt = DateTimeOffset.FromUnixTimeMilliseconds(ms);
            }
            else
            {
                sentAt = default;
            }

            return new Event(content, id, type, stateKey, roomId, sender, sentAt, previousContent, redacts, unsigned);
        }

        /// <summary>
        /// Parses event content data into an appropriate <see cref="EventContent" /> class,
        /// based on event and event content type.
        /// </summary>
        /// <param name="type">The type of event this content came from.</param>
        /// <param name="jObject">An instance of <see cref="JObject" /> containing the data.</param>
        /// <param name="key">The key in the JSON object to extract data from.</param>
        /// <returns>An appropriate instance of <see cref="EventContent" />.</returns>
        private static EventContent ParseEventContent(string type, JObject jObject, string key = "content")
        {
            if (!jObject.TryGetValue(key, out var contentToken))
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

        /// <summary>
        /// Parses unsigned data from an event, if any.
        /// </summary>
        /// <param name="eventType">The type of the event.</param>
        /// <param name="jObject">The JSON object containing the event.</param>
        /// <param name="key">The name of the key containing unsigned data.</param>
        /// <returns>
        /// An instance of <see cref="UnsignedData" /> if found; otherwise, <c>null</c>.
        /// </returns>
        private static UnsignedData? ParseUnsignedData(string eventType, JObject jObject, string key = "unsigned")
        {
            var hasData = jObject.TryGetValue(key, out var token);

            if (!hasData || !(token is JObject unsignedObject))
            {
                return null;
            }

            var replacesStateEventId = unsignedObject.ValueOrDefault<string>("replaces_state");
            var previousContent = ParseEventContent(eventType, unsignedObject, "prev_content");
            var previousSender = unsignedObject.ObjectOrDefault<UserId>("prev_sender");
            var ageMilliseconds = unsignedObject.ValueOrDefault<long?>("age");
            var age = ageMilliseconds.HasValue ? TimeSpan.FromSeconds(ageMilliseconds.Value) : (TimeSpan?)null;
            var redactionEvent = unsignedObject.ObjectOrDefault<Event>("redacted_because");
            var transactionId = unsignedObject.ValueOrDefault<string>("transaction_id");
            var inviteRoomState = unsignedObject.ObjectOrDefault<IReadOnlyCollection<Event>>("invite_room_state");

            return new UnsignedData(
                replacesStateEventId,
                previousContent,
                previousSender,
                age,
                redactionEvent,
                transactionId,
                inviteRoomState);
        }
    }
}
