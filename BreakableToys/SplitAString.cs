using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace BreakableToys
{
    [TestFixture]
    public class SplitAString
    {
        private HashSet<string[]> GetSplits(string s)
        {
            var hashSet = new HashSet<string[]>();
            GetSplits(s, new List<string>(), hashSet);
            return hashSet;
        }

        private void GetSplits(string s, List<string> chosen, HashSet<string[]> acc)
        {
            if (s.Length == 0)
                acc.Add(chosen.ToArray());
            else
                for (var i = 0; i < s.Length; i++)
                {
                    var remainder = s.Substring(i + 1);
                    var prefix = s.Substring(0, i + 1);
                    chosen.Add(prefix);
                    GetSplits(remainder, chosen, acc);
                    chosen.Remove(prefix);
                }
        }

        [Test]
        public void SplitString()
        {
            var splitPermutations = GetSplits("ABC");
            splitPermutations.Should().BeEquivalentTo(new List<string[]>
            {
                new[] {"A", "B", "C"},
                new[] {"AB", "C"},
                new[] {"A", "BC"},
                new[] {"ABC"}
            });
        }
    }
}