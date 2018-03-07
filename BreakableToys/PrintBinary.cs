using System;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BreakableToys
{
    [TestFixture]
    public class PrintBinary
    {
        [Test]
        [TestCaseSource(nameof(PrintBinaryOfSizeNTestCases))]
        public void PrintBinaryOfSizeN(int n, string[] results)
        {
            var result = GetAllBinaryValuesOfLength(n);
            result.Should().BeEquivalentTo(results);
        }

        private IEnumerable<string> GetAllBinaryValuesOfLength(int n)
        {
            var result = new HashSet<string>();
            GetAllBinaryValuesOfLength(n, string.Empty, result);
            return result;
        }

        private void GetAllBinaryValuesOfLength(int i, string prefix, HashSet<string> result)
        {
            if (i == 0)
            {
                result.Add(prefix);
            }
            else
            {
                GetAllBinaryValuesOfLength(i - 1, prefix + "0", result);
                GetAllBinaryValuesOfLength(i - 1, prefix + "1", result);
            }
        }

        public static IEnumerable<object[]> PrintBinaryOfSizeNTestCases
        {
            get
            {
                yield return new object []{1, new[] {"0", "1"}};
                yield return new object []{2, new[] {"00", "01", "10", "11"}};
            }
        }
    }
}
