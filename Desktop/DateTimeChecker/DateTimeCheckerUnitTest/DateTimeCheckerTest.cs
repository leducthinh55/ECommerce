using System;
using DateTimeCheckerUtils;
using NUnit.Framework;

namespace DateTimeCheckerUnitTest
{
    [TestFixture]
    public class DateTimeCheckerTest
    {
        [TestCase]
        public void TestIsValidDate01()
        {
            int Day = 29;
            int Month = 2;
            int Year = 2008;
            bool Result = Utils.IsValidDate(Day, Month, Year);
            bool Expected = true;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestIsValidDate02()
        {
            int Day = 35;
            int Month = 5;
            int Year = 2021;
            bool Result = Utils.IsValidDate(Day, Month, Year);
            bool Expected = false;
            NUnit.Framework.Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestIsValidDate03()
        {
            int Day = 31;
            int Month = 17;
            int Year = 2020;
            bool Result = Utils.IsValidDate(Day, Month, Year);
            bool Expected = false;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestIsValidDate04()
        {
            int Day = 29;
            int Month = 2;
            int Year = 2010;
            bool Result = Utils.IsValidDate(Day, Month, Year);
            bool Expected = false;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestIsValidDate05()
        {
            int Day = 29;
            int Month = 2;
            int Year = 2008;
            bool Result = Utils.IsValidDate(Day, Month, Year);
            bool Expected = true;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestIsValidDate06()
        {
            int Day = 30;
            int Month = 1;
            int Year = 2000;
            bool Result = Utils.IsValidDate(Day, Month, Year);
            bool Expected = true;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestIsValidDate07()
        {
            int Day = 29;
            int Month = 2;
            int Year = 2000;
            bool Result = Utils.IsValidDate(Day, Month, Year);
            bool Expected = true;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestIsValidDate08()
        {
            int Day = 31;
            int Month = 3;
            int Year = 2019;
            bool Result = Utils.IsValidDate(Day, Month, Year);
            bool Expected = true;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestIsValidDate09()
        {
            int Day = 30;
            int Month = 9;
            int Year = 2000;
            bool Result = Utils.IsValidDate(Day, Month, Year);
            bool Expected = true;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestIsValidDate10()
        {
            int Day = 31;
            int Month = 11;
            int Year = 2010;
            bool Result = Utils.IsValidDate(Day, Month, Year);
            bool Expected = false;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestIsValidDate11()
        {
            int Day = 31;
            int Month = 6;
            int Year = 2008;
            bool Result = Utils.IsValidDate(Day, Month, Year);
            bool Expected = false;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestIsValidDate12()
        {
            int Day = 31;
            int Month = 7;
            int Year = 2019;
            bool Result = Utils.IsValidDate(Day, Month, Year);
            bool Expected = true;
            Assert.AreEqual(Expected, Result, "fail");
        }
        [TestCase]
        public void TestDayInMonth01()
        {
            int Month = 1;
            int Year = 2008;
            int Result = Utils.DayInMonth(Month, Year);
            int Expected = 31;
            Assert.AreEqual(Expected, Result, "fail");
        }
        [TestCase]
        public void TestDayInMonth02()
        {
            int Month = 2;
            int Year = 2000;
            int Result = Utils.DayInMonth(Month, Year);
            int Expected = 29;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestDayInMonth03()
        {
            int Month = 2;
            int Year = 2200;
            int Result = Utils.DayInMonth(Month, Year);
            int Expected = 28;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestDayInMonth04()
        {
            int Month = 5;
            int Year = 2000;
            int Result = Utils.DayInMonth(Month, Year);
            int Expected = 31;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestDayInMonth05()
        {
            int Month = 7;
            int Year = 2001;
            int Result = Utils.DayInMonth(Month, Year);
            int Expected = 31;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestDayInMonth06()
        {
            int Month = 4;
            int Year = 2000;
            int Result = Utils.DayInMonth(Month, Year);
            int Expected = 30;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestDayInMonth07()
        {
            int Month = 9;
            int Year = 2200;
            int Result = Utils.DayInMonth(Month, Year);
            int Expected = 30;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestDayInMonth08()
        {
            int Month = 6;
            int Year = 2001;
            int Result = Utils.DayInMonth(Month, Year);
            int Expected = 30;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestDayInMonth09()
        {
            int Month = 2;
            int Year = 2001;
            int Result = Utils.DayInMonth(Month, Year);
            int Expected = 28;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestDayInMonth10()
        {
            int Month = 11;
            int Year = 2008;
            int Result = Utils.DayInMonth(Month, Year);
            int Expected = 30;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestDayInMonth12()
        {
            int Month = 12;
            int Year = 2200;
            int Result = Utils.DayInMonth(Month, Year);
            int Expected = 31;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestDayInMonth13()
        {
            int Month = 2;
            int Year = 2008;
            int Result = Utils.DayInMonth(Month, Year);
            int Expected = 29;
            Assert.AreEqual(Expected, Result, "fail");
        }

        [TestCase]
        public void TestDayInMonth14()
        {
            int Month = 10;
            int Year = 2200;
            int Result = Utils.DayInMonth(Month, Year);
            int Expected = 31;
            Assert.AreEqual(Expected, Result, "fail");
        }
    }
}
