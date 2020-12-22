using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Mapping.Attributes;

namespace QSP.Business.Fulfillment
{
	public partial class CMAppItems
	{
		public static CMAppItems GetCMAppItemsByAppItemNumber(int noAppItem)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(CMAppItems));
				c.Add(Expression.Eq(NoAppItemProperty, noAppItem));
				c.SetMaxResults(1);
				return c.UniqueResult<CMAppItems>();
			}
		}
	}
}
