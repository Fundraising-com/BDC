using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QSP.OrderExpress.Business.Context;
using QSP.OrderExpress.Business.Entity;

namespace QSPForm.Business
{
    public class FiscalYearSystem
    {

        /// <summary>
        /// Gets the fiscal year for the specified date
        /// </summary>
        /// <param name="date">The date to get the fiscal year from</param>
        /// <returns>The fiscal year</returns>
        public static int GetFYFromDate(DateTime date)
        {

            int result = date.Year;

            // only if the month is between July and December, fiscal year would next year
            //i.e 2010-08-01 means FY 2011
            //i.e 2010-03-25 means FY 2010
            if ((date.Month >= 7) && (date.Month <= 12))
                result = date.Year + 1;

            #region Old Logic Commented
            //if (date >= new DateTime(2011, 1, 1) && date <= new DateTime(2011, 12, 31))
            //{
            //    result = 2011;
            //}
            //else if (date >= new DateTime(2009, 7, 1) && date <= new DateTime(2010, 12, 31))
            //{
            //    result = 2010;
            //}
            //else if (date >= new DateTime(2008, 7, 1) && date < new DateTime(2009, 7, 1))
            //{
            //    result = 2009;
            //}
            //else if (date >= new DateTime(2007, 7, 1) && date < new DateTime(2008, 7, 1))
            //{
            //    result = 2008;
            //}
            #endregion

            return result;
        }

        /// <summary>
        /// Gets the fiscal year for the specified form
        /// </summary>
        /// <param name="formId">The id of the form to get the fiscal year from</param>
        /// <returns>The fiscal year</returns>
        public static int GetFYFromForm(int formId)
        {
            int result = 0;

            QSP.OrderExpress.Business.Context.OrderExpressDataContext context = new QSP.OrderExpress.Business.Context.OrderExpressDataContext();

            Form form = context.Forms.Where(f => f.FormId == formId).SingleOrDefault();

            if (form != null)
            {
                if (form.SeasonId.HasValue)
                {
                    Season season = context.Seasons.Where(s => s.SeasonId == form.SeasonId).SingleOrDefault();
                    result = season.FiscalYear;
                }
            }

            return result;
        }

    }
}
