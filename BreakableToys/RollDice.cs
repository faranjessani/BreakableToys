using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BreakableToys
{
    [TestFixture]
    public class RollDice
    {
        private IEnumerable<List<int>> GetRollDiceResults(int n)
        {
            var result = new List<List<int>>();
            GetRollDiceResults(n, new List<int>(), result);
            return result;
        }

        private void GetRollDiceResults(int n, List<int> list, List<List<int>> result)
        {
            if (n == 0)
                result.Add(new List<int>(list));
            else
                for (var i = 1; i <= 6; i++)
                {
                    list.Add(i);
                    GetRollDiceResults(n - 1, list, result);
                    list.Remove(i);
                }
        }

        [Test]
        public void RollDiceTest()
        {
            for (int i = 1; i <= 6; i++)
            {
                var rolls = GetRollDiceResults(i);
                rolls.Count().Should().Be((int)Math.Pow(6, i));
            }
        }
    }
}