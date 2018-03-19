using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace BreakableToys
{
    [TestFixture]
    public class LookAndSay
    {
        [Test]
        [TestCaseSource(nameof(LookAndSayTestCases))]
        public void LookAndSayTest(int generation, string expected)
        {
            GetLookAndSaySequence(generation).Should().BeEquivalentTo(expected);
        }

        private string GetLookAndSaySequence(int generation)
        {
            return generation == 1 ? "1" : Encode(GetLookAndSaySequence(generation - 1));
        }

        private string Encode(string s)
        {
            var sb = new StringBuilder();
            char current = s[0];
            int count = 1;
            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] == current)
                {
                    count++;
                }
                else
                {
                    sb.Append($"{count}{current}");
                    current = s[i];
                    count = 1;
                }
            }

            sb.Append($"{count}{current}");
            return sb.ToString();
        }

        static IEnumerable<object[]> LookAndSayTestCases
        {
            get
            {
                yield return new object[] {7, "13112221"};
                yield return new object[] {8, "1113213211"};
                yield return new object[] {10, "13211311123113112211"};
            }
        }
    }
}