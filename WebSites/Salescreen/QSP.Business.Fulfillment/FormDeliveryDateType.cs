using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace QSP.Business.Fulfillment
{
	public partial class FormDeliveryDateType
	{
		#region Methods

        public static List<FormDeliveryDateType> GetFormDeliveryDateTypeFromFormId(int formId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(FormDeliveryDateType));
                c.Add(Expression.Eq(FormIdProperty, formId));

                return (List<FormDeliveryDateType>)c.List<FormDeliveryDateType>();
            }
        }

        #endregion
    }
}
