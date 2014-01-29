using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace whiteshore.OptionType.Tests
{
    [TestClass]
    public class LinqToOptionTest
    {
        [TestMethod]
        public void AddingTwoPresentNumberWorks()
        {
            var value = from a in 5.AsOption()
                        from b in 3.AsOption()
                        select a + b;

            Assert.AreEqual(value.Value, 8);
        }

        [TestMethod]
        public void AddingTwoNumberWhereOneIsAbsentYieldsNone()
        {
            var value = from a in 5.AsOption()
                        from b in new None<int>()
                        select a + b;

            Assert.IsFalse(value.HasValue);
        }

        [TestMethod]
        public void LoopingOverSomeWorks()
        {
            const string stringValue = "foo";
            var result = new List<string>();

            foreach(var value in Option.Of(stringValue))
            {
                result.Add(value);
            }

            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0], stringValue);
        }

        [TestMethod]
        public void LoopingOverNoneYieldsEmptySeq()
        {
            var result = new List<string>();
            foreach (var value in Option.None<string>())
            {
                result.Add(value);
            }

            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void ProjectingSomeWorks()
        {
            const string stringValue = "foo";
            Assert.AreEqual(Option.Of(stringValue).Select(x => x).First(), stringValue);
        }

        [TestMethod]
        public void ProjectingNoneYieldsEmptySeq()
        {
            Assert.IsFalse(Option.None<string>().Select(x => x).Any());
        }
    }
}
