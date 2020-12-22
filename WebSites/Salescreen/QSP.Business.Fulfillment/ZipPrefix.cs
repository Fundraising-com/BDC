using System;
using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace QSP.Business.Fulfillment
{
    public partial class ZipPrefix
	{
		#region Constants
		private const int PrefixLength = 3;
		#endregion

		#region Methods

		public static ZipPrefix GetZipCodePrefixFromZipCodePrefix(string zipCodePrefix)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(ZipPrefix));
                c.Add(Expression.Eq(ZipPrefixTextProperty, zipCodePrefix));
                return c.UniqueResult<ZipPrefix>();
            }
        }


		public static ZipPrefix GetZipCodePrefixByZipCode(string zipCode)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(ZipPrefix));
				c.Add(Expression.Eq(ZipPrefixTextProperty, zipCode.Trim().Substring(0, PrefixLength)));
				return c.UniqueResult<ZipPrefix>();
			}
		}

        #endregion
    }
}
