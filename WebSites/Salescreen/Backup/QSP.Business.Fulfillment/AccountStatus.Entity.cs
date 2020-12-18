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
	[Class(Schema="`dbo`", Table="`account_status`")]
	public partial class AccountStatus
	{
		#region Constants
        public const string AccountStatusEntity = "AccountStatus";
		public const string AccountStatusIdProperty = "AccountStatusId";
		public const string AccountStatusNameProperty = "AccountStatusName";
		public const string ColorCodeProperty = "ColorCode";
		public const string StatusCategoryIdProperty = "StatusCategoryId";
		public const string DescriptionProperty = "Description";
		public const string ShortDescriptionProperty = "ShortDescription";
		#endregion

		#region Fields
		protected int accountStatusId = 0;
		protected string accountStatusName = "";
		protected string colorCode = null;
		protected int? statusCategoryId = null;
		protected string description = null;
		protected string shortDescription = null;
		#endregion

		#region Constructors
		public AccountStatus() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""AccountStatusId"" column=""`account_status_id`"">
			<generator class=""assigned"">
			</generator>
		</id>")]

		public virtual int AccountStatusId
		{
			get { return this.accountStatusId; }
			set { this.accountStatusId = value; }
		}

		[Property(Column="`account_status_name`")]
		public virtual string AccountStatusName
		{
			get { return this.accountStatusName; }
			set { this.accountStatusName = value; }
		}

		[Property(Column="`color_code`")]
		public virtual string ColorCode
		{
			get { return this.colorCode; }
			set { this.colorCode = value; }
		}

		[Property(Column="`status_category_id`")]
		public virtual int? StatusCategoryId
		{
			get { return this.statusCategoryId; }
			set { this.statusCategoryId = value; }
		}

		[Property(Column="`description`")]
		public virtual string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		[Property(Column="`short_description`")]
		public virtual string ShortDescription
		{
			get { return this.shortDescription; }
			set { this.shortDescription = value; }
		}
		#endregion

		#region Methods
        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(AccountStatus));
                return c;
            }
        }

        public static List<AccountStatus> GetAccountStatusList(ICriteria criteria)
        {
            return (List<AccountStatus>)criteria.List<AccountStatus>();
        }

		public static AccountStatus GetAccountStatus(int accountStatusId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<AccountStatus>(accountStatusId);
			}
		}

		public static List<AccountStatus> GetAccountStatusList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(AccountStatus));
				return (List<AccountStatus>)c.List<AccountStatus>();
			}
		}

		public static List<AccountStatus> GetAccountStatusList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(AccountStatus));
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

				return (List<AccountStatus>)c.List<AccountStatus>();
			}
		}

		public static List<AccountStatus> GetAccountStatusList(string sortExpression)
		{
			return GetAccountStatusList(sortExpression, -1, -1);
		}

		public static void InsertAccountStatus(AccountStatus obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateAccountStatus(AccountStatus obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteAccountStatus(AccountStatus obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static AccountStatus PopulateAccountStatus(IDataReader r)
		{
			AccountStatus obj = new AccountStatus();
			obj.AccountStatusId = (int)r["account_status_id"];
			obj.AccountStatusName = (string)r["account_status_name"];
			obj.ColorCode = (r["color_code"] == DBNull.Value) ? null : (string)r["color_code"];
			obj.StatusCategoryId = (r["status_category_id"] == DBNull.Value) ? null : (int?)r["status_category_id"];
			obj.Description = (r["description"] == DBNull.Value) ? null : (string)r["description"];
			obj.ShortDescription = (r["short_description"] == DBNull.Value) ? null : (string)r["short_description"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(AccountStatus));
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

			AccountStatus castObj = (AccountStatus)obj;
			return (castObj != null && this.accountStatusId == castObj.AccountStatusId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.accountStatusId.GetHashCode());
		}
		#endregion
	}
}
