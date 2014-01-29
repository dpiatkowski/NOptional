using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace whiteshore.OptionType.Tests
{
    [TestClass]
    public class NoneTest
    {
        [TestMethod]
        public void BuildViaConstructorIsCorrect()
        {
            var maybe = new None<string>();

            Assert.IsFalse(maybe.HasValue);
        }

        [TestMethod]
        public void BuildViaFactoryMethodIsCorrect()
        {
            var maybe = Option.Of((string) null);

            Assert.IsFalse(maybe.HasValue);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GettingValueFromNoneThrowsAnException()
        {
            var maybe = Option.Of((string)null);

            var value= maybe.Value;
        }
    }
}
