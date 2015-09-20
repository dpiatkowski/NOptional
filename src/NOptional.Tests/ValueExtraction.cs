using System;
using System.Threading.Tasks;
using Xunit;

namespace NOptional.Tests
{
    public class ValueExtraction
    {
        private const string TestValue = "Hello world";
        private const string FallbackValue = "foo bar";

        [Fact]
        public void GetFromSome()
        {
            var optional = Optional<string>.Of(TestValue);

            Assert.Equal(TestValue, optional.Get());
        }

        [Fact]
        public void GetOrElseFromSome()
        {
            var optional = Optional<string>.Of(TestValue);

            Assert.Equal(TestValue, optional.OrElseGet(() => FallbackValue));
        }

        [Fact]
        public async Task GetOrElseAsyncFromSome()
        {
            var optional = Optional<string>.Of(TestValue);

            var value = await optional.OrElseGetAsync(async () =>
            {
                return await AsyncString();
            });

            Assert.Equal(TestValue, value);
        }

        [Fact]
        public void GetOrElseThrowFromSome()
        {
            var optional = Optional<string>.Of(TestValue);

            Assert.Equal(TestValue, optional.OrElseThrow(() => new Exception()));
        }

        [Fact]
        public void GetFromNoneThrows()
        {
            var none = Optional<string>.Empty;

            Assert.Throws<InvalidOperationException>(() =>
            {
                var value = none.Get();
            });
        }

        [Fact]
        public void GetOrElseFromNone()
        {
            var none = Optional<string>.Empty;

            Assert.Equal(FallbackValue, none.OrElseGet(() => FallbackValue));
        }

        [Fact]
        public async Task GetOrElseAsyncFromNone()
        {
            var none = Optional<string>.Empty;

            var value = await none.OrElseGetAsync(async () =>
            {
                return await AsyncString();
            });

            Assert.Equal(FallbackValue, value);
        }

        [Fact]
        public void GetOrElseThrowFromNoneThrow()
        {
            var none = Optional<string>.Empty;

            Assert.Throws<Exception>(() =>
            {
                var value = none.OrElseThrow(() => new Exception());
            });
        }

        private Task<string> AsyncString()
        {
            return Task.FromResult(FallbackValue);
        }
    }
}
