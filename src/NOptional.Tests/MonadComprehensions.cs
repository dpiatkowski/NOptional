using System.Linq;
using Xunit;

namespace NOptional.Tests
{
    public class MonadComprehensions
    {
        [Fact]
        public void SimpleMonadComprehension()
        {
            var hello = "hello".AsOption();
            var world = "world".AsOption();

            var helloWorld = from h in hello
                             from w in world
                             select h + " " + w;

            Assert.Equal("hello world", helloWorld.First());
        }

        [Fact]
        public void ComposeWithSome()
        {
            var hello = "hello".AsOption();

            var helloWorld = from hh in Replicate(hello) select hh;

            Assert.Equal("hellohello", helloWorld.First());
        }

        [Fact]
        public void ComposeWithNone()
        {
            var none = Optional<string>.Empty;

            var helloWorld = from hh in Replicate(none) select hh;

            Assert.False(helloWorld.Any());
        }

        private Optional<string> Replicate(Optional<string> optionalString)
        {
            if (optionalString.IsPresent)
            {
                var value = optionalString.Get();

                return Optional<string>.Of(value + value);
            }

            return Optional<string>.Empty;
        }
    }
}
