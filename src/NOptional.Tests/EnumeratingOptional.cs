using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NOptional.Tests
{
    public class EnumeratingOptional
    {
        [Fact]
        public void LoopingOverOptional()
        {
            const string stringValue = "foo";
            var result = new List<string>();

            foreach (var value in Optional<string>.Of(stringValue))
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

            foreach (var value in Optional<string>.Empty)
            {
                result.Add(value);
            }

            Assert.Equal(result.Count, 0);
        }

        [Fact]
        public void ProjectingOptional()
        {
            const string stringValue = "foo";

            Assert.Equal(Optional<string>.Of(stringValue).Select(x => x).First(), stringValue);
        }

        [Fact]
        public void ProjectingNoneYieldsEmptySeq()
        {
            Assert.False(Optional<string>.Empty.Select(x => x).Any());
        }

        [Fact]
        public void QuerySyntaxYieldsValues()
        {
            const string someString = "Hello world";

            var optional = Optional<string>.Of(someString);

            var value = from s in optional select s;

            Assert.Equal(value.First(), someString);
        }
    }
}
