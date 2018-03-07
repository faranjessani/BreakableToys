using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace BreakableToys
{
    [TestFixture]
    public class PrintDecimal
    {
        private IEnumerable<string> GetAllValuesOfLength(int n)
        {
            var result = new HashSet<string>();
            GetAllValuesOfLength(n, string.Empty, result);
            return result;
        }

        private void GetAllValuesOfLength(int i, string prefix, HashSet<string> result)
        {
            if (i == 0)
                result.Add(prefix);
            else
                for (var j = 0; j < 10; j++)
                    GetAllValuesOfLength(i - 1, prefix + j, result);
        }

        [Test]
        public void PrintDecimalTest()
        {
            var n = 3;
            var result = GetAllValuesOfLength(n);
            var format = string.Join(string.Empty, Enumerable.Repeat("0", n));
            result.Should().Contain(Enumerable.Range(0, (int) Math.Pow(10, n)).Select(i => i.ToString(format)));
        }
    }
}