// <copyright file="ComparisonCondition.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Notifications
{
    using System;
    using System.Collections.Generic;

    using JetBrains.Annotations;

    /// <summary>
    /// A comparison condition used for push notification rules.
    /// </summary>
    /// <typeparam name="T">The type of value being compared.</typeparam>
    public readonly struct ComparisonCondition<T> where T : IEquatable<T>, IComparable<T>
    {
        /// <summary>
        /// Contains a mapping between string comparison operators and their comparator functions.
        /// </summary>
        private static readonly IReadOnlyDictionary<string, Func<T, T, bool>> Predicates =
            new Dictionary<string, Func<T, T, bool>>
            {
                ["=="] = (a, b) => a.Equals(b),
                ["<"] = (a, b) => a.CompareTo(b) < 0,
                [">"] = (a, b) => a.CompareTo(b) > 0,
                [">="] = (a, b) => a.CompareTo(b) >= 0,
                ["<="] = (a, b) => a.CompareTo(b) <= 0
            };

        /// <summary>
        /// The original string operator this comparison condition was created from.
        /// </summary>
        private readonly string _operator;

        /// <summary>
        /// The predicate function that is used to compare values.
        /// </summary>
        private readonly Func<T, T, bool> _predicate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparisonCondition{T}" /> structure.
        /// </summary>
        /// <param name="operand">The operand to compare values against.</param>
        /// <param name="op">The operator to use when comparing.</param>
        public ComparisonCondition(T operand, string op)
            : this()
        {
            _operator = op;
            _predicate = Predicates[op];
            Operand = operand;
        }

        /// <summary>
        /// Gets the operand (right-hand side) that values will be compared to.
        /// </summary>
        [PublicAPI]
        public T Operand { get; }

        /// <summary>
        /// Checks if a value passes the comparison check defined by this condition.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="value" /> is not <c>null</c> and passes the defined predicate;
        /// otherwise, <c>false</c>.
        /// </returns>
        [Pure]
        public bool IsValid(T value) => !ReferenceEquals(null, value) && _predicate(value, Operand);

        /// <inheritdoc />
        /// <summary>
        /// Returns a string representation of this comparison condition.
        /// </summary>
        /// <returns>The string representation of this condition.</returns>
        public override string ToString() => $"{_operator}{Operand}";
    }
}
