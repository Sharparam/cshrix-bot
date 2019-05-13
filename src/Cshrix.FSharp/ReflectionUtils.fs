// Copyright (c) 2019 by Adam Hellberg.
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

module Cshrix.Utilities.ReflectionUtils
open System

let isNullableType (t:Type) = t.IsGenericType && t.GetGenericTypeDefinition() = typedefof<Nullable<_>>

let isNullable (t:Type) = not t.IsValueType || t |> isNullableType
