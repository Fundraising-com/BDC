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
	[Class(Schema="`dbo`", Table="`user`")]
	public partial class User
	{
		#region Constants
        public const string UserEntity = "User";
		public const string UserIdProperty = "UserId";
		public const string RoleIdProperty = "RoleId";
		public const string UserNameProperty = "UserName";
		public const string PasswordProperty = "Password";
		public const string FirstNameProperty = "FirstName";
		public const string LastNameProperty = "LastName";
		public const string TitleProperty = "Title";
		public const string EmailProperty = "Email";
		public const string BestTimeToCallProperty = "BestTimeToCall";
		public const string DayPhoneNoProperty = "DayPhoneNo";
		public const string EveningPhoneNoProperty = "EveningPhoneNo";
		public const string FaxNoProperty = "FaxNo";
		public const string DeletedProperty = "Deleted";
		public const string CreateUserIdProperty = "CreateUserId";
		public const string CreateDateProperty = "CreateDate";
		public const string UpdateUserIdProperty = "UpdateUserId";
		public const string UpdateDateProperty = "UpdateDate";
		#endregion

		#region Fields
		protected int userId = 0;
		protected int? roleId = null;
		protected string userName = "";
		protected string password = "";
		protected string firstName = null;
		protected string lastName = null;
		protected string title = null;
		protected string email = null;
		protected string bestTimeToCall = null;
		protected string dayPhoneNo = null;
		protected string eveningPhoneNo = null;
		protected string faxNo = null;
		protected bool deleted = false;
		protected int createUserId = 0;
		protected DateTime createDate = DateTime.Now;
		protected int? updateUserId = null;
		protected DateTime? updateDate = DateTime.Now;
		#endregion

		#region Constructors
		public User() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""UserId"" column=""`user_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int UserId
		{
			get { return this.userId; }
			set { this.userId = value; }
		}

		[Property(Column="`role_id`")]
		public virtual int? RoleId
		{
			get { return this.roleId; }
			set { this.roleId = value; }
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

		[Property(Column="`title`")]
		public virtual string Title
		{
			get { return this.title; }
			set { this.title = value; }
		}

		[Property(Column="`email`")]
		public virtual string Email
		{
			get { return this.email; }
			set { this.email = value; }
		}

		[Property(Column="`best_time_to_call`")]
		public virtual string BestTimeToCall
		{
			get { return this.bestTimeToCall; }
			set { this.bestTimeToCall = value; }
		}

		[Property(Column="`day_phone_no`")]
		public virtual string DayPhoneNo
		{
			get { return this.dayPhoneNo; }
			set { this.dayPhoneNo = value; }
		}

		[Property(Column="`evening_phone_no`")]
		public virtual string EveningPhoneNo
		{
			get { return this.eveningPhoneNo; }
			set { this.eveningPhoneNo = value; }
		}

		[Property(Column="`fax_no`")]
		public virtual string FaxNo
		{
			get { return this.faxNo; }
			set { this.faxNo = value; }
		}

		[Property(Column="`deleted`")]
		public virtual bool Deleted
		{
			get { return this.deleted; }
			set { this.deleted = value; }
		}

		[Property(Column="`create_user_id`")]
		public virtual int CreateUserId
		{
			get { return this.createUserId; }
			set { this.createUserId = value; }
		}

		[Property(Column="`create_date`")]
		public virtual DateTime CreateDate
		{
			get { return this.createDate; }
			set { this.createDate = value; }
		}

		[Property(Column="`update_user_id`")]
		public virtual int? UpdateUserId
		{
			get { return this.updateUserId; }
			set { this.updateUserId = value; }
		}

		[Property(Column="`update_date`")]
		public virtual DateTime? UpdateDate
		{
			get { return this.updateDate; }
			set { this.updateDate = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(User));
                return c;
            }
        }

        public static List<User> GetUserList(ICriteria criteria)
        {
            return (List<User>)criteria.List<User>();
        }

		public static User GetUser(int userId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<User>(userId);
			}
		}

		public static List<User> GetUserList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(User));
				return (List<User>)c.List<User>();
			}
		}

		public static List<User> GetUserList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(User));
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

				return (List<User>)c.List<User>();
			}
		}

		public static List<User> GetUserList(string sortExpression)
		{
			return GetUserList(sortExpression, -1, -1);
		}

		public static void InsertUser(User obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateUser(User obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteUser(User obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static User PopulateUser(IDataReader r)
		{
			User obj = new User();
			obj.UserId = (int)r["user_id"];
			obj.RoleId = (r["role_id"] == DBNull.Value) ? null : (int?)r["role_id"];
			obj.UserName = (string)r["user_name"];
			obj.Password = (string)r["password"];
			obj.FirstName = (r["first_name"] == DBNull.Value) ? null : (string)r["first_name"];
			obj.LastName = (r["last_name"] == DBNull.Value) ? null : (string)r["last_name"];
			obj.Title = (r["title"] == DBNull.Value) ? null : (string)r["title"];
			obj.Email = (r["email"] == DBNull.Value) ? null : (string)r["email"];
			obj.BestTimeToCall = (r["best_time_to_call"] == DBNull.Value) ? null : (string)r["best_time_to_call"];
			obj.DayPhoneNo = (r["day_phone_no"] == DBNull.Value) ? null : (string)r["day_phone_no"];
			obj.EveningPhoneNo = (r["evening_phone_no"] == DBNull.Value) ? null : (string)r["evening_phone_no"];
			obj.FaxNo = (r["fax_no"] == DBNull.Value) ? null : (string)r["fax_no"];
			obj.Deleted = (bool)r["deleted"];
			obj.CreateUserId = (int)r["create_user_id"];
			obj.CreateDate = (DateTime)r["create_date"];
			obj.UpdateUserId = (r["update_user_id"] == DBNull.Value) ? null : (int?)r["update_user_id"];
			obj.UpdateDate = (r["update_date"] == DBNull.Value) ? null : (DateTime?)r["update_date"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(User));
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

			User castObj = (User)obj;
			return (castObj != null && this.userId == castObj.UserId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.userId.GetHashCode());
		}
		#endregion
	}
}
