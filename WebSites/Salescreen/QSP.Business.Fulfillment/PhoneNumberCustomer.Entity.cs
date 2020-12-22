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
	[Class(Schema="`dbo`", Table="`phone_number_customer`")]
	public partial class PhoneNumberCustomer
	{
		#region Constants
        public const string PhoneNumberCustomerEntity = "PhoneNumberCustomer";
		public const string PhoneNumberCustomerIdProperty = "PhoneNumberCustomerId";
		public const string PhoneNumberIdProperty = "PhoneNumberId";
		public const string CustomerIdProperty = "CustomerId";
		public const string PhoneNumberTypeIdProperty = "PhoneNumberTypeId";
		public const string DeletedProperty = "Deleted";
		public const string CreateDateProperty = "CreateDate";
		public const string CreateUserIdProperty = "CreateUserId";
		public const string UpdateDateProperty = "UpdateDate";
		public const string UpdateUserIdProperty = "UpdateUserId";
		#endregion

		#region Fields
		protected int phoneNumberCustomerId = 0;
		protected int phoneNumberId = 0;
		protected int customerId = 0;
		protected int phoneNumberTypeId = 0;
		protected bool deleted = false;
		protected DateTime createDate = DateTime.Now;
		protected int createUserId = 0;
		protected DateTime? updateDate = DateTime.Now;
		protected int? updateUserId = null;
		#endregion

		#region Constructors
		public PhoneNumberCustomer() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""PhoneNumberCustomerId"" column=""`phone_number_customer_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int PhoneNumberCustomerId
		{
			get { return this.phoneNumberCustomerId; }
			set { this.phoneNumberCustomerId = value; }
		}

		[Property(Column="`phone_number_id`")]
		public virtual int PhoneNumberId
		{
			get { return this.phoneNumberId; }
			set { this.phoneNumberId = value; }
		}

		[Property(Column="`customer_id`")]
		public virtual int CustomerId
		{
			get { return this.customerId; }
			set { this.customerId = value; }
		}

		[Property(Column="`phone_number_type_id`")]
		public virtual int PhoneNumberTypeId
		{
			get { return this.phoneNumberTypeId; }
			set { this.phoneNumberTypeId = value; }
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
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(PhoneNumberCustomer));
                return c;
            }
        }

        public static List<PhoneNumberCustomer> GetPhoneNumberCustomerList(ICriteria criteria)
        {
            return (List<PhoneNumberCustomer>)criteria.List<PhoneNumberCustomer>();
        }

		public static PhoneNumberCustomer GetPhoneNumberCustomer(int phoneNumberCustomerId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<PhoneNumberCustomer>(phoneNumberCustomerId);
			}
		}

		public static List<PhoneNumberCustomer> GetPhoneNumberCustomerList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(PhoneNumberCustomer));
				return (List<PhoneNumberCustomer>)c.List<PhoneNumberCustomer>();
			}
		}

		public static List<PhoneNumberCustomer> GetPhoneNumberCustomerList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(PhoneNumberCustomer));
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

				return (List<PhoneNumberCustomer>)c.List<PhoneNumberCustomer>();
			}
		}

		public static List<PhoneNumberCustomer> GetPhoneNumberCustomerList(string sortExpression)
		{
			return GetPhoneNumberCustomerList(sortExpression, -1, -1);
		}

		public static void InsertPhoneNumberCustomer(PhoneNumberCustomer obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdatePhoneNumberCustomer(PhoneNumberCustomer obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeletePhoneNumberCustomer(PhoneNumberCustomer obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static PhoneNumberCustomer PopulatePhoneNumberCustomer(IDataReader r)
		{
			PhoneNumberCustomer obj = new PhoneNumberCustomer();
			obj.PhoneNumberCustomerId = (int)r["phone_number_customer_id"];
			obj.PhoneNumberId = (int)r["phone_number_id"];
			obj.CustomerId = (int)r["customer_id"];
			obj.PhoneNumberTypeId = (int)r["phone_number_type_id"];
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
				ICriteria c = session.CreateCriteria(typeof(PhoneNumberCustomer));
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

			PhoneNumberCustomer castObj = (PhoneNumberCustomer)obj;
			return (castObj != null && this.phoneNumberCustomerId == castObj.PhoneNumberCustomerId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.phoneNumberCustomerId.GetHashCode());
		}
		#endregion
	}
}
