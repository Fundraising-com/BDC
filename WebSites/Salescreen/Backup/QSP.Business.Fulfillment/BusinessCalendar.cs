using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Expression;
using NHibernate.Mapping.Attributes;

namespace QSP.Business.Fulfillment
{
	public partial class BusinessCalendar
    {

        #region Methods

        public static int GetNaturalDaysFromBusinessDays(int businessDays, DateTime startDate)
        {
            // This is what we want
            // select top 1 business_date from (select top @pBusinessDays * from business_calendar where business_date > @pStartDate and weekend = 0 and holiday = 0) as tableWithBusinessDates order by business_date desc

            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(BusinessCalendar));
                c.Add(Expression.Gt(BusinessDateProperty, startDate));
                c.Add(Expression.Eq(WeekendProperty, false));
                c.Add(Expression.Eq(HolidayProperty, false));
                c.SetMaxResults(businessDays);

                List<BusinessCalendar> deliveryDateList = (List<BusinessCalendar>)c.List<BusinessCalendar>();
                BusinessCalendar deliveryDate = deliveryDateList[deliveryDateList.Count - 1];

                TimeSpan ts = new TimeSpan(
                    new DateTime(deliveryDate.BusinessDate.Year, deliveryDate.BusinessDate.Month, deliveryDate.BusinessDate.Day, 0, 0, 0).Ticks
                    -
                    new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0).Ticks);

                return ts.Days;
            }
        }

        public static int GetBusinessDaysConsideringHolidays(int businessDays, DateTime startDate)
        {
            // This is what we want
            // select top 1 business_date from (select top @pBusinessDays * from business_calendar where business_date > @pStartDate and weekend = 0 and holiday = 0) as tableWithBusinessDates order by business_date desc

            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(BusinessCalendar));
                c.Add(Expression.Gt(BusinessDateProperty, startDate));
                c.Add(Expression.Eq(HolidayProperty, false));
                c.SetMaxResults(businessDays);

                List<BusinessCalendar> deliveryDateList = (List<BusinessCalendar>)c.List<BusinessCalendar>();
                BusinessCalendar deliveryDate = deliveryDateList[deliveryDateList.Count - 1];

                TimeSpan ts = new TimeSpan(
                    new DateTime(deliveryDate.BusinessDate.Year, deliveryDate.BusinessDate.Month, deliveryDate.BusinessDate.Day, 0, 0, 0).Ticks
                    -
                    new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0).Ticks);

                return ts.Days;
            }
        }

        #endregion

    }
}
