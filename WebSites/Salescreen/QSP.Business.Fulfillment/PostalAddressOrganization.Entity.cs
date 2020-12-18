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
	[Class(Schema="`dbo`", Table="`postal_address_organization`")]
	public partial class PostalAddressOrganization
	{
		#region Constants
        public const string PostalAddressOrganizationEntity = "PostalAddressOrganization";
		public const string PostalAddressOrganizationIdProperty = "PostalAddressOrganizationId";
		public const string PostalAddressIdProperty = "PostalAddressId";
		public const string OrganizationIdProperty = "OrganizationId";
		public const string PostalAddressTypeIdProperty = "PostalAddressTypeId";
		public const string DeletedProperty = "Deleted";
		public const string CreateDateProperty = "CreateDate";
		public const string CreateUserIdProperty = "CreateUserId";
		public const string UpdateDateProperty = "UpdateDate";
		public const string UpdateUserIdProperty = "UpdateUserId";
		#endregion

		#region Fields
		protected int postalAddressOrganizationId = 0;
		protected int postalAddressId = 0;
		protected int organizationId = 0;
		protected int postalAddressTypeId = 0;
		protected bool deleted = false;
		protected DateTime createDate = DateTime.Now;
		protected int createUserId = 0;
		protected DateTime? updateDate = DateTime.Now;
		protected int? updateUserId = null;
		#endregion

		#region Constructors
		public PostalAddressOrganization() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""PostalAddressOrganizationId"" column=""`postal_address_organization_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int PostalAddressOrganizationId
		{
			get { return this.postalAddressOrganizationId; }
			set { this.postalAddressOrganizationId = value; }
		}

		[Property(Column="`postal_address_id`")]
		public virtual int PostalAddressId
		{
			get { return this.postalAddressId; }
			set { this.postalAddressId = value; }
		}

		[Property(Column="`organization_id`")]
		public virtual int OrganizationId
		{
			get { return this.organizationId; }
			set { this.organizationId = value; }
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
                ICriteria c = session.CreateCriteria(typeof(PostalAddressOrganization));
                return c;
            }
        }

        public static List<PostalAddressOrganization> GetPostalAddressOrganizationList(ICriteria criteria)
        {
            return (List<PostalAddressOrganization>)criteria.List<PostalAddressOrganization>();
        }

		public static PostalAddressOrganization GetPostalAddressOrganization(int postalAddressOrganizationId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<PostalAddressOrganization>(postalAddressOrganizationId);
			}
		}

		public static List<PostalAddressOrganization> GetPostalAddressOrganizationList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(PostalAddressOrganization));
				return (List<PostalAddressOrganization>)c.List<PostalAddressOrganization>();
			}
		}

		public static List<PostalAddressOrganization> GetPostalAddressOrganizationList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(PostalAddressOrganization));
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

				return (List<PostalAddressOrganization>)c.List<PostalAddressOrganization>();
			}
		}

		public static List<PostalAddressOrganization> GetPostalAddressOrganizationList(string sortExpression)
		{
			return GetPostalAddressOrganizationList(sortExpression, -1, -1);
		}

		public static void InsertPostalAddressOrganization(PostalAddressOrganization obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdatePostalAddressOrganization(PostalAddressOrganization obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeletePostalAddressOrganization(PostalAddressOrganization obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static PostalAddressOrganization PopulatePostalAddressOrganization(IDataReader r)
		{
			PostalAddressOrganization obj = new PostalAddressOrganization();
			obj.PostalAddressOrganizationId = (int)r["postal_address_organization_id"];
			obj.PostalAddressId = (int)r["postal_address_id"];
			obj.OrganizationId = (int)r["organization_id"];
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
				ICriteria c = session.CreateCriteria(typeof(PostalAddressOrganization));
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

			PostalAddressOrganization castObj = (PostalAddressOrganization)obj;
			return (castObj != null && this.postalAddressOrganizationId == castObj.PostalAddressOrganizationId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.postalAddressOrganizationId.GetHashCode());
		}
		#endregion
	}
}
