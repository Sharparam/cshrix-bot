// Copyright (c) 2019 by Adam Hellberg.
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

/// Contains helper methods for performing reflection tasks.
module Cshrix.Utilities.ReflectionUtils
open System

/// <summary>
/// Checks if a type is not a value type and nullable.
/// </summary>
/// <param name="type">The type to check.</param>
/// <returns><c>true</c> if the type is nullable, otherwise <c>false</c>.</returns>
[<CompiledName("IsNullableType")>]
let isNullableType (t:Type) = t.IsGenericType && t.GetGenericTypeDefinition() = typedefof<Nullable<_>>

/// <summary>
/// Checks if a type (including value types) is nullable.
/// </summary>
/// <param name="type">The type to check.</param>
/// <returns><c>true</c> if the type is nullable, otherwise <c>false</c>.</returns>
[<CompiledName("IsNullable")>]
let isNullable (t:Type) = not t.IsValueType || t |> isNullableType
