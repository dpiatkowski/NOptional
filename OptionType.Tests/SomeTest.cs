using whiteshore.OptionType;
using Xunit;

namespace OptionType.Tests
{
    public class SomeTest
    {
        private const string InnerValue = "Lorem ipsum";

        [Fact]
        public void BuildSomeViaConstructor()
        {
            var maybe = new Some<string>(InnerValue);

            Assert.True(maybe.HasValue);
            Assert.Equal(maybe.Value, InnerValue);
        }

        [Fact]
        public void BuildSomeViaFactoryMethod()
        {
            var maybe = Option.Of(InnerValue);

            Assert.True(maybe.HasValue);
            Assert.Equal(maybe.Value, InnerValue);
        }

        [Fact]
        public void ToStringYieldsInnerValue()
        {
            var maybe = Option.Of(InnerValue);

            Assert.Equal(InnerValue, maybe.ToString());
        }
    }
}
