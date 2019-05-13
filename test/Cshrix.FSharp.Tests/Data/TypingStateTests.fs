module Cshrix.FSharp.Tests.Data.TypingStateTests

open FsUnit

open System
open Cshrix.Data
open NUnit.Framework
open Newtonsoft.Json

[<Test>]
let ``Should serialize``() =
    let ts = { Timeout = TimeSpan.FromSeconds 1.0; IsTyping = true }
    let json = ts |> JsonConvert.SerializeObject
    json |> should equal """{"timeout":1000,"typing":true}"""

[<Test>]
let ``Should deserialize``() =
    let json = """
    {
        "timeout": 5000,
        "typing": true
    }
    """

    let ts = json |> JsonConvert.DeserializeObject<TypingState>
    let { Timeout = timeout; IsTyping = it } = ts

    timeout |> should equal (TimeSpan.FromSeconds 5.0)
    it |> should be True

[<Test>]
let ``States with identical properties should equal each other``() =
    let a = { Timeout = TimeSpan.FromSeconds 4.0; IsTyping = true }
    let b = { Timeout = TimeSpan.FromSeconds 4.0; IsTyping = true }
    a |> should equal b
