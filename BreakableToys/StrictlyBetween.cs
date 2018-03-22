using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace BreakableToys
{
    [TestFixture]
    public class StrictlyBetween
    {
        public static IEnumerable<object[]> TestCases
        {
            get
            {
                yield return new object[] {new[] {0, 3, 3, 7, 5, 3, 11, 1}, 0};
                yield return new object[] {new int[0], -2};
                yield return new object[] {new[] {5}, -2};
            }
        }

        public int Solution(int[] A)
        {
            Array.Sort(A);
            var minDistance = int.MaxValue;
            for (var i = 1; i < A.Length; i++)
            {
                var distance = Math.Abs(A[i] - A[i - 1]);
                if (distance < minDistance)
                    minDistance = distance;
            }

            if (minDistance == int.MaxValue)
                return -2;
            if (minDistance > 100000000)
                return -1;

            return minDistance;
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void GetValuesStrictlyBetween(int[] testCase, int expected)
        {
            Solution(testCase).Should().Be(expected);
        }
    }
}