using Xunit;

namespace whiteshore.OptionType.Tests
{
    public class LinqToNullablesTests
    {
        [Fact]
        public void AddingTwoPresentNumberWorks()
        {
            int? x = 5;
            int? y = 3;

            var value = from a in x
                        from b in y
                        select a + b;

            Assert.Equal(value.Value, 8);
        }

        [Fact]
        public void AddingTwoNumberWhereOneIsAbsentYieldsNone()
        {
            int? x = 2;
            int? y = null;

            var value = from a in x
                        from b in y
                        select a + b;

            Assert.False(value.HasValue);
        }
    }
}
