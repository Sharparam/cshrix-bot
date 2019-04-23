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

    public readonly struct ComparisonCondition<T> where T : IEquatable<T>, IComparable<T>
    {
        private static readonly IReadOnlyDictionary<string, Func<T, T, bool>> Predicates =
            new Dictionary<string, Func<T, T, bool>>
            {
                ["=="] = (a, b) => a.Equals(b),
                ["<"] = (a, b) => a.CompareTo(b) < 0,
                [">"] = (a, b) => a.CompareTo(b) > 0,
                [">="] = (a, b) => a.CompareTo(b) >= 0,
                ["<="] = (a, b) => a.CompareTo(b) <= 0
            };

        private readonly string _operator;

        private readonly Func<T, T, bool> _predicate;

        public ComparisonCondition(T operand, string op)
            : this()
        {
            _operator = op;
            _predicate = Predicates[op];
            Operand = operand;
        }

        public T Operand { get; }

        [Pure]
        public bool IsValid(T value) => !ReferenceEquals(null, value) && _predicate(value, Operand);

        public override string ToString() => $"{_operator}{Operand}";
    }
}
