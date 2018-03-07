using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
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
            var wordsFromNumbers = GetWordsFromNumbers(input);
            wordsFromNumbers.Should().BeEquivalentTo(result);
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

            var permutations = SplitString(input);
            foreach (var combination in permutations)
            {
                var result = ConvertCombinationToString(combination);
                if (!string.IsNullOrEmpty(result))
                    results.Add(result);
            }

            return results;
        }

        private IEnumerable<string[]> SplitString(string input)
        {
            var result = new List<string[]>();
            SplitString(input, new List<string>(), result);
            return result;
        }

        private void SplitString(string input, List<string> list, List<string[]> result)
        {
            if (input.Length == 0)
            {
                result.Add(list.ToArray());
            }
            else
            {
                for (int i = 0; i < input.Length; i++)
                {
                    var left = input.Substring(0, i + 1);
                    list.Add(left);
                    var right = input.Substring(i + 1);
                    SplitString(right, list, result);
                    list.Remove(left);
                }
            }
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

        /// <summary>
        /// This solution uses a sliding window of length 2 to combine values
        /// ie. 1234, it combines 12 then returns 12,3,4
        /// Next it combines 23 and returns 1,23,4
        /// Last it would combine 34 and return 1,2,34
        /// I did this because we know that our max number is 2 digits
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public IEnumerable<IEnumerable<string>> GetCombinationsOfLength2OrLess(string str)
        {
            var charArray = str.ToCharArray().Select(c => c.ToString()).ToList();
            yield return charArray;
            for (int i = 1; i < str.Length; i++)
            {
                var temp = new List<string>();
                for (int j = 0; j < i - 1; j++)
                {
                    temp.Add(charArray[j]);
                }

                temp.Add($"{charArray[i - 1]}{charArray[i]}");
                temp.AddRange(charArray.Skip(i + 1));
                yield return temp;
            }
        }
    }
}