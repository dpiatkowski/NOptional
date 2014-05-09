using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace whiteshore.OptionType.Tests
{
    public class LinqToOptionTest
    {
        [Fact]
        public void AddingTwoPresentNumberWorks()
        {
            var value = from a in 5.AsOption()
                        from b in 3.AsOption()
                        select a + b;

            Assert.Equal(value.Value, 8);
        }

        [Fact]
        public void AddingTwoNumberWhereOneIsAbsentYieldsNone()
        {
            var value = from a in 5.AsOption()
                        from b in new None<int>()
                        select a + b;

            Assert.False(value.HasValue);
        }

        [Fact]
        public void LoopingOverSomeWorks()
        {
            const string stringValue = "foo";
            var result = new List<string>();

            foreach(var value in Option.Of(stringValue))
            {
                result.Add(value);
            }

            Assert.Equal(result.Count, 1);
            Assert.Equal(result[0], stringValue);
        }

        [Fact]
        public void LoopingOverNoneYieldsEmptySeq()
        {
            var result = new List<string>();
            foreach (var value in Option.None<string>())
            {
                result.Add(value);
            }

            Assert.Equal(result.Count, 0);
        }

        [Fact]
        public void ProjectingSomeWorks()
        {
            const string stringValue = "foo";
            Assert.Equal(Option.Of(stringValue).Select(x => x).First(), stringValue);
        }

        [Fact]
        public void ProjectingNoneYieldsEmptySeq()
        {
            Assert.False(Option.None<string>().Select(x => x).Any());
        }
    }
}
