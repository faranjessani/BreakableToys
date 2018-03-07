using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BreakableToys
{
    [TestFixture]
    public class GetPermutations
    {
        [Test]
        public void GetPermutationsOfString()
        {
            var sut = "sagiv";
            var charArray = sut.ToCharArray();
            var perms = GetPerms(charArray, 0, charArray.Length - 1);
        }

        private IEnumerable<string> GetPerms(char[] sut, int start, int end)
        {
            return null;
        }
    }
}
