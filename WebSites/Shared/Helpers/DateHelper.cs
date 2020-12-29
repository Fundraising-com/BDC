using System;

namespace GA.BDC.Shared.Helpers
{
    public static class DateHelper
    {
        public static DateTime? PrepareStartDate(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day);
            }
            return null;
        }
        public static DateTime? PrepareEndDate(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day, 23, 59, 59);
            }
            return null;
        }

        public static DateTime PrepareStartDate(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
        }
        public static DateTime PrepareEndDate(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
        }
    }
}
