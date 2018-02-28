using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace BreakableToys
{
    [TestFixture]
    public class SortedRotatedArray
    {
        int[] BuildSut(int arrayLength, out int rotation)
        {
            var rand = new Random();
            rotation = rand.Next(0, arrayLength - 1);
            
            var temp = new SortedSet<int>();
            while(temp.Count < arrayLength)
            {
                temp.Add(rand.Next(1, arrayLength * 10));
            }
            var tempAsArray = temp.ToArray();

            var sut = new int[arrayLength];
            for (int i = 0; i < arrayLength; i++)
            {
                sut[i] = tempAsArray[(i + rotation) % arrayLength];
            }

            return sut;
        }

        [Test]
        public void FindElement()
        {
            for (int i = 0; i < 100; i++)
            {
                
            int rotation;
            var sut = BuildSut(1000, out rotation);
            var offset = new Random().Next(0, sut.Length - 1);
            var expectedIndex = sut.Length - 1 - rotation - offset;
            if (expectedIndex < 0)
                expectedIndex = sut.Length + expectedIndex;

            var expected = sut[expectedIndex];
            Console.WriteLine($"Rotation: {rotation}, Offset: {offset}");
            Console.WriteLine($"Array: {string.Join(", ", sut)}");
            Console.WriteLine($"Expected: {expected}");
            var result = FindElementNFromLargest(sut, offset);
            Assert.That(result, Is.EqualTo(expected));
            }
        }

        private int FindElementNFromLargest(int[] sut, int offset)
        {
            var index = FindMinValueIndex(0, sut.Length - 1, sut);
            var indexOfDesired = index - offset - 1;
            if (indexOfDesired < 0)
                indexOfDesired = sut.Length + indexOfDesired;
            return sut[indexOfDesired];
        }

        private int FindMinValueIndex(int left, int right, int[] sut)
        {
            var leftValue = sut[left];
            var rightValue = sut[right];
            var mid = (left + right) >> 1;
            var midValue = sut[mid];

            if (leftValue > midValue)
                return FindMinValueIndex(left, mid, sut);
            if(midValue > rightValue)
                return FindMinValueIndex(mid + 1, right, sut);
            return left;
        }
    }
}
