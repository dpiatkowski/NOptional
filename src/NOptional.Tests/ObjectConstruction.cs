using System;
using Xunit;

namespace NOptional.Tests
{
    public class ObjectConstruction
    {
        private const string TestValue = "Hello world";

        [Fact]
        public void EmptyFactoryReturnedNone()
        {
            var none = Optional<object>.Empty;

            Assert.False(none.IsPresent);
        }

        [Fact]
        public void NoneByExtensionMethod()
        {
            string someValue = null;

            var optional = someValue.AsOption();

            Assert.False(optional.IsPresent);
        }

        [Fact]
        public void OfFromValue()
        {
            var optional = Optional<string>.Of(TestValue);

            Assert.True(optional.IsPresent);
        }

        [Fact]
        public void SomeByExtensionMethod()
        {
            var optional = TestValue.AsOption();

            Assert.True(optional.IsPresent);
        }

        [Fact]
        public void OfNullableFromNull()
        {
            var none = Optional<string>.OfNullable(null);

            Assert.False(none.IsPresent);
        }

        [Fact]
        public void OfNullableFromValue()
        {
            var optional = Optional<string>.OfNullable(TestValue);

            Assert.True(optional.IsPresent);
        }

        [Fact]
        public void OfFromNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var optional = Optional<string>.Of(null);
            });
        }
    }
}
