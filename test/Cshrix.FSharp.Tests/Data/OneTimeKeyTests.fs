// <copyright file="OneTimeKeyTests.fs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

module Cshrix.FSharp.Tests.Data.OneTimeKeyTests

open Cshrix.Data
open Cshrix.FSharp.Tests.Helpers

open FsUnit

open NUnit.Framework

open Newtonsoft.Json

[<Test>]
let ``When OTK is unsigned it should serialize to string``() =
    let otk = OneTimeKey("foobar")
    let json = JsonConvert.SerializeObject(otk)
    json |> should equal "\"foobar\""

[<Test>]
let ``When OTK is signed it should serialize to an object``() =
    let userData = [ ("fizz", "buzz") ] |> dict
    let signatures = [ (!>"@sharparam:sharparam.com", userData) ] |> dict
    let otk = OneTimeKey("foobar", signatures)
    let json = JsonConvert.SerializeObject(otk)
    let expectedJson = """{"key":"foobar","signatures":{"@sharparam:sharparam.com":{"fizz":"buzz"}}}"""

    json |> should equal expectedJson
