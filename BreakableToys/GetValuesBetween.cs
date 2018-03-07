using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace BreakableToys
{
    [TestFixture]
    public class GetValuesBetween
    {
        private RangeStore Init(int[] values)
        {
            return new RangeStore(values);
        }

        [Test]
        public void GetNumbersBetween()
        {
            var sut = Init(new[] {3, 3, 9, 9, 9, 5, 5, 7, 7, 7});
            var result = sut.Get(3, 9);
            result.Should().Be(5);
        }
    }

    public class IndexStore
    {
        public IndexStore(int index)
        {
            Start = End = index;
        }

        public int Start { get; set; }
        public int End { get; set; }
    }

    internal class RangeStore
    {
        private readonly Dictionary<int, IndexStore> _reverseLookup;

        public RangeStore(int[] values)
        {
            var sorted = new int[values.Length];
            values.CopyTo(sorted, 0);
            _reverseLookup = new Dictionary<int, IndexStore>(values.Length);

            Array.Sort(values);
            for (var index = 0; index < values.Length; index++)
            {
                var value = values[index];
                IndexStore indexStore;
                if (_reverseLookup.TryGetValue(value, out indexStore))
                    indexStore.End = index;
                else
                    _reverseLookup[value] = new IndexStore(index);
            }
        }

        public int Get(int a, int b)
        {
            return _reverseLookup[b].Start - _reverseLookup[a].End - 1;
        }
    }
}