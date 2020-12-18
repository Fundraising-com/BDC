using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace QSP.Business.Fulfillment
{
	public partial class UserRole
	{
		public static List<UserRole> GetUserRoleListByUser(int userId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(UserRole));
				c.Add(Expression.Eq(UserIdProperty, userId));
				return (List<UserRole>)c.List<UserRole>();
			}
		}

		public static UserRole GetUserRole(int userId, int roleId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(UserRole));
				c.Add(Expression.Eq(UserIdProperty, userId));
				c.Add(Expression.Eq(RoleIdProperty, roleId));
				return c.UniqueResult<UserRole>();
			}
		}
	}
}
