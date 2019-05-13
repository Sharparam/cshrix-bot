// Copyright (c) 2019 by Adam Hellberg.
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace Cshrix.Data
open Cshrix.Serialization
open Newtonsoft.Json
open System

/// Represents the typing state of a user.
type TypingState =
    {
        /// Specifies for which duration the user will be typing.
        [<JsonProperty("timeout")>]
        [<JsonConverter(typeof<MillisecondTimeSpanConverter>)>]
        Timeout: TimeSpan

        /// Indicates whether the user is currently typing.
        [<JsonProperty("typing")>]
        IsTyping: bool
    }
