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
	[Class(Schema="`dbo`", Table="`vendor`")]
	public partial class Vendor
	{
		#region Constants
        public const string VendorEntity = "Vendor";
		public const string VendorIdProperty = "VendorId";
		public const string VendorCodeProperty = "VendorCode";
		public const string VendorNameProperty = "VendorName";
		public const string PostalAddressIdProperty = "PostalAddressId";
		public const string PhoneNumberIdProperty = "PhoneNumberId";
		public const string FaxNumberIdProperty = "FaxNumberId";
		public const string EmailIdProperty = "EmailId";
		public const string DivisionProperty = "Division";
		public const string VendorTermProperty = "VendorTerm";
		public const string OracleVendorCodeProperty = "OracleVendorCode";
		public const string POCOProperty = "POCO";
		public const string LastImportDateProperty = "LastImportDate";
		public const string ImportSourceIdProperty = "ImportSourceId";
		public const string DeletedProperty = "Deleted";
		public const string CreateDateProperty = "CreateDate";
		public const string CreateUserIdProperty = "CreateUserId";
		public const string UpdateDateProperty = "UpdateDate";
		public const string UpdateUserIdProperty = "UpdateUserId";
		public const string VendorTypeIdProperty = "VendorTypeId";
		#endregion

		#region Fields
		protected int vendorId = 0;
		protected string vendorCode = null;
		protected string vendorName = "";
		protected int? postalAddressId = null;
		protected int? phoneNumberId = null;
		protected int? faxNumberId = null;
		protected int? emailId = null;
		protected string division = null;
		protected string vendorTerm = null;
		protected string oracleVendorCode = null;
		protected string pOCO = null;
		protected DateTime? lastImportDate = null;
		protected int? importSourceId = null;
		protected bool deleted = false;
		protected DateTime createDate = DateTime.Now;
		protected int createUserId = 0;
		protected DateTime? updateDate = DateTime.Now;
		protected int? updateUserId = null;
		protected int vendorTypeId = 1;
		#endregion

		#region Constructors
		public Vendor() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""VendorId"" column=""`vendor_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int VendorId
		{
			get { return this.vendorId; }
			set { this.vendorId = value; }
		}

		[Property(Column="`vendor_code`")]
		public virtual string VendorCode
		{
			get { return this.vendorCode; }
			set { this.vendorCode = value; }
		}

		[Property(Column="`vendor_name`")]
		public virtual string VendorName
		{
			get { return this.vendorName; }
			set { this.vendorName = value; }
		}

		[Property(Column="`postal_address_id`")]
		public virtual int? PostalAddressId
		{
			get { return this.postalAddressId; }
			set { this.postalAddressId = value; }
		}

		[Property(Column="`phone_number_id`")]
		public virtual int? PhoneNumberId
		{
			get { return this.phoneNumberId; }
			set { this.phoneNumberId = value; }
		}

		[Property(Column="`fax_number_id`")]
		public virtual int? FaxNumberId
		{
			get { return this.faxNumberId; }
			set { this.faxNumberId = value; }
		}

		[Property(Column="`email_id`")]
		public virtual int? EmailId
		{
			get { return this.emailId; }
			set { this.emailId = value; }
		}

		[Property(Column="`division`")]
		public virtual string Division
		{
			get { return this.division; }
			set { this.division = value; }
		}

		[Property(Column="`vendor_term`")]
		public virtual string VendorTerm
		{
			get { return this.vendorTerm; }
			set { this.vendorTerm = value; }
		}

		[Property(Column="`oracle_vendor_code`")]
		public virtual string OracleVendorCode
		{
			get { return this.oracleVendorCode; }
			set { this.oracleVendorCode = value; }
		}

		[Property(Column="`PO#CO`")]
		public virtual string POCO
		{
			get { return this.pOCO; }
			set { this.pOCO = value; }
		}

		[Property(Column="`last_import_date`")]
		public virtual DateTime? LastImportDate
		{
			get { return this.lastImportDate; }
			set { this.lastImportDate = value; }
		}

		[Property(Column="`import_source_id`")]
		public virtual int? ImportSourceId
		{
			get { return this.importSourceId; }
			set { this.importSourceId = value; }
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

		[Property(Column="`vendor_type_id`")]
		public virtual int VendorTypeId
		{
			get { return this.vendorTypeId; }
			set { this.vendorTypeId = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(Vendor));
                return c;
            }
        }

        public static List<Vendor> GetVendorList(ICriteria criteria)
        {
            return (List<Vendor>)criteria.List<Vendor>();
        }

		public static Vendor GetVendor(int vendorId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<Vendor>(vendorId);
			}
		}

		public static List<Vendor> GetVendorList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(Vendor));
				return (List<Vendor>)c.List<Vendor>();
			}
		}

		public static List<Vendor> GetVendorList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(Vendor));
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

				return (List<Vendor>)c.List<Vendor>();
			}
		}

		public static List<Vendor> GetVendorList(string sortExpression)
		{
			return GetVendorList(sortExpression, -1, -1);
		}

		public static void InsertVendor(Vendor obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateVendor(Vendor obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteVendor(Vendor obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static Vendor PopulateVendor(IDataReader r)
		{
			Vendor obj = new Vendor();
			obj.VendorId = (int)r["vendor_id"];
			obj.VendorCode = (r["vendor_code"] == DBNull.Value) ? null : (string)r["vendor_code"];
			obj.VendorName = (string)r["vendor_name"];
			obj.PostalAddressId = (r["postal_address_id"] == DBNull.Value) ? null : (int?)r["postal_address_id"];
			obj.PhoneNumberId = (r["phone_number_id"] == DBNull.Value) ? null : (int?)r["phone_number_id"];
			obj.FaxNumberId = (r["fax_number_id"] == DBNull.Value) ? null : (int?)r["fax_number_id"];
			obj.EmailId = (r["email_id"] == DBNull.Value) ? null : (int?)r["email_id"];
			obj.Division = (r["division"] == DBNull.Value) ? null : (string)r["division"];
			obj.VendorTerm = (r["vendor_term"] == DBNull.Value) ? null : (string)r["vendor_term"];
			obj.OracleVendorCode = (r["oracle_vendor_code"] == DBNull.Value) ? null : (string)r["oracle_vendor_code"];
			obj.POCO = (r["PO#CO"] == DBNull.Value) ? null : (string)r["PO#CO"];
			obj.LastImportDate = (r["last_import_date"] == DBNull.Value) ? null : (DateTime?)r["last_import_date"];
			obj.ImportSourceId = (r["import_source_id"] == DBNull.Value) ? null : (int?)r["import_source_id"];
			obj.Deleted = (bool)r["deleted"];
			obj.CreateDate = (DateTime)r["create_date"];
			obj.CreateUserId = (int)r["create_user_id"];
			obj.UpdateDate = (r["update_date"] == DBNull.Value) ? null : (DateTime?)r["update_date"];
			obj.UpdateUserId = (r["update_user_id"] == DBNull.Value) ? null : (int?)r["update_user_id"];
			obj.VendorTypeId = (int)r["vendor_type_id"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(Vendor));
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

			Vendor castObj = (Vendor)obj;
			return (castObj != null && this.vendorId == castObj.VendorId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.vendorId.GetHashCode());
		}
		#endregion
	}
}
