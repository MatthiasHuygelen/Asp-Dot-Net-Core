using System;
using Utility;
using Xunit;

namespace XUnitTestProject
{
    public class MonthUtilTest
    {
        [Theory()]
        [InlineData(2020,11)]
        [InlineData(2020,1)]
        [InlineData(2020,5)]
        [InlineData(1999,5)]
        public void GenerateStartEndTest(int year , int month)
        {
            (DateTime start, DateTime end) = MonthUtil.GenerateStartEnd(year, month);
            Assert.Equal((new DateTime(year, month, 1), new DateTime(year, month, DateTime.DaysInMonth(year, month),23,59,59)),(start, end));
        }

        [Fact]
        public void TestSum()
        {
            var expected = 10;
            var result = 5+5;
            Assert.Equal(expected, result);
        }
    }
}
