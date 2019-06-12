// Copyright (c) 2019 by Adam Hellberg.
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace Cshrix.Serialization
open Cshrix.Utilities
open Newtonsoft.Json
open System

/// <inheritdoc />
/// <summary>
/// Converts <see cref="TimeSpan" /> values to/from their millisecond JSON representation.
/// </summary>
type MillisecondTimeSpanConverter() =
    inherit JsonConverter()

    let handleString value =
        let (success, parsed) = Int64.TryParse(value)
        if success then double(parsed)
        else raise(JsonSerializationException("Cannot convert invalid value to long"))

    /// <inheritdoc />
    /// <summary>
    /// Determines whether this instance can convert the specified object type.
    /// </summary>
    /// <param name="objectType">Type of the object.</param>
    /// <returns>
    /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
    /// </returns>
    override this.CanConvert objectType = objectType = typedefof<TimeSpan> || objectType = typedefof<Nullable<TimeSpan>>

    /// <inheritdoc />
    /// <summary>Writes the millisecond JSON representation of the <see cref="TimeSpan" />.</summary>
    /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
    /// <param name="value">The value.</param>
    /// <param name="serializer">The calling serializer.</param>
    override this.WriteJson(writer, value, _) =
        match value with
        | null -> writer.WriteNull()
        | _ -> writer.WriteValue(int64 (value :?> TimeSpan).TotalMilliseconds)

    /// <inheritdoc />
    /// <summary>Reads a <see cref="TimeSpan" /> value from milliseconds represented in JSON.</summary>
    /// <param name="reader">The <see cref="JsonReader" /> to read from.</param>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="existingValue">The existing value of object being read.</param>
    /// <param name="serializer">The calling serializer.</param>
    /// <returns>The <see cref="TimeSpan" /> value.</returns>
    /// <exception cref="JsonSerializationException">
    /// Thrown if the JSON being deserialized is not valid.
    /// </exception>
    override this.ReadJson(reader, objectType, _, _) =
        let nullable = objectType |> ReflectionUtils.isNullable

        match reader.TokenType with
        | JsonToken.Null when nullable -> null
        | JsonToken.Null -> raise(JsonSerializationException(sprintf "Cannot convert null value to %O" objectType))
        | JsonToken.Integer -> double(reader.Value :?> Int64) |> TimeSpan.FromMilliseconds :> obj
        | JsonToken.String -> reader.Value :?> string |> handleString |> TimeSpan.FromMilliseconds :> obj
        | _ -> raise(JsonSerializationException(sprintf "Unexpected token parsing timespan. Expected Integer or String, got %O" reader.TokenType))
