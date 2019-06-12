// <copyright file="Base64UtilsTests.fs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

module Cshrix.FSharp.Tests.Utilities.Base64UtilsTests

open Cshrix.Utilities

open FsUnit

open NUnit.Framework

[<TestCase("", "")>]
[<TestCase("f", "Zg==")>]
[<TestCase("fo", "Zm8=")>]
[<TestCase("foo", "Zm9v")>]
[<TestCase("foob", "Zm9vYg==")>]
[<TestCase("fooba", "Zm9vYmE=")>]
[<TestCase("foobar", "Zm9vYmFy")>]
let ``When encoding to regular Base64 it should behave as expected`` (input: string) expected =
    let base64 = Base64Utils.ToBase64(input, false, false)
    base64 |> should equal expected

[<TestCase("", "")>]
[<TestCase("f", "Zg")>]
[<TestCase("fo", "Zm8")>]
[<TestCase("foo", "Zm9v")>]
[<TestCase("foob", "Zm9vYg")>]
[<TestCase("fooba", "Zm9vYmE")>]
[<TestCase("foobar", "Zm9vYmFy")>]
let ``When encoding to unpadded Base64 it should behave as expected`` (input: string) expected =
    let base64 = Base64Utils.ToBase64(input, true, false)
    base64 |> should equal expected

let byteTestCases =
    [
        TestCaseData([| 0uy |], false, false).Returns("AA==")
        TestCaseData([| 0uy; 0uy |], false, false).Returns("AAA=")
        TestCaseData([| 0uy; 0uy; 0uy |], false, false).Returns("AAAA")
        TestCaseData([| 255uy |], false, false).Returns("/w==")
        TestCaseData([| 255uy; 255uy |], false, false).Returns("//8=")
        TestCaseData([| 255uy; 255uy; 255uy |], false, false).Returns("////")
        TestCaseData([| 0xFFuy; 0xEFuy |], false, false).Returns("/+8=")

        TestCaseData([| 0uy |], true, false).Returns("AA")
        TestCaseData([| 0uy; 0uy |], true, false).Returns("AAA")
        TestCaseData([| 0uy; 0uy; 0uy |], true, false).Returns("AAAA")
        TestCaseData([| 255uy |], true, false).Returns("/w")
        TestCaseData([| 255uy; 255uy |], true, false).Returns("//8")
        TestCaseData([| 255uy; 255uy; 255uy |], true, false).Returns("////")
        TestCaseData([| 0xFFuy; 0xEFuy |], true, false).Returns("/+8")

        TestCaseData([| 0uy |], false, true).Returns("AA==")
        TestCaseData([| 0uy; 0uy |], false, true).Returns("AAA=")
        TestCaseData([| 0uy; 0uy; 0uy |], false, true).Returns("AAAA")
        TestCaseData([| 255uy |], false, true).Returns("_w==")
        TestCaseData([| 255uy; 255uy |], false, true).Returns("__8=")
        TestCaseData([| 255uy; 255uy; 255uy |], false, true).Returns("____")
        TestCaseData([| 0xFFuy; 0xEFuy |], false, true).Returns("_-8=")

        TestCaseData([| 0uy |], true, true).Returns("AA")
        TestCaseData([| 0uy; 0uy |], true, true).Returns("AAA")
        TestCaseData([| 0uy; 0uy; 0uy |], true, true).Returns("AAAA")
        TestCaseData([| 255uy |], true, true).Returns("_w")
        TestCaseData([| 255uy; 255uy |], true, true).Returns("__8")
        TestCaseData([| 255uy; 255uy; 255uy |], true, true).Returns("____")
        TestCaseData([| 0xFFuy; 0xEFuy |], true, true).Returns("_-8")
    ]

[<TestCaseSource("byteTestCases")>]
let ``When encoding bytes to Base64 it should behave as expected`` (input: byte[]) unpad urlsafe =
    Base64Utils.ToBase64(input, unpad, urlsafe)
