// <copyright file="Helpers.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

[<AutoOpen>]
module Cshrix.FSharp.Tests.Helpers

#nowarn "77"

let inline ( !> ) (b : ^b) : ^a = (^a : (static member op_Explicit : ^b -> ^a) (b))
