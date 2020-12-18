////////////////////////////////////////////////////////////////////////////////////////////
// Class generated by SqlCodeGen 1.1.0.0.
// Do not edit this file directly. Changes will be lost when this file is regenerated.
// Extensions should be added in a separate file using partial classes.
////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NHibernate;
using NHibernate.Expression;
using NHibernate.Mapping.Attributes;

namespace QSP.Business.Fulfillment
{
	[Serializable]
	[Class(Schema="`dbo`", Table="`ADDRESS_SYNC_LOG`")]
	public partial class ADDRESSSYNCLOG
	{
		#region Constants
        public const string ADDRESSSYNCLOGEntity = "ADDRESSSYNCLOG";
		public const string LOGIDProperty = "LOGID";
		public const string ADDRESSIDProperty = "ADDRESSID";
		public const string POSTALADDRESSIDProperty = "POSTALADDRESSID";
		public const string LOGTIMEProperty = "LOGTIME";
		#endregion

		#region Fields
		protected int lOGID = 0;
		protected int aDDRESSID = 0;
		protected int pOSTALADDRESSID = 0;
		protected DateTime lOGTIME = DateTime.Now;
		#endregion

		#region Constructors
		public ADDRESSSYNCLOG() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""LOGID"" column=""`LOG_ID`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int LOGID
		{
			get { return this.lOGID; }
			set { this.lOGID = value; }
		}

		[Property(Column="`ADDRESS_ID`")]
		public virtual int ADDRESSID
		{
			get { return this.aDDRESSID; }
			set { this.aDDRESSID = value; }
		}

		[Property(Column="`POSTAL_ADDRESS_ID`")]
		public virtual int POSTALADDRESSID
		{
			get { return this.pOSTALADDRESSID; }
			set { this.pOSTALADDRESSID = value; }
		}

		[Property(Column="`LOG_TIME`")]
		public virtual DateTime LOGTIME
		{
			get { return this.lOGTIME; }
			set { this.lOGTIME = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(ADDRESSSYNCLOG));
                return c;
            }
        }

        public static List<ADDRESSSYNCLOG> GetADDRESSSYNCLOGList(ICriteria criteria)
        {
            return (List<ADDRESSSYNCLOG>)criteria.List<ADDRESSSYNCLOG>();
        }

		public static ADDRESSSYNCLOG GetADDRESSSYNCLOG(int lOGID)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<ADDRESSSYNCLOG>(lOGID);
			}
		}

		public static List<ADDRESSSYNCLOG> GetADDRESSSYNCLOGList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(ADDRESSSYNCLOG));
				return (List<ADDRESSSYNCLOG>)c.List<ADDRESSSYNCLOG>();
			}
		}

		public static List<ADDRESSSYNCLOG> GetADDRESSSYNCLOGList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(ADDRESSSYNCLOG));
				if (sortExpression != null && sortExpression != "")
				{
					// Get ascending or descending order
					bool descending = sortExpression.Contains(" DESC");

					// Strip off ASC or DESC ordering
					sortExpression = sortExpression.Replace(" ASC", "");
					sortExpression = sortExpression.Replace(" DESC", "");
					sortExpression = sortExpression.Trim();

					// Get multi column sort from the comma delimited string
					List<String> expressions = new List<String>();
					if (sortExpression.Contains(","))
					{
						string[] tokens = sortExpression.Split(",".ToCharArray());
						for (int i = 0; i < tokens.Length; i++)
						{
							tokens[i] = tokens[i].Trim();
							if (tokens[i] != "")
								expressions.Add(tokens[i]);
						}
					}
					else if (sortExpression != "")
					{
						expressions.Add(sortExpression);
					}

					// Create the order
					for (int i = 0; i < expressions.Count; i++)
					{
						if (descending)
							c.AddOrder(NHibernate.Expression.Order.Desc(expressions[i]));
						else
							c.AddOrder(NHibernate.Expression.Order.Asc(expressions[i]));
					}
				}

				// Set offset and limit
				if (startRowIndex >= 0)
					c.SetFirstResult(startRowIndex);

				if (maximumRows >= 0)
					c.SetMaxResults(maximumRows);

				return (List<ADDRESSSYNCLOG>)c.List<ADDRESSSYNCLOG>();
			}
		}

		public static List<ADDRESSSYNCLOG> GetADDRESSSYNCLOGList(string sortExpression)
		{
			return GetADDRESSSYNCLOGList(sortExpression, -1, -1);
		}

		public static void InsertADDRESSSYNCLOG(ADDRESSSYNCLOG obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateADDRESSSYNCLOG(ADDRESSSYNCLOG obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteADDRESSSYNCLOG(ADDRESSSYNCLOG obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static ADDRESSSYNCLOG PopulateADDRESSSYNCLOG(IDataReader r)
		{
			ADDRESSSYNCLOG obj = new ADDRESSSYNCLOG();
			obj.LOGID = (int)r["LOG_ID"];
			obj.ADDRESSID = (int)r["ADDRESS_ID"];
			obj.POSTALADDRESSID = (int)r["POSTAL_ADDRESS_ID"];
			obj.LOGTIME = (DateTime)r["LOG_TIME"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(ADDRESSSYNCLOG));
				c.SetProjection(Projections.RowCount());
				return (int) c.UniqueResult();
			}
		}

		/// <summary>
		/// Insert the entity to database.
		/// </summary>
		public virtual void Insert()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ITransaction trans = session.BeginTransaction();
				try
				{
					session.Save(this);
					trans.Commit();
				}
				catch
				{
					trans.Rollback();
					throw;
				}
			}
		}

		/// <summary>
		/// Update the entity to database.
		/// </summary>
		public virtual void Update()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ITransaction trans = session.BeginTransaction();
				try
				{
					session.Update(this);
					trans.Commit();
				}
				catch
				{
					trans.Rollback();
					throw;
				}
			}
		}

		/// <summary>
		/// Persist the entity back to database.
		/// </summary>
		public virtual void Save()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ITransaction trans = session.BeginTransaction();
				try
				{
					session.SaveOrUpdate(this);
					trans.Commit();
				}
				catch
				{
					trans.Rollback();
					throw;
				}
			}
		}

		/// <summary>
		/// Delete entity in database.
		/// </summary>
		public virtual void Delete()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ITransaction trans = session.BeginTransaction();
				try
				{
					session.Delete(this);
					trans.Commit();
				}
				catch
				{
					trans.Rollback();
					throw;
				}
			}
		}

		/// <summary>
		/// Local implementation of Equals based on unique value members
		/// </summary>
		public override bool Equals(object obj)
		{
			if (this == obj)
				return true;

			if ((obj == null) || (obj.GetType() != this.GetType())) 
				return false;

			ADDRESSSYNCLOG castObj = (ADDRESSSYNCLOG)obj;
			return (castObj != null && this.lOGID == castObj.LOGID);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.lOGID.GetHashCode());
		}
		#endregion
	}
}
