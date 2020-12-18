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
	[Class(Schema="`dbo`", Table="`registry`")]
	public partial class Registry
	{
		#region Constants
        public const string RegistryEntity = "Registry";
		public const string RegistryIdProperty = "RegistryId";
		public const string UserIdProperty = "UserId";
		public const string RoleProperty = "Role";
		public const string FmIdProperty = "FmId";
		public const string UserNameProperty = "UserName";
		public const string PasswordProperty = "Password";
		public const string LoginDatetimeProperty = "LoginDatetime";
		public const string LogoutDatetimeProperty = "LogoutDatetime";
		public const string CampaignIdProperty = "CampaignId";
		#endregion

		#region Fields
		protected int registryId = 0;
		protected int? userId = null;
		protected int? role = null;
		protected string fmId = null;
		protected string userName = null;
		protected string password = null;
		protected DateTime? loginDatetime = null;
		protected DateTime? logoutDatetime = null;
		protected int? campaignId = null;
		#endregion

		#region Constructors
		public Registry() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""RegistryId"" column=""`registry_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int RegistryId
		{
			get { return this.registryId; }
			set { this.registryId = value; }
		}

		[Property(Column="`user_id`")]
		public virtual int? UserId
		{
			get { return this.userId; }
			set { this.userId = value; }
		}

		[Property(Column="`role`")]
		public virtual int? Role
		{
			get { return this.role; }
			set { this.role = value; }
		}

		[Property(Column="`fm_id`")]
		public virtual string FmId
		{
			get { return this.fmId; }
			set { this.fmId = value; }
		}

		[Property(Column="`user_name`")]
		public virtual string UserName
		{
			get { return this.userName; }
			set { this.userName = value; }
		}

		[Property(Column="`password`")]
		public virtual string Password
		{
			get { return this.password; }
			set { this.password = value; }
		}

		[Property(Column="`login_datetime`")]
		public virtual DateTime? LoginDatetime
		{
			get { return this.loginDatetime; }
			set { this.loginDatetime = value; }
		}

		[Property(Column="`logout_datetime`")]
		public virtual DateTime? LogoutDatetime
		{
			get { return this.logoutDatetime; }
			set { this.logoutDatetime = value; }
		}

		[Property(Column="`campaign_id`")]
		public virtual int? CampaignId
		{
			get { return this.campaignId; }
			set { this.campaignId = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(Registry));
                return c;
            }
        }

        public static List<Registry> GetRegistryList(ICriteria criteria)
        {
            return (List<Registry>)criteria.List<Registry>();
        }

		public static Registry GetRegistry(int registryId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<Registry>(registryId);
			}
		}

		public static List<Registry> GetRegistryList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(Registry));
				return (List<Registry>)c.List<Registry>();
			}
		}

		public static List<Registry> GetRegistryList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(Registry));
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

				return (List<Registry>)c.List<Registry>();
			}
		}

		public static List<Registry> GetRegistryList(string sortExpression)
		{
			return GetRegistryList(sortExpression, -1, -1);
		}

		public static void InsertRegistry(Registry obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateRegistry(Registry obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteRegistry(Registry obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static Registry PopulateRegistry(IDataReader r)
		{
			Registry obj = new Registry();
			obj.RegistryId = (int)r["registry_id"];
			obj.UserId = (r["user_id"] == DBNull.Value) ? null : (int?)r["user_id"];
			obj.Role = (r["role"] == DBNull.Value) ? null : (int?)r["role"];
			obj.FmId = (r["fm_id"] == DBNull.Value) ? null : (string)r["fm_id"];
			obj.UserName = (r["user_name"] == DBNull.Value) ? null : (string)r["user_name"];
			obj.Password = (r["password"] == DBNull.Value) ? null : (string)r["password"];
			obj.LoginDatetime = (r["login_datetime"] == DBNull.Value) ? null : (DateTime?)r["login_datetime"];
			obj.LogoutDatetime = (r["logout_datetime"] == DBNull.Value) ? null : (DateTime?)r["logout_datetime"];
			obj.CampaignId = (r["campaign_id"] == DBNull.Value) ? null : (int?)r["campaign_id"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(Registry));
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

			Registry castObj = (Registry)obj;
			return (castObj != null && this.registryId == castObj.RegistryId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.registryId.GetHashCode());
		}
		#endregion
	}
}
