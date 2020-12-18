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
	[Class(Schema="`dbo`", Table="`phone_number_type`")]
	public partial class PhoneNumberType
	{
		#region Constants
        public const string PhoneNumberTypeEntity = "PhoneNumberType";
		public const string PhoneNumberTypeIdProperty = "PhoneNumberTypeId";
		public const string PhoneNumberTypeNameProperty = "PhoneNumberTypeName";
		#endregion

		#region Fields
		protected int phoneNumberTypeId = 0;
		protected string phoneNumberTypeName = "";
		#endregion

		#region Constructors
		public PhoneNumberType() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""PhoneNumberTypeId"" column=""`phone_number_type_id`"">
			<generator class=""assigned"">
			</generator>
		</id>")]

		public virtual int PhoneNumberTypeId
		{
			get { return this.phoneNumberTypeId; }
			set { this.phoneNumberTypeId = value; }
		}

		[Property(Column="`phone_number_type_name`")]
		public virtual string PhoneNumberTypeName
		{
			get { return this.phoneNumberTypeName; }
			set { this.phoneNumberTypeName = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(PhoneNumberType));
                return c;
            }
        }

        public static List<PhoneNumberType> GetPhoneNumberTypeList(ICriteria criteria)
        {
            return (List<PhoneNumberType>)criteria.List<PhoneNumberType>();
        }

		public static PhoneNumberType GetPhoneNumberType(int phoneNumberTypeId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<PhoneNumberType>(phoneNumberTypeId);
			}
		}

		public static List<PhoneNumberType> GetPhoneNumberTypeList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(PhoneNumberType));
				return (List<PhoneNumberType>)c.List<PhoneNumberType>();
			}
		}

		public static List<PhoneNumberType> GetPhoneNumberTypeList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(PhoneNumberType));
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

				return (List<PhoneNumberType>)c.List<PhoneNumberType>();
			}
		}

		public static List<PhoneNumberType> GetPhoneNumberTypeList(string sortExpression)
		{
			return GetPhoneNumberTypeList(sortExpression, -1, -1);
		}

		public static void InsertPhoneNumberType(PhoneNumberType obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdatePhoneNumberType(PhoneNumberType obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeletePhoneNumberType(PhoneNumberType obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static PhoneNumberType PopulatePhoneNumberType(IDataReader r)
		{
			PhoneNumberType obj = new PhoneNumberType();
			obj.PhoneNumberTypeId = (int)r["phone_number_type_id"];
			obj.PhoneNumberTypeName = (string)r["phone_number_type_name"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(PhoneNumberType));
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

			PhoneNumberType castObj = (PhoneNumberType)obj;
			return (castObj != null && this.phoneNumberTypeId == castObj.PhoneNumberTypeId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.phoneNumberTypeId.GetHashCode());
		}
		#endregion
	}
}
