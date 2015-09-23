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

            var helloWorld = hello.Select(h => Optional<string>.Of(h + "world"));

            Assert.Equal("helloworld", helloWorld.Get());
        }

        [Fact]
        public void ComposeWithNone()
        {
            var none = Optional<string>.Empty;

            var helloWorld = none.Select(h => Optional<string>.Of(h + "world"));

            Assert.False(helloWorld.IsPresent);
        }
    }
}
