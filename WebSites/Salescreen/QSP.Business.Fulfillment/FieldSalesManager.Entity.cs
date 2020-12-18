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
	[Class(Schema="`dbo`", Table="`field_sales_manager`")]
	public partial class FieldSalesManager
	{
		#region Constants
        public const string FieldSalesManagerEntity = "FieldSalesManager";
		public const string FieldSalesManagerIdProperty = "FieldSalesManagerId";
		public const string FmIdProperty = "FmId";
		public const string UserIdProperty = "UserId";
		public const string FirstNameProperty = "FirstName";
		public const string LastNameProperty = "LastName";
		public const string CustomerIdProperty = "CustomerId";
		public const string CUserProfileInstanceProperty = "CUserProfileInstance";
		public const string DeletedProperty = "Deleted";
		public const string CreateDateProperty = "CreateDate";
		public const string CreateUserIdProperty = "CreateUserId";
		public const string UpdateDateProperty = "UpdateDate";
		public const string UpdateUserIdProperty = "UpdateUserId";
		#endregion

		#region Fields
		protected int fieldSalesManagerId = 0;
		protected string fmId = "";
		protected int? userId = null;
		protected string firstName = null;
		protected string lastName = null;
		protected int? customerId = null;
		protected int? cUserProfileInstance = null;
		protected bool deleted = false;
		protected DateTime createDate = DateTime.Now;
		protected int createUserId = 0;
		protected DateTime? updateDate = DateTime.Now;
		protected int? updateUserId = 0;
		#endregion

		#region Constructors
		public FieldSalesManager() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""FieldSalesManagerId"" column=""`field_sales_manager_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int FieldSalesManagerId
		{
			get { return this.fieldSalesManagerId; }
			set { this.fieldSalesManagerId = value; }
		}

		[Property(Column="`fm_id`")]
		public virtual string FmId
		{
			get { return this.fmId; }
			set { this.fmId = value; }
		}

		[Property(Column="`user_id`")]
		public virtual int? UserId
		{
			get { return this.userId; }
			set { this.userId = value; }
		}

		[Property(Column="`first_name`")]
		public virtual string FirstName
		{
			get { return this.firstName; }
			set { this.firstName = value; }
		}

		[Property(Column="`last_name`")]
		public virtual string LastName
		{
			get { return this.lastName; }
			set { this.lastName = value; }
		}

		[Property(Column="`customer_id`")]
		public virtual int? CustomerId
		{
			get { return this.customerId; }
			set { this.customerId = value; }
		}

		[Property(Column="`CUserProfile_Instance`")]
		public virtual int? CUserProfileInstance
		{
			get { return this.cUserProfileInstance; }
			set { this.cUserProfileInstance = value; }
		}

		[Property(Column="`deleted`")]
		public virtual bool Deleted
		{
			get { return this.deleted; }
			set { this.deleted = value; }
		}

		[Property(Column="`create_date`")]
		public virtual DateTime CreateDate
		{
			get { return this.createDate; }
			set { this.createDate = value; }
		}

		[Property(Column="`create_user_id`")]
		public virtual int CreateUserId
		{
			get { return this.createUserId; }
			set { this.createUserId = value; }
		}

		[Property(Column="`update_date`")]
		public virtual DateTime? UpdateDate
		{
			get { return this.updateDate; }
			set { this.updateDate = value; }
		}

		[Property(Column="`update_user_id`")]
		public virtual int? UpdateUserId
		{
			get { return this.updateUserId; }
			set { this.updateUserId = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            ISession session = SqlSessionManager.OpenSession();
            ICriteria c = session.CreateCriteria(typeof(FieldSalesManager));
            return c;
            
        }

        public static List<FieldSalesManager> GetFieldSalesManagerList(ICriteria criteria)
        {
            return (List<FieldSalesManager>)criteria.List<FieldSalesManager>();
        }

		public static FieldSalesManager GetFieldSalesManager(int fieldSalesManagerId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<FieldSalesManager>(fieldSalesManagerId);
			}
		}

		public static List<FieldSalesManager> GetFieldSalesManagerList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(FieldSalesManager));
				return (List<FieldSalesManager>)c.List<FieldSalesManager>();
			}
		}

		public static List<FieldSalesManager> GetFieldSalesManagerList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(FieldSalesManager));
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

				return (List<FieldSalesManager>)c.List<FieldSalesManager>();
			}
		}

		public static List<FieldSalesManager> GetFieldSalesManagerList(string sortExpression)
		{
			return GetFieldSalesManagerList(sortExpression, -1, -1);
		}

		public static void InsertFieldSalesManager(FieldSalesManager obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateFieldSalesManager(FieldSalesManager obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteFieldSalesManager(FieldSalesManager obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static FieldSalesManager PopulateFieldSalesManager(IDataReader r)
		{
			FieldSalesManager obj = new FieldSalesManager();
			obj.FieldSalesManagerId = (int)r["field_sales_manager_id"];
			obj.FmId = (string)r["fm_id"];
			obj.UserId = (r["user_id"] == DBNull.Value) ? null : (int?)r["user_id"];
			obj.FirstName = (r["first_name"] == DBNull.Value) ? null : (string)r["first_name"];
			obj.LastName = (r["last_name"] == DBNull.Value) ? null : (string)r["last_name"];
			obj.CustomerId = (r["customer_id"] == DBNull.Value) ? null : (int?)r["customer_id"];
			obj.CUserProfileInstance = (r["CUserProfile_Instance"] == DBNull.Value) ? null : (int?)r["CUserProfile_Instance"];
			obj.Deleted = (bool)r["deleted"];
			obj.CreateDate = (DateTime)r["create_date"];
			obj.CreateUserId = (int)r["create_user_id"];
			obj.UpdateDate = (r["update_date"] == DBNull.Value) ? null : (DateTime?)r["update_date"];
			obj.UpdateUserId = (r["update_user_id"] == DBNull.Value) ? null : (int?)r["update_user_id"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(FieldSalesManager));
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

			FieldSalesManager castObj = (FieldSalesManager)obj;
			return (castObj != null && this.fieldSalesManagerId == castObj.FieldSalesManagerId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.fieldSalesManagerId.GetHashCode());
		}
		#endregion
	}
}
