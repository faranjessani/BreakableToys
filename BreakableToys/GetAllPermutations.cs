using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace BreakableToys
{
    [TestFixture]
    public class GetAllPermutations
    {
        private HashSet<string> GetPermutations(string s)
        {
            var hashSet = new HashSet<string>();
            GetPermutations(s, string.Empty, hashSet);
            return hashSet;
        }

        private void GetPermutations(string s, string chosen, HashSet<string> acc)
        {
            if (s.Length == 0)
                acc.Add(chosen);
            else
                for (var i = 0; i < s.Length; i++)
                {
                    var c = s[i];
                    GetPermutations(s.Remove(i, 1), chosen + c, acc);
                }
        }

        public static IEnumerable<object[]> PermutationTestCases
        {
            get
            {
                yield return new object[] {"ok", new[] {"ok", "ko"}};
                yield return new object[] {"abc", new[] {"abc", "acb", "bac", "bca", "cab", "cba"}};
            }
        }

        [Test]
        [TestCaseSource(nameof(PermutationTestCases))]
        public void PermuteAString(string input, IEnumerable<string> expected)
        {
            var permutations = GetPermutations(input);
            permutations.Should().BeEquivalentTo(expected);
        }
    }
}