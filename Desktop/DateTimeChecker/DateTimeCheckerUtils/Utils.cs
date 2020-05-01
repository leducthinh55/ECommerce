using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeCheckerUtils
{
    public static class Utils
    {
        private static int[] month28_29 = new int[] {2};
        private static int[] month30 = new int[] { 4, 6, 9, 11 };
        private static int[] month31 = new int[] { 1, 3, 5, 7, 8, 10, 12 };
        public static int DayInMonth(int month, int year)
        {
            if (month30.Contains(month))
            {
                return 30;
            }
            else if (month31.Contains(month))
            {
                return 31;
            }
            else if (month28_29.Contains(month))
            {
                if (year % 4 == 0 && year % 100 != 0)
                {
                    return 29;
                }
                else if (year % 400 == 0)
                {
                    return 29;
                }
                else if (year % 100 == 0)
                {
                    return 28;
                }
                else return 28;
            }
            else return -1;
        }
        public static bool IsValidDate(int day, int month, int year)
        {
            if (day > 31 || day < 1) return false;
            if (DayInMonth(month, year) < 0) return false;
            if (day > DayInMonth(month, year)) return false;
            return true;
        }
    }
}
