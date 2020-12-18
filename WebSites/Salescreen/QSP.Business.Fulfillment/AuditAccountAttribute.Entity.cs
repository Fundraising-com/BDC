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
using NHibernate.Criterion;
using NHibernate.Mapping.Attributes;

namespace QSP.Business.Fulfillment
{
	[Serializable]
	[Class(Schema="`dbo`", Table="`audit_account_attribute`")]
	public partial class AuditAccountAttribute
	{
		#region Constants
        public const string AuditAccountAttributeEntity = "AuditAccountAttribute";
		public const string AuditAccountAttributeIdProperty = "AuditAccountAttributeId";
		public const string AuditDateProperty = "AuditDate";
		public const string AccountAttributeIdProperty = "AccountAttributeId";
		public const string AccountIdProperty = "AccountId";
		public const string DisplayNameProperty = "DisplayName";
		public const string WebEnabledProperty = "WebEnabled";
		public const string CreateUserIdProperty = "CreateUserId";
		public const string CreateDateProperty = "CreateDate";
		#endregion

		#region Fields
		protected int auditAccountAttributeId = 0;
		protected DateTime auditDate = DateTime.Now;
		protected int? accountAttributeId = null;
		protected int? accountId = null;
		protected string displayName = null;
		protected bool? webEnabled = null;
		protected int? createUserId = null;
		protected DateTime? createDate = null;
		#endregion

		#region Constructors
		public AuditAccountAttribute() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""AuditAccountAttributeId"" column=""`audit_account_attribute_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int AuditAccountAttributeId
		{
			get { return this.auditAccountAttributeId; }
			set { this.auditAccountAttributeId = value; }
		}

		[Property(Column="`audit_date`")]
		public virtual DateTime AuditDate
		{
			get { return this.auditDate; }
			set { this.auditDate = value; }
		}

		[Property(Column="`account_attribute_id`")]
		public virtual int? AccountAttributeId
		{
			get { return this.accountAttributeId; }
			set { this.accountAttributeId = value; }
		}

		[Property(Column="`account_id`")]
		public virtual int? AccountId
		{
			get { return this.accountId; }
			set { this.accountId = value; }
		}

		[Property(Column="`display_name`")]
		public virtual string DisplayName
		{
			get { return this.displayName; }
			set { this.displayName = value; }
		}

		[Property(Column="`web_enabled`")]
		public virtual bool? WebEnabled
		{
			get { return this.webEnabled; }
			set { this.webEnabled = value; }
		}

		[Property(Column="`create_user_id`")]
		public virtual int? CreateUserId
		{
			get { return this.createUserId; }
			set { this.createUserId = value; }
		}

		[Property(Column="`create_date`")]
		public virtual DateTime? CreateDate
		{
			get { return this.createDate; }
			set { this.createDate = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(AuditAccountAttribute));
                return c;
            }
        }

        public static List<AuditAccountAttribute> GetAuditAccountAttributeList(ICriteria criteria)
        {
            return (List<AuditAccountAttribute>)criteria.List<AuditAccountAttribute>();
        }

		public static AuditAccountAttribute GetAuditAccountAttribute(int auditAccountAttributeId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<AuditAccountAttribute>(auditAccountAttributeId);
			}
		}

		public static List<AuditAccountAttribute> GetAuditAccountAttributeList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(AuditAccountAttribute));
				return (List<AuditAccountAttribute>)c.List<AuditAccountAttribute>();
			}
		}

		public static List<AuditAccountAttribute> GetAuditAccountAttributeList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(AuditAccountAttribute));
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
							c.AddOrder(NHibernate.Criterion.Order.Desc(expressions[i]));
						else
							c.AddOrder(NHibernate.Criterion.Order.Asc(expressions[i]));
					}
				}

				// Set offset and limit
				if (startRowIndex >= 0)
					c.SetFirstResult(startRowIndex);

				if (maximumRows >= 0)
					c.SetMaxResults(maximumRows);

				return (List<AuditAccountAttribute>)c.List<AuditAccountAttribute>();
			}
		}

		public static List<AuditAccountAttribute> GetAuditAccountAttributeList(string sortExpression)
		{
			return GetAuditAccountAttributeList(sortExpression, -1, -1);
		}

		public static void InsertAuditAccountAttribute(AuditAccountAttribute obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateAuditAccountAttribute(AuditAccountAttribute obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteAuditAccountAttribute(AuditAccountAttribute obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static AuditAccountAttribute PopulateAuditAccountAttribute(IDataReader r)
		{
			AuditAccountAttribute obj = new AuditAccountAttribute();
			obj.AuditAccountAttributeId = (int)r["audit_account_attribute_id"];
			obj.AuditDate = (DateTime)r["audit_date"];
			obj.AccountAttributeId = (r["account_attribute_id"] == DBNull.Value) ? null : (int?)r["account_attribute_id"];
			obj.AccountId = (r["account_id"] == DBNull.Value) ? null : (int?)r["account_id"];
			obj.DisplayName = (r["display_name"] == DBNull.Value) ? null : (string)r["display_name"];
			obj.WebEnabled = (r["web_enabled"] == DBNull.Value) ? null : (bool?)r["web_enabled"];
			obj.CreateUserId = (r["create_user_id"] == DBNull.Value) ? null : (int?)r["create_user_id"];
			obj.CreateDate = (r["create_date"] == DBNull.Value) ? null : (DateTime?)r["create_date"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(AuditAccountAttribute));
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

			AuditAccountAttribute castObj = (AuditAccountAttribute)obj;
			return (castObj != null && this.auditAccountAttributeId == castObj.AuditAccountAttributeId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.auditAccountAttributeId.GetHashCode());
		}
		#endregion
	}
}
