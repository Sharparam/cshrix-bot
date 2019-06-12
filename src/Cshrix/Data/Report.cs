// <copyright file="Report.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about a message report.
    /// </summary>
    public readonly struct Report
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Report" /> structure.
        /// </summary>
        /// <param name="reason">The reason for the report.</param>
        /// <param name="score">The score to apply to the message.</param>
        [JsonConstructor]
        public Report(string reason, int score)
            : this()
        {
            Reason = reason;
            Score = score;
        }

        /// <summary>
        /// Gets the reason this report was made.
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; }

        /// <summary>
        /// Gets the user-provided score for this report.
        /// </summary>
        /// <remarks>
        /// <c>-100</c> is the lowest score, and <c>100</c> the highest.
        /// </remarks>
        [JsonProperty("score")]
        public int Score { get; }
    }
}
