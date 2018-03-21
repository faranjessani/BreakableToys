using FluentAssertions;
using NUnit.Framework;

namespace BreakableToys
{
    [TestFixture]
    public class CountSteps
    {
        public static int CountStepsR(int n, int[] ints)
        {
            if (n < 0)
                return 0;
            if (n == 0)
                return 1;
            
            if (ints[n] > 0)
                return ints[n];

            return ints[n] = CountStepsR(n - 1, ints) + CountStepsR(n - 2, ints) + CountStepsR(n - 3, ints);
        }

        public static int CountStepsDynamically(int n, int[] ints)
        {
            for (var i = 1; i <= n; i++)
            {
                if (i >= 3)
                    ints[i] += ints[i - 3];
                if (i >= 2)
                    ints[i] += ints[i - 2];
                if (i >= 1)
                    ints[i] += ints[i - 1];
            }

            return ints[n];
        }

        [Test]
        [TestCase(3, 4)]
        [TestCase(4, 7)]
        [TestCase(5, 13)]
        public void DoTestsPass(int key, int val)
        {
            var ints = new int[key + 1];
            ints[0] = 1;
            var countSteps = CountStepsR(key, ints);
            countSteps.Should().Be(val);
        }
    }
}