using System;

namespace Utility
{
    public static class MonthUtil
    {
        public static (DateTime Start, DateTime End) GenerateStartEnd(int year, int month)
        {
            return (new DateTime(year, month, 1), new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59));
        }
    }
}
