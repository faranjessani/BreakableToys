using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace BreakableToys
{
    [TestFixture]
    public class OneEdit
    {
        /// <summary>
        /// Write a function to return if two words are exactly "one edit" away, where an edit is:
        /// - Inserting one character anywhere in the word (including at the beginning and end)
        /// - Removing one character
        /// - Replacing exactly one character
        /// </summary>
        [Test]
        [TestCaseSource(nameof(OneEditTestCases))]
        public void OneEditAway(string word1, string word2, bool expected)
        {
            IsOneEditAway(word1, word2).Should().Be(expected);
        }

        private bool IsOneEditAway(string word1, string word2)
        {
            int numDifferences = 0;

            int i = 0;
            int j = 0;
            while(i < word1.Length || j < word2.Length)
            {
                if (numDifferences > 1)
                    return false;
                    
                if (i >= word1.Length)
                {
                    numDifferences++;
                    j++;
                }
                else if (j >= word2.Length)
                {
                    numDifferences++;
                    i++;
                }
                else if (word1[i] != word2[j])
                {
                    numDifferences++;
                    
                    if (word1.Length > word2.Length)
                        i++;
                    else if (word2.Length > word1.Length)
                        j++;
                    else
                    {
                        i++;
                        j++;
                    }
                }
                else
                {
                    i++;
                    j++;
                }
            }

            return numDifferences == 1;
        }

        static IEnumerable<object[]> OneEditTestCases
        {
            get
            {
                yield return new object[] {"geek", "geeks", true};
                yield return new object[] {"geek", "beek", true};
                yield return new object[] {"geek", "bleak", false};
                yield return new object[] {"geek", "ageek", true};
                yield return new object[] {"geek", "peaks", false};
                yield return new object[] {"apples", "oranges", false};
            }
        }
    }
}