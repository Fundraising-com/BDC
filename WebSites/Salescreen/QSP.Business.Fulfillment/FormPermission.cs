using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace QSP.Business.Fulfillment
{
	public partial class FormPermission
	{
		public static List<FormPermission> GetFormPermissionListByMatchingForm(int formId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				// Select all permissions including NULL roles
				ICriteria c = session.CreateCriteria(typeof(FormPermission));
				c.Add(Expression.Or(Expression.IsNull(FormIdProperty), Expression.Eq(FormIdProperty, formId)));
                c.AddOrder(NHibernate.Criterion.Order.Asc(FormPermissionIdProperty));
				return (List<FormPermission>)c.List<FormPermission>();
			}
		}

		public static List<FormPermission> GetFormPermissionListByMatchingRole(int roleId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				// Select all permissions including NULL roles
				ICriteria c = session.CreateCriteria(typeof(FormPermission));
				c.Add(Expression.Or(Expression.IsNull(RoleIdProperty), Expression.Eq(RoleIdProperty, roleId)));
				c.AddOrder(NHibernate.Criterion.Order.Asc(FormPermissionIdProperty));
				return (List<FormPermission>)c.List<FormPermission>();
			}
		}

		public static List<FormPermission> GetFormPermissionListByMatchingUserRoles(List<UserRole> userRoles)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				// Select all permissions including NULL roles
				ICriteria c = session.CreateCriteria(typeof(FormPermission));

				Disjunction orExpressions = new Disjunction();
				orExpressions.Add(Expression.IsNull(RoleIdProperty));
				foreach (UserRole u in userRoles)
					orExpressions.Add(Expression.Eq(RoleIdProperty, u.RoleId));

				c.Add(orExpressions);
				c.AddOrder(NHibernate.Criterion.Order.Asc(FormPermissionIdProperty));
				return (List<FormPermission>)c.List<FormPermission>();
			}
		}
	}
}
