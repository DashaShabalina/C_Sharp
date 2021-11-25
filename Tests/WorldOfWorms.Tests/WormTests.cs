using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;

namespace WorldOfWorms.Tests
{
    [TestFixture]
    public class WormTests
    {
        [TestCase(2)]
        [TestCase(100)]
        public void ShouldBeUniqueNames(int countWorms)
        {
            List<string> listName = new();
            for (int i = 0; i < countWorms; i++)
            {
                listName.Add(new Worm().Name);
            }
            listName.Should().OnlyHaveUniqueItems();
        }
    }
}