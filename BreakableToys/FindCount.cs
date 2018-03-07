using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;

namespace BreakableToys
{
    [TestFixture]
    public class FindCount
    {
        private static Dictionary<string, int> _stringTranslator;
        private static Dictionary<int, char> _characterTranslator;

        [SetUp]
        public void SetUp()
        {
            _stringTranslator = new Dictionary<string, int>();
            _characterTranslator = new Dictionary<int, char>();

            for (int i = 0; i < 26; i++)
            {
                _stringTranslator[i.ToString()] = i;
                _characterTranslator[i + 1] = (char)('a' + i);
            }
        }

        [TestCaseSource(nameof(GetWordsFromNumbersTestCases))]
        public void GetWordsFromNumbersTest(string input, string[] result)
        {
            GetWordsFromNumbers(input).Should().BeEquivalentTo(result);
        }

        public static IEnumerable<object[]> GetWordsFromNumbersTestCases
        {
            get
            {
                yield return new object[] { "12", new[] { "ab", "l" } };
                yield return new object[] { "23", new[] { "bc", "w" } };
                yield return new object[] { "123", new[] { "abc", "lc", "aw" } };
                yield return new object[] { "1234", new[] { "abcd", "lcd", "awd" } };
            }
        }

        private IEnumerable<string> GetWordsFromNumbers(string input)
        {
            var results = new List<string>();

            var combinations = GetCombinations(input);
            foreach (var combination in combinations)
            {
                var result = ConvertCombinationToString(combination);
                if (!string.IsNullOrEmpty(result))
                    results.Add(result);
            }

            return results;
        }

        private static string ConvertCombinationToString(IEnumerable<string> combination)
        {
            var invalid = false;
            var sb = new StringBuilder();

            foreach (var i in combination)
            {
                int asNumber;
                if (_stringTranslator.TryGetValue(i, out asNumber))
                    sb.Append(_characterTranslator[asNumber]);
                else
                {
                    invalid = true;
                    break;
                }
            }

            return !invalid ? sb.ToString() : null;
        }

        public IEnumerable<IEnumerable<string>> GetCombinations(string str)
        {
            if (str.Length == 1)
                return new[] { new[] { str } };

            var combinations = new List<IEnumerable<string>> { new List<string>() { str } };
            for (int i = 1; i < str.Length; i++)
            {
                var combos = GetCombinations(str.Substring(i));
                foreach (var combo in combos)
                {
                    var innerList = new List<string> { str.Substring(0, i) };
                    innerList.AddRange(combo);
                    combinations.Add(innerList);
                }
            }

            return combinations;
        }
    }
}