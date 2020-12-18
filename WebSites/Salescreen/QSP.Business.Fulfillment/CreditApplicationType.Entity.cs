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
	[Class(Schema="`dbo`", Table="`credit_application_type`")]
	public partial class CreditApplicationType
	{
		#region Constants
        public const string CreditApplicationTypeEntity = "CreditApplicationType";
		public const string CreditApplicationTypeIdProperty = "CreditApplicationTypeId";
		public const string CreditApplicationTypeNameProperty = "CreditApplicationTypeName";
		public const string DescriptionProperty = "Description";
		#endregion

		#region Fields
		protected int creditApplicationTypeId = 0;
		protected string creditApplicationTypeName = "";
		protected string description = null;
		#endregion

		#region Constructors
		public CreditApplicationType() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""CreditApplicationTypeId"" column=""`credit_application_type_id`"">
			<generator class=""assigned"">
			</generator>
		</id>")]

		public virtual int CreditApplicationTypeId
		{
			get { return this.creditApplicationTypeId; }
			set { this.creditApplicationTypeId = value; }
		}

		[Property(Column="`credit_application_type_name`")]
		public virtual string CreditApplicationTypeName
		{
			get { return this.creditApplicationTypeName; }
			set { this.creditApplicationTypeName = value; }
		}

		[Property(Column="`description`")]
		public virtual string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(CreditApplicationType));
                return c;
            }
        }

        public static List<CreditApplicationType> GetCreditApplicationTypeList(ICriteria criteria)
        {
            return (List<CreditApplicationType>)criteria.List<CreditApplicationType>();
        }

		public static CreditApplicationType GetCreditApplicationType(int creditApplicationTypeId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<CreditApplicationType>(creditApplicationTypeId);
			}
		}

		public static List<CreditApplicationType> GetCreditApplicationTypeList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(CreditApplicationType));
				return (List<CreditApplicationType>)c.List<CreditApplicationType>();
			}
		}

		public static List<CreditApplicationType> GetCreditApplicationTypeList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(CreditApplicationType));
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

				return (List<CreditApplicationType>)c.List<CreditApplicationType>();
			}
		}

		public static List<CreditApplicationType> GetCreditApplicationTypeList(string sortExpression)
		{
			return GetCreditApplicationTypeList(sortExpression, -1, -1);
		}

		public static void InsertCreditApplicationType(CreditApplicationType obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateCreditApplicationType(CreditApplicationType obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteCreditApplicationType(CreditApplicationType obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static CreditApplicationType PopulateCreditApplicationType(IDataReader r)
		{
			CreditApplicationType obj = new CreditApplicationType();
			obj.CreditApplicationTypeId = (int)r["credit_application_type_id"];
			obj.CreditApplicationTypeName = (string)r["credit_application_type_name"];
			obj.Description = (r["description"] == DBNull.Value) ? null : (string)r["description"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(CreditApplicationType));
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

			CreditApplicationType castObj = (CreditApplicationType)obj;
			return (castObj != null && this.creditApplicationTypeId == castObj.CreditApplicationTypeId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.creditApplicationTypeId.GetHashCode());
		}
		#endregion
	}
}
