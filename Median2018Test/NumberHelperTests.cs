using Median2018;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Median2018Test
{
    [TestFixture]
    public class Tests
    {      

        [Test]
        public void GetNumberFromString_should_return_equal_value()
        {
            string number = "..12";
            long expected = 12;
            Assert.AreEqual(expected, NumberHelper.GetNumberFromString(number));
        }
        [Test]
        public void GetNumberFromString_should_throw_FormatException()
        {
            Assert.Throws<FormatException>(() => NumberHelper.GetNumberFromString("..;f"));
        }

        [Test]
        public void GetStringFromNumber_should_return_equal_string()
        {
            int number = 123;
            string expected = "123\n";
            Assert.AreEqual(expected, NumberHelper.GetStringFromNumber(number));
        }
        [Test]
        public void GetMedian_should_return_equal_value()
        {
            long expected = 7;
            var numbers = new long[] {12, 2, 11, 3, 7, 10, 3};
            
            Assert.AreEqual(expected, NumberHelper.GetMedian(numbers));
        }

    }
}