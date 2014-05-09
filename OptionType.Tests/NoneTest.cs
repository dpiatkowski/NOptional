using System;
using Xunit;

namespace whiteshore.OptionType.Tests
{
    public class NoneTest
    {
        [Fact]
        public void BuildNoneViaConstructor()
        {
            var maybe = new None<string>();

            Assert.False(maybe.HasValue);
        }

        [Fact]
        public void BuildNoneViaFactoryMethod()
        {
            var maybe = Option.Of((string) null);

            Assert.False(maybe.HasValue);
        }

        [Fact]
        public void GettingValueFromNoneThrowsAnException()
        {
            var maybe = Option.Of((string)null);

            Assert.Throws<InvalidOperationException>(() =>
            {
                var value = maybe.Value;
            });
        }

        [Fact]
        public void NoneToStringEqualsNone()
        {
            var maybe = Option.None<object>();

            Assert.Equal("None", maybe.ToString());
        }
    }
}
