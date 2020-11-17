using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UitgaveBeheer.Util
{
    public static class DateUtil
    {
        public static (DateTime Start, DateTime End) GenerateMonthDateTimes(int year, int month)
        {
            return (new DateTime(year, month, 1), new DateTime(year, month, DateTime.DaysInMonth(year, month)));
        }
    }
}
