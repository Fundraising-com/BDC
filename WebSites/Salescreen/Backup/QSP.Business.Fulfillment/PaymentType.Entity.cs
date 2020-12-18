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
	[Class(Schema="`dbo`", Table="`payment_type`")]
	public partial class PaymentType
	{
		#region Constants
        public const string PaymentTypeEntity = "PaymentType";
		public const string PaymentTypeIdProperty = "PaymentTypeId";
		public const string PaymentTypeNameProperty = "PaymentTypeName";
		#endregion

		#region Fields
		protected int paymentTypeId = 0;
		protected string paymentTypeName = "";
		#endregion

		#region Constructors
		public PaymentType() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""PaymentTypeId"" column=""`payment_type_id`"">
			<generator class=""assigned"">
			</generator>
		</id>")]

		public virtual int PaymentTypeId
		{
			get { return this.paymentTypeId; }
			set { this.paymentTypeId = value; }
		}

		[Property(Column="`payment_type_name`")]
		public virtual string PaymentTypeName
		{
			get { return this.paymentTypeName; }
			set { this.paymentTypeName = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(PaymentType));
                return c;
            }
        }

        public static List<PaymentType> GetPaymentTypeList(ICriteria criteria)
        {
            return (List<PaymentType>)criteria.List<PaymentType>();
        }

		public static PaymentType GetPaymentType(int paymentTypeId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<PaymentType>(paymentTypeId);
			}
		}

		public static List<PaymentType> GetPaymentTypeList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(PaymentType));
				return (List<PaymentType>)c.List<PaymentType>();
			}
		}

		public static List<PaymentType> GetPaymentTypeList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(PaymentType));
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

				return (List<PaymentType>)c.List<PaymentType>();
			}
		}

		public static List<PaymentType> GetPaymentTypeList(string sortExpression)
		{
			return GetPaymentTypeList(sortExpression, -1, -1);
		}

		public static void InsertPaymentType(PaymentType obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdatePaymentType(PaymentType obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeletePaymentType(PaymentType obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static PaymentType PopulatePaymentType(IDataReader r)
		{
			PaymentType obj = new PaymentType();
			obj.PaymentTypeId = (int)r["payment_type_id"];
			obj.PaymentTypeName = (string)r["payment_type_name"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(PaymentType));
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

			PaymentType castObj = (PaymentType)obj;
			return (castObj != null && this.paymentTypeId == castObj.PaymentTypeId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.paymentTypeId.GetHashCode());
		}
		#endregion
	}
}
