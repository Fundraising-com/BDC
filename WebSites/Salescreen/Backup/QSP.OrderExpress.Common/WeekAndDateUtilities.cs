using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;


namespace QSPForm.Common
{
    public class WeekAndDateUtilities
    {
        public static int GetWeekNumber(DateTime date, DayOfWeek weekStartDay)
        {
            int result = 0;

            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            result = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, weekStartDay);

            return result;
        }
        public static DateTime GetWeekStart(int weekNumber, int year, DayOfWeek weekStartDay)
        {
            System.DateTime firstDayDate = DateTime.Parse(String.Format("1/1/{0}", year));

            int intWeekStartDay = (int)weekStartDay;
            int intYearStartDay = (int)firstDayDate.DayOfWeek;

            int daysToFirstFullWeek = (7 - intYearStartDay + intWeekStartDay);
            DateTime firstFullWeekStartDate = firstDayDate.AddDays(daysToFirstFullWeek);

            int weeksToAdd = weekNumber - 1;
            if (intYearStartDay > intWeekStartDay)
            {
                weeksToAdd--;
            }

            DateTime weekStart = firstFullWeekStartDate.AddDays(7 * weeksToAdd);

            return weekStart;
        }
        public static DateTime GetWeekEnd(int weekNumber, int year, DayOfWeek weekStartDay)
        {
            DateTime weekStart = WeekAndDateUtilities.GetWeekStart(weekNumber, year, weekStartDay);
            DateTime weekEnd = weekStart.AddDays(6);

            return weekEnd;
        }
    }
}
