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
	[Class(Schema="`dbo`", Table="`Account_Search`")]
	public partial class AccountSearch
	{
		#region Constants
        public const string AccountSearchEntity = "AccountSearch";
		public const string AccountSearchIdProperty = "AccountSearchId";
		public const string FulfAccountIdProperty = "FulfAccountId";
		public const string CampaignIdProperty = "CampaignId";
		public const string DisplayNameProperty = "DisplayName";
		public const string SearchNameProperty = "SearchName";
		public const string CityProperty = "City";
		public const string StateProperty = "State";
		public const string ZipProperty = "Zip";
		public const string CountryProperty = "Country";
		public const string ProgramIDProperty = "ProgramID";
		public const string ProgramDescProperty = "ProgramDesc";
		public const string IsStaffOrderProperty = "IsStaffOrder";
		#endregion

		#region Fields
		protected int accountSearchId = 0;
		protected int fulfAccountId = 0;
		protected int campaignId = 0;
		protected string displayName = "";
		protected string searchName = null;
		protected string city = "";
		protected string state = "";
		protected string zip = "";
		protected string country = "";
		protected int? programID = null;
		protected string programDesc = null;
		protected bool? isStaffOrder = null;
		#endregion

		#region Constructors
		public AccountSearch() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""AccountSearchId"" column=""`account_search_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int AccountSearchId
		{
			get { return this.accountSearchId; }
			set { this.accountSearchId = value; }
		}

		[Property(Column="`fulf_account_id`")]
		public virtual int FulfAccountId
		{
			get { return this.fulfAccountId; }
			set { this.fulfAccountId = value; }
		}

		[Property(Column="`campaign_id`")]
		public virtual int CampaignId
		{
			get { return this.campaignId; }
			set { this.campaignId = value; }
		}

		[Property(Column="`DisplayName`")]
		public virtual string DisplayName
		{
			get { return this.displayName; }
			set { this.displayName = value; }
		}

		[Property(Column="`SearchName`")]
		public virtual string SearchName
		{
			get { return this.searchName; }
			set { this.searchName = value; }
		}

		[Property(Column="`City`")]
		public virtual string City
		{
			get { return this.city; }
			set { this.city = value; }
		}

		[Property(Column="`State`")]
		public virtual string State
		{
			get { return this.state; }
			set { this.state = value; }
		}

		[Property(Column="`Zip`")]
		public virtual string Zip
		{
			get { return this.zip; }
			set { this.zip = value; }
		}

		[Property(Column="`Country`")]
		public virtual string Country
		{
			get { return this.country; }
			set { this.country = value; }
		}

		[Property(Column="`ProgramID`")]
		public virtual int? ProgramID
		{
			get { return this.programID; }
			set { this.programID = value; }
		}

		[Property(Column="`ProgramDesc`")]
		public virtual string ProgramDesc
		{
			get { return this.programDesc; }
			set { this.programDesc = value; }
		}

		[Property(Column="`IsStaffOrder`")]
		public virtual bool? IsStaffOrder
		{
			get { return this.isStaffOrder; }
			set { this.isStaffOrder = value; }
		}
		#endregion

		#region Methods
        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(AccountSearch));
                return c;
            }
        }

        public static List<AccountSearch> GetAccountSearchList(ICriteria criteria)
        {
            return (List<AccountSearch>)criteria.List<AccountSearch>();
        }

		public static AccountSearch GetAccountSearch(int accountSearchId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<AccountSearch>(accountSearchId);
			}
		}

		public static List<AccountSearch> GetAccountSearchList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(AccountSearch));
				return (List<AccountSearch>)c.List<AccountSearch>();
			}
		}

		public static List<AccountSearch> GetAccountSearchList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(AccountSearch));
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

				return (List<AccountSearch>)c.List<AccountSearch>();
			}
		}

		public static List<AccountSearch> GetAccountSearchList(string sortExpression)
		{
			return GetAccountSearchList(sortExpression, -1, -1);
		}

		public static void InsertAccountSearch(AccountSearch obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateAccountSearch(AccountSearch obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteAccountSearch(AccountSearch obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static AccountSearch PopulateAccountSearch(IDataReader r)
		{
			AccountSearch obj = new AccountSearch();
			obj.AccountSearchId = (int)r["account_search_id"];
			obj.FulfAccountId = (int)r["fulf_account_id"];
			obj.CampaignId = (int)r["campaign_id"];
			obj.DisplayName = (string)r["DisplayName"];
			obj.SearchName = (r["SearchName"] == DBNull.Value) ? null : (string)r["SearchName"];
			obj.City = (string)r["City"];
			obj.State = (string)r["State"];
			obj.Zip = (string)r["Zip"];
			obj.Country = (string)r["Country"];
			obj.ProgramID = (r["ProgramID"] == DBNull.Value) ? null : (int?)r["ProgramID"];
			obj.ProgramDesc = (r["ProgramDesc"] == DBNull.Value) ? null : (string)r["ProgramDesc"];
			obj.IsStaffOrder = (r["IsStaffOrder"] == DBNull.Value) ? null : (bool?)r["IsStaffOrder"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(AccountSearch));
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

			AccountSearch castObj = (AccountSearch)obj;
			return (castObj != null && this.accountSearchId == castObj.AccountSearchId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.accountSearchId.GetHashCode());
		}
		#endregion
	}
}
