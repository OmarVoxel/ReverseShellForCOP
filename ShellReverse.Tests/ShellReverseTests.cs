using System.Collections.Generic;
using Xunit;

namespace ShellReverse.Tests
{
    public class ShellReverseTests
    {
        public static IEnumerable<object[]> intTest()
        {
            ShellReverse.Execute();

            return new List<object[]>
            {
                new object[] { 1 },
            };
        }

        [Theory]
        [MemberData(nameof(intTest))]
        public void JustTestingStuff(int num)
        {
            Assert.Equal(1, num);
        }
    }
}
