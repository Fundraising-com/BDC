using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NHibernate;
using NHibernate.Expression;
namespace QSP.Business.Fulfillment
{
	public partial class FormPermissionRegion
	{
		public static List<FormPermissionRegion> GetFormPermissionRegionListByMatchingForm(int formId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				// Select all permissions including NULL roles
				ICriteria c = session.CreateCriteria(typeof(FormPermissionRegion));
				c.Add(Expression.Or(Expression.IsNull(FormIdProperty), Expression.Eq(FormIdProperty, formId)));
				c.AddOrder(NHibernate.Expression.Order.Asc(FormPermissionRegionIdProperty));
				return (List<FormPermissionRegion>)c.List<FormPermissionRegion>();
			}
		}

		public static List<FormPermissionRegion> GetFormPermissionRegionListByMatchingZipCode(string zipCode)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				// Select all permissions including NULL roles and possible zip prefixes
				ICriteria c = session.CreateCriteria(typeof(FormPermissionRegion));
				
				Disjunction orExpressions = new Disjunction();
				orExpressions.Add(Expression.IsNull(ZipProperty));
				for (int i = 0; i < zipCode.Length; i++)
					orExpressions.Add(Expression.Eq(ZipProperty, zipCode.Remove(i)));

				c.Add(orExpressions);
				c.AddOrder(NHibernate.Expression.Order.Asc(FormPermissionRegionIdProperty));
				return (List<FormPermissionRegion>)c.List<FormPermissionRegion>();
			}
		}
	}
}
