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
	[Class(Schema="`dbo`", Table="`status_reason`")]
	public partial class StatusReason
	{
		#region Constants
        public const string StatusReasonEntity = "StatusReason";
		public const string StatusReasonIdProperty = "StatusReasonId";
		public const string StatusReasonNameProperty = "StatusReasonName";
		public const string OEREASProperty = "OEREAS";
		#endregion

		#region Fields
		protected int statusReasonId = 0;
		protected string statusReasonName = "";
		protected string oEREAS = null;
		#endregion

		#region Constructors
		public StatusReason() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""StatusReasonId"" column=""`status_reason_id`"">
			<generator class=""assigned"">
			</generator>
		</id>")]

		public virtual int StatusReasonId
		{
			get { return this.statusReasonId; }
			set { this.statusReasonId = value; }
		}

		[Property(Column="`status_reason_name`")]
		public virtual string StatusReasonName
		{
			get { return this.statusReasonName; }
			set { this.statusReasonName = value; }
		}

		[Property(Column="`OEREAS`")]
		public virtual string OEREAS
		{
			get { return this.oEREAS; }
			set { this.oEREAS = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(StatusReason));
                return c;
            }
        }

        public static List<StatusReason> GetStatusReasonList(ICriteria criteria)
        {
            return (List<StatusReason>)criteria.List<StatusReason>();
        }

		public static StatusReason GetStatusReason(int statusReasonId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<StatusReason>(statusReasonId);
			}
		}

		public static List<StatusReason> GetStatusReasonList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(StatusReason));
				return (List<StatusReason>)c.List<StatusReason>();
			}
		}

		public static List<StatusReason> GetStatusReasonList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(StatusReason));
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

				return (List<StatusReason>)c.List<StatusReason>();
			}
		}

		public static List<StatusReason> GetStatusReasonList(string sortExpression)
		{
			return GetStatusReasonList(sortExpression, -1, -1);
		}

		public static void InsertStatusReason(StatusReason obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateStatusReason(StatusReason obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteStatusReason(StatusReason obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static StatusReason PopulateStatusReason(IDataReader r)
		{
			StatusReason obj = new StatusReason();
			obj.StatusReasonId = (int)r["status_reason_id"];
			obj.StatusReasonName = (string)r["status_reason_name"];
			obj.OEREAS = (r["OEREAS"] == DBNull.Value) ? null : (string)r["OEREAS"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(StatusReason));
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

			StatusReason castObj = (StatusReason)obj;
			return (castObj != null && this.statusReasonId == castObj.StatusReasonId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.statusReasonId.GetHashCode());
		}
		#endregion
	}
}
