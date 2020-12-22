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
	[Class(Schema="`dbo`", Table="`credit_application`")]
	public partial class CreditApplication
	{
		#region Constants
        public const string CreditApplicationEntity = "CreditApplication";
		public const string CreditApplicationIdProperty = "CreditApplicationId";
		public const string CreditApplicationTypeIdProperty = "CreditApplicationTypeId";
		public const string AccountIdProperty = "AccountId";
		public const string CustomerIdProperty = "CustomerId";
		public const string CreditCardIdProperty = "CreditCardId";
		public const string SocialSecurityNumberProperty = "SocialSecurityNumber";
		public const string CreditLimitProperty = "CreditLimit";
		public const string DeletedProperty = "Deleted";
		public const string CreateDateProperty = "CreateDate";
		public const string CreateUserIdProperty = "CreateUserId";
		public const string UpdateDateProperty = "UpdateDate";
		public const string UpdateUserIdProperty = "UpdateUserId";
		public const string ApproveDateProperty = "ApproveDate";
		public const string ApproveUserIdProperty = "ApproveUserId";
		public const string ApprovedProperty = "Approved";
		public const string CreditApplicationStatusIdProperty = "CreditApplicationStatusId";
		public const string HomePhoneNumberIdProperty = "HomePhoneNumberId";
		public const string OfficerNameProperty = "OfficerName";
		public const string PhoneNumberIdProperty = "PhoneNumberId";
		public const string PostalAddressIdProperty = "PostalAddressId";
		public const string FormIdProperty = "FormId";
		public const string ApproveCodeProperty = "ApproveCode";
		#endregion

		#region Fields
		protected int creditApplicationId = 0;
		protected int creditApplicationTypeId = 0;
		protected int accountId = 0;
		protected int? customerId = null;
		protected int? creditCardId = null;
		protected string socialSecurityNumber = null;
		protected decimal creditLimit = 0;
		protected bool deleted = false;
		protected DateTime createDate = DateTime.Now;
		protected int createUserId = 0;
		protected DateTime? updateDate = DateTime.Now;
		protected int? updateUserId = null;
		protected DateTime? approveDate = null;
		protected int? approveUserId = null;
		protected bool? approved = false;
		protected int? creditApplicationStatusId = 0;
		protected int? homePhoneNumberId = null;
		protected string officerName = null;
		protected int? phoneNumberId = null;
		protected int? postalAddressId = null;
		protected int? formId = null;
		protected string approveCode = null;
		#endregion

		#region Constructors
		public CreditApplication() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""CreditApplicationId"" column=""`credit_application_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int CreditApplicationId
		{
			get { return this.creditApplicationId; }
			set { this.creditApplicationId = value; }
		}

		[Property(Column="`credit_application_type_id`")]
		public virtual int CreditApplicationTypeId
		{
			get { return this.creditApplicationTypeId; }
			set { this.creditApplicationTypeId = value; }
		}

		[Property(Column="`account_id`")]
		public virtual int AccountId
		{
			get { return this.accountId; }
			set { this.accountId = value; }
		}

		[Property(Column="`customer_id`")]
		public virtual int? CustomerId
		{
			get { return this.customerId; }
			set { this.customerId = value; }
		}

		[Property(Column="`credit_card_id`")]
		public virtual int? CreditCardId
		{
			get { return this.creditCardId; }
			set { this.creditCardId = value; }
		}

		[Property(Column="`social_security_number`")]
		public virtual string SocialSecurityNumber
		{
			get { return this.socialSecurityNumber; }
			set { this.socialSecurityNumber = value; }
		}

		[Property(Column="`credit_limit`")]
		public virtual decimal CreditLimit
		{
			get { return this.creditLimit; }
			set { this.creditLimit = value; }
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

		[Property(Column="`approve_date`")]
		public virtual DateTime? ApproveDate
		{
			get { return this.approveDate; }
			set { this.approveDate = value; }
		}

		[Property(Column="`approve_user_id`")]
		public virtual int? ApproveUserId
		{
			get { return this.approveUserId; }
			set { this.approveUserId = value; }
		}

		[Property(Column="`approved`")]
		public virtual bool? Approved
		{
			get { return this.approved; }
			set { this.approved = value; }
		}

		[Property(Column="`credit_application_status_id`")]
		public virtual int? CreditApplicationStatusId
		{
			get { return this.creditApplicationStatusId; }
			set { this.creditApplicationStatusId = value; }
		}

		[Property(Column="`home_phone_number_id`")]
		public virtual int? HomePhoneNumberId
		{
			get { return this.homePhoneNumberId; }
			set { this.homePhoneNumberId = value; }
		}

		[Property(Column="`officer_name`")]
		public virtual string OfficerName
		{
			get { return this.officerName; }
			set { this.officerName = value; }
		}

		[Property(Column="`phone_number_id`")]
		public virtual int? PhoneNumberId
		{
			get { return this.phoneNumberId; }
			set { this.phoneNumberId = value; }
		}

		[Property(Column="`postal_address_id`")]
		public virtual int? PostalAddressId
		{
			get { return this.postalAddressId; }
			set { this.postalAddressId = value; }
		}

		[Property(Column="`form_id`")]
		public virtual int? FormId
		{
			get { return this.formId; }
			set { this.formId = value; }
		}

		[Property(Column="`approve_code`")]
		public virtual string ApproveCode
		{
			get { return this.approveCode; }
			set { this.approveCode = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(CreditApplication));
                return c;
            }
        }

        public static List<CreditApplication> GetCreditApplicationList(ICriteria criteria)
        {
            return (List<CreditApplication>)criteria.List<CreditApplication>();
        }

		public static CreditApplication GetCreditApplication(int creditApplicationId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<CreditApplication>(creditApplicationId);
			}
		}

		public static List<CreditApplication> GetCreditApplicationList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(CreditApplication));
				return (List<CreditApplication>)c.List<CreditApplication>();
			}
		}

		public static List<CreditApplication> GetCreditApplicationList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(CreditApplication));
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

				return (List<CreditApplication>)c.List<CreditApplication>();
			}
		}

		public static List<CreditApplication> GetCreditApplicationList(string sortExpression)
		{
			return GetCreditApplicationList(sortExpression, -1, -1);
		}

		public static void InsertCreditApplication(CreditApplication obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateCreditApplication(CreditApplication obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteCreditApplication(CreditApplication obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static CreditApplication PopulateCreditApplication(IDataReader r)
		{
			CreditApplication obj = new CreditApplication();
			obj.CreditApplicationId = (int)r["credit_application_id"];
			obj.CreditApplicationTypeId = (int)r["credit_application_type_id"];
			obj.AccountId = (int)r["account_id"];
			obj.CustomerId = (r["customer_id"] == DBNull.Value) ? null : (int?)r["customer_id"];
			obj.CreditCardId = (r["credit_card_id"] == DBNull.Value) ? null : (int?)r["credit_card_id"];
			obj.SocialSecurityNumber = (r["social_security_number"] == DBNull.Value) ? null : (string)r["social_security_number"];
			obj.CreditLimit = (decimal)r["credit_limit"];
			obj.Deleted = (bool)r["deleted"];
			obj.CreateDate = (DateTime)r["create_date"];
			obj.CreateUserId = (int)r["create_user_id"];
			obj.UpdateDate = (r["update_date"] == DBNull.Value) ? null : (DateTime?)r["update_date"];
			obj.UpdateUserId = (r["update_user_id"] == DBNull.Value) ? null : (int?)r["update_user_id"];
			obj.ApproveDate = (r["approve_date"] == DBNull.Value) ? null : (DateTime?)r["approve_date"];
			obj.ApproveUserId = (r["approve_user_id"] == DBNull.Value) ? null : (int?)r["approve_user_id"];
			obj.Approved = (r["approved"] == DBNull.Value) ? null : (bool?)r["approved"];
			obj.CreditApplicationStatusId = (r["credit_application_status_id"] == DBNull.Value) ? null : (int?)r["credit_application_status_id"];
			obj.HomePhoneNumberId = (r["home_phone_number_id"] == DBNull.Value) ? null : (int?)r["home_phone_number_id"];
			obj.OfficerName = (r["officer_name"] == DBNull.Value) ? null : (string)r["officer_name"];
			obj.PhoneNumberId = (r["phone_number_id"] == DBNull.Value) ? null : (int?)r["phone_number_id"];
			obj.PostalAddressId = (r["postal_address_id"] == DBNull.Value) ? null : (int?)r["postal_address_id"];
			obj.FormId = (r["form_id"] == DBNull.Value) ? null : (int?)r["form_id"];
			obj.ApproveCode = (r["approve_code"] == DBNull.Value) ? null : (string)r["approve_code"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(CreditApplication));
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

			CreditApplication castObj = (CreditApplication)obj;
			return (castObj != null && this.creditApplicationId == castObj.CreditApplicationId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.creditApplicationId.GetHashCode());
		}
		#endregion
	}
}
