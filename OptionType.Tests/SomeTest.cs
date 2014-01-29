using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using whiteshore.OptionType;

namespace OptionType.Tests
{
    [TestClass]
    public class SomeTest
    {
        private const string InnerValue = "Lorem ipsum";

        [TestMethod]
        public void BuildViaConstructorIsCorrect()
        {
            var maybe = new Some<string>(InnerValue);

            Assert.IsTrue(maybe.HasValue);
            Assert.AreEqual(maybe.Value, InnerValue);
        }

        [TestMethod]
        public void BuildViaFactoryMethodIsCorrect()
        {
            var maybe = Option.Of(InnerValue);

            Assert.IsTrue(maybe.HasValue);
            Assert.AreEqual(maybe.Value, InnerValue);
        }
    }
}
