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
	[Class(Schema="`dbo`", Table="`postal_address_customer`")]
	public partial class PostalAddressCustomer
	{
		#region Constants
        public const string PostalAddressCustomerEntity = "PostalAddressCustomer";
		public const string PostalAddressCustomerIdProperty = "PostalAddressCustomerId";
		public const string PostalAddressIdProperty = "PostalAddressId";
		public const string CustomerIdProperty = "CustomerId";
		public const string PostalAddressTypeIdProperty = "PostalAddressTypeId";
		public const string DeletedProperty = "Deleted";
		public const string CreateDateProperty = "CreateDate";
		public const string CreateUserIdProperty = "CreateUserId";
		public const string UpdateDateProperty = "UpdateDate";
		public const string UpdateUserIdProperty = "UpdateUserId";
		#endregion

		#region Fields
		protected int postalAddressCustomerId = 0;
		protected int postalAddressId = 0;
		protected int customerId = 0;
		protected int postalAddressTypeId = 0;
		protected bool deleted = false;
		protected DateTime createDate = DateTime.Now;
		protected int createUserId = 0;
		protected DateTime? updateDate = DateTime.Now;
		protected int? updateUserId = null;
		#endregion

		#region Constructors
		public PostalAddressCustomer() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""PostalAddressCustomerId"" column=""`postal_address_customer_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int PostalAddressCustomerId
		{
			get { return this.postalAddressCustomerId; }
			set { this.postalAddressCustomerId = value; }
		}

		[Property(Column="`postal_address_id`")]
		public virtual int PostalAddressId
		{
			get { return this.postalAddressId; }
			set { this.postalAddressId = value; }
		}

		[Property(Column="`customer_id`")]
		public virtual int CustomerId
		{
			get { return this.customerId; }
			set { this.customerId = value; }
		}

		[Property(Column="`postal_address_type_id`")]
		public virtual int PostalAddressTypeId
		{
			get { return this.postalAddressTypeId; }
			set { this.postalAddressTypeId = value; }
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
                ICriteria c = session.CreateCriteria(typeof(PostalAddressCustomer));
                return c;
            }
        }

        public static List<PostalAddressCustomer> GetPostalAddressCustomerList(ICriteria criteria)
        {
            return (List<PostalAddressCustomer>)criteria.List<PostalAddressCustomer>();
        }

		public static PostalAddressCustomer GetPostalAddressCustomer(int postalAddressCustomerId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<PostalAddressCustomer>(postalAddressCustomerId);
			}
		}

		public static List<PostalAddressCustomer> GetPostalAddressCustomerList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(PostalAddressCustomer));
				return (List<PostalAddressCustomer>)c.List<PostalAddressCustomer>();
			}
		}

		public static List<PostalAddressCustomer> GetPostalAddressCustomerList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(PostalAddressCustomer));
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

				return (List<PostalAddressCustomer>)c.List<PostalAddressCustomer>();
			}
		}

		public static List<PostalAddressCustomer> GetPostalAddressCustomerList(string sortExpression)
		{
			return GetPostalAddressCustomerList(sortExpression, -1, -1);
		}

		public static void InsertPostalAddressCustomer(PostalAddressCustomer obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdatePostalAddressCustomer(PostalAddressCustomer obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeletePostalAddressCustomer(PostalAddressCustomer obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static PostalAddressCustomer PopulatePostalAddressCustomer(IDataReader r)
		{
			PostalAddressCustomer obj = new PostalAddressCustomer();
			obj.PostalAddressCustomerId = (int)r["postal_address_customer_id"];
			obj.PostalAddressId = (int)r["postal_address_id"];
			obj.CustomerId = (int)r["customer_id"];
			obj.PostalAddressTypeId = (int)r["postal_address_type_id"];
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
				ICriteria c = session.CreateCriteria(typeof(PostalAddressCustomer));
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

			PostalAddressCustomer castObj = (PostalAddressCustomer)obj;
			return (castObj != null && this.postalAddressCustomerId == castObj.PostalAddressCustomerId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.postalAddressCustomerId.GetHashCode());
		}
		#endregion
	}
}
