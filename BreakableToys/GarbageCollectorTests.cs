using System;
using NUnit.Framework;

namespace Challenges
{
    [TestFixture]
    public class GarbageCollectorTests
    {
        private const long MaxGarbage = 1000;

        private void MakeSomeGarbage()
        {
            Version vt;

            for (var i = 0; i < MaxGarbage; i++)
                vt = new Version();
        }

        [Test]
        public void GarbageCollectionSample()
        {
            var myGcCol = new GarbageCollectorTests();

            // Determine the maximum number of generations the system
            // garbage collector currently supports.
            Console.WriteLine("The highest generation is {0}", GC.MaxGeneration);

            myGcCol.MakeSomeGarbage();

            // Determine which generation myGCCol object is stored in.
            Console.WriteLine("Generation for myGcCol: {0}", GC.GetGeneration(myGcCol));

            // Determine the best available approximation of the number 
            // of bytes currently allocated in managed memory.
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));

            Console.WriteLine("Collecting GC Gen 0");
            // Perform a collection of generation 0 only.
            GC.Collect(0, GCCollectionMode.Forced);

            // Determine which generation myGCCol object is stored in.
            Console.WriteLine("Generation for myGcCol: {0}", GC.GetGeneration(myGcCol));

            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));

            // Perform a collection of all generations up to and including 2.
            GC.Collect(2);

            // Determine which generation myGCCol object is stored in.
            Console.WriteLine("Generation for myGcCol: {0}", GC.GetGeneration(myGcCol));
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));

            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                Console.WriteLine($"Total GC {i}: {GC.CollectionCount(i)}");
            }
        }
    }
}