using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Expression;

namespace QSP.Business.Fulfillment
{
    public partial class FormProfitRate
    {
        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public static List<FormProfitRate> GetProfitRateListFromForm(int formId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(FormProfitRate));
                c.Add(Expression.Eq(FormIdProperty, formId));

                return (List<FormProfitRate>)c.List<FormProfitRate>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public static List<double> GetProfitRateList(int formId)
        {
            List<double> result = new List<double>();

            List<FormProfitRate> formProfitRateList = FormProfitRate.GetProfitRateListFromForm(formId);
            foreach (FormProfitRate formProfitRate in formProfitRateList)
            {
                int profitRateId = Convert.ToInt32(formProfitRate.ProfitRateId);

                ProfitRate profitRate = ProfitRate.GetProfitRate(profitRateId);

                result.Add(profitRate.ProfitRateAmount);
            }

            return result;
        }

        #endregion
    }
}
